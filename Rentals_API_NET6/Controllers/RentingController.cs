using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models.DatabaseModel;
using Rentals_API_NET6.Models.InputModel;
using Rentals_API_NET6.Models.OutputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Rentals_API_NET6.Models.DatabaseModel.ItemHistoryLog;
using Action = Rentals_API_NET6.Models.DatabaseModel.Action;

namespace Rentals_API_NET6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<RentingController> _logger;
        public RentingController(RentalsDbContext context, IAuthorizationService authorizationService, ILogger<RentingController> logger)
        {
            _context = context;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        /// <summary>
        /// Vytvoření nové výpůjčky
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Renting>> AddNewRenting([FromBody] RentingRequest renting)
        {
            var error = 0;
            var userId = UserId();
            User owner = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (owner != null)
            {
                Renting newRenting = new Renting
                {
                    Start = renting.Start,
                    End = renting.End,
                    OwnerId = owner.Id,
                    Note = renting.Note,
                };
                newRenting.State = RentingState.WillStart;
                _context.Rentings.Add(newRenting);
                await _context.SaveChangesAsync();

                //Přidání itemů
                foreach (var item in renting.Items)
                {
                    if (_context.Items.Any(x => x.Id == item) && _context.Items.Find(item).State == ItemState.Available && _context.Items.Find(item).IsDeleted == false)
                    {
                        var rentingItem = new RentingItem { ItemId = item, RentingId = newRenting.Id };
                        _context.RentingItems.Add(rentingItem);
                    }
                    else
                    {
                        error++;
                    }
                }

                //Smazání košíku
                foreach (var item in _context.CartItems.Where(x => x.User.OauthId == userId))
                {
                    _context.Remove(item);
                }

                if (error == 0)
                {
                    RentingHistoryLog log = new RentingHistoryLog
                    {
                        RentingId = newRenting.Id,
                        UserId = owner.Id,
                        ChangedTime = DateTime.Now,
                        Action = Action.Created
                    };
                    _context.RentingHistoryLogs.Add(log);
                    await _context.SaveChangesAsync();
                    return Ok(newRenting);
                }
                else
                {
                    _context.Rentings.Remove(newRenting);
                    await _context.SaveChangesAsync();
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vrácení předmětů dané výpůjčky
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpPut]
        public async Task<ActionResult<Renting>> ChangeRenting([FromBody] ChangeRentingRequest request)
        {
            var errors = 0;
            Renting renting = _context.Rentings.SingleOrDefault(x => x.Id == request.Id);
            if (renting != null)
            {
                var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                RentingHistoryLog log = new RentingHistoryLog
                {
                    RentingId = renting.Id,
                    UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                    ChangedTime = DateTime.Now,
                    Action = Action.Changed,
                    ReturnedItems = new List<Item>()
                };

                if (request.ReturnedItems != null)
                {
                    foreach (var item in request.ReturnedItems)
                    {
                        //Pokud se ve výpůjčce nachází - vrácení itemu
                        if (_context.RentingItems.Any(x => x.RentingId == request.Id && x.ItemId == item && x.Returned == false) && _context.InventoryItems.Any(x => x.UserId == renting.OwnerId && x.ItemId == item))
                        {
                            var Ritem = _context.RentingItems.SingleOrDefault(x => x.RentingId == request.Id && x.ItemId == item);
                            Ritem.Returned = true;
                            _context.Entry(Ritem).State = EntityState.Modified;

                            //Odebrání itemu z uživatelova inventáře
                            _context.InventoryItems.Remove(_context.InventoryItems.Single(x => x.UserId == renting.OwnerId && x.ItemId == item));

                            //Změna stavu itemu
                            var itemToReturn = _context.Items.Find(item);
                            itemToReturn.State = ItemState.Available;
                            _context.Entry(itemToReturn).State = EntityState.Modified;
                        }
                        else
                        {
                            errors++;
                        }
                    }
                }
                if (errors == 0)
                {
                    //nelze ukončit pokud se neprojeví vrácení
                    _context.RentingHistoryLogs.Add(log);
                    await _context.SaveChangesAsync();

                    foreach (var item in request.ReturnedItems)
                    {
                        ItemHistoryLog Itemlog = new ItemHistoryLog
                        {
                            ItemId = item,
                            UserId = _context.Users.SingleOrDefault(x => x.OauthId == UserId()).Id,
                            UserInventoryId = null,
                            ChangedTime = DateTime.Now,
                            Action = ItemAction.DeletedFromInventory
                        };
                        _context.ItemHistoryLogs.Add(Itemlog);
                        log.ReturnedItems.Add(_context.Items.SingleOrDefault(x => x.Id == item));
                    }
                    await _context.SaveChangesAsync();

                    //Ukončení pokud vše vráceno
                    if (!_context.RentingItems.Any(x => x.RentingId == request.Id && x.Returned == false))
                    {
                        renting.State = RentingState.Ended;
                        renting.End = DateTime.Now;
                        log.Action = Action.Returned;
                        _context.Entry(log).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                    return Ok(renting);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Zrušení neuskutečněné výpůjčky
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Renting>> CancelRenting(int id)
        {
            var userId = UserId();
            Renting renting = _context.Rentings.SingleOrDefault(x => x.Id == id);
            if (renting != null && renting.State == RentingState.WillStart)
            {
                //Odebrání itemů
                foreach (var item in _context.RentingItems.Where(x => x.RentingId == id))
                {
                    _context.RentingItems.Remove(item);
                }

                renting.State = RentingState.Cancelled;
                _context.Entry(renting).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                foreach (var item in _context.RentingItems.Where(x => x.RentingId == id).Select(c => c.ItemId))
                {
                    ItemHistoryLog Itemlog = new ItemHistoryLog
                    {
                        ItemId = item,
                        UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                        UserInventoryId = null,
                        ChangedTime = DateTime.Now,
                        Action = ItemAction.DeletedFromInventory
                    };
                    _context.ItemHistoryLogs.Add(Itemlog);
                }

                await _context.SaveChangesAsync();

                RentingHistoryLog log = new RentingHistoryLog
                {
                    RentingId = renting.Id,
                    UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                    ChangedTime = DateTime.Now,
                    Action = Action.Canceled
                };
                _context.RentingHistoryLogs.Add(log);

                await _context.SaveChangesAsync();
                return Ok(renting);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vypíše všechny výpůjčky
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Renting>>> GetAllRentings()
        {
            IEnumerable<Renting> rentings = _context.Rentings.Include(o => o.Owner).Include(x => x.Items).OrderBy(x => x.State).AsEnumerable();
            return Ok(rentings);
        }

        /// <summary>
        /// Vypíše všechny výpůjčky podle stavu
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpGet("AllByState")]
        public async Task<ActionResult<IEnumerable<Renting>>> GetAllRentingsByState(int state)
        {
            IEnumerable<Renting> rentings = _context.Rentings.Include(o => o.Owner).Include(x => x.Items).Where(x => x.State == (RentingState)state).OrderBy(x => x.Start).AsEnumerable();
            return Ok(rentings);
        }

        /// <summary>
        /// Vypíše všechny výpůjčky daného uživatele
        /// </summary>
        [HttpGet("RentingsByUser/{id}")]
        public async Task<ActionResult<IEnumerable<Renting>>> GetRentings(string id)
        {
            var userId = UserId();
            var isEmployee = await _authorizationService.AuthorizeAsync(User, "Employee");
            User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
            if (user != null && (userId == user.OauthId || isEmployee.Succeeded))
            {
                IEnumerable<Renting> rentings = _context.Rentings.Include(x => x.Items).Where(y => y.OwnerId == user.Id).OrderBy(x => x.State).AsEnumerable();
                return Ok(rentings);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Aktivuje výpůjčku
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<Renting>> ActivateRenting(int id)
        {
            var userId = UserId();
            var error = 0;
            Renting renting = _context.Rentings.SingleOrDefault(x => x.Id == id);
            if (renting != null && renting.State == RentingState.WillStart)
            {
                renting.ApproverId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id;
                renting.State = RentingState.InProgress;
                _context.Entry(renting).State = EntityState.Modified;

                //Vypůjčení itemů
                foreach (var item in _context.RentingItems.Where(x => x.RentingId == id).Select(y => y.Item))
                {
                    if (_context.Items.Any(x => x.Id == item.Id) && _context.Items.Find(item.Id).State == ItemState.Available && _context.Items.Find(item.Id).IsDeleted == false)
                    {
                        //Vložení itemu do uživatelova inventáře
                        var inventoryitem = new InventoryItem { ItemId = item.Id, UserId = renting.OwnerId };
                        _context.InventoryItems.Add(inventoryitem);

                        //Nastavení stavu itemu na půjčený
                        var rentedItem = _context.Items.Find(item.Id);
                        rentedItem.State = ItemState.Rented;
                        _context.Entry(rentedItem).State = EntityState.Modified;
                    }
                    else
                    {
                        error++;
                    }
                }

                if (error == 0)
                {
                    await _context.SaveChangesAsync();
                    foreach (var item in _context.RentingItems.Where(x => x.RentingId == id).Select(y => y.Item))
                    {
                        ItemHistoryLog Itemlog = new ItemHistoryLog
                        {
                            ItemId = item.Id,
                            UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                            UserInventoryId = null,
                            ChangedTime = DateTime.Now,
                            Action = ItemAction.AddedToInventory
                        };
                        _context.ItemHistoryLogs.Add(Itemlog);
                    }

                    await _context.SaveChangesAsync();

                    RentingHistoryLog log = new RentingHistoryLog
                    {
                        RentingId = renting.Id,
                        UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                        ChangedTime = DateTime.Now,
                        Action = Action.Activated
                    };
                    _context.RentingHistoryLogs.Add(log);

                    await _context.SaveChangesAsync();
                    return Ok(renting);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vypíše data (od kdy do kdy) všech výpůjček, které probíhají nebo teprve začnou
        /// </summary>
        [HttpGet("Dates")]
        public async Task<ActionResult<List<DatesResponse>>> GetDates()
        {
            List<DatesResponse> dates = new();
            foreach (var item in _context.Rentings.Include(y => y.Owner).Where(x => x.State == RentingState.WillStart || x.State == RentingState.InProgress))
            {
                dates.Add(new DatesResponse
                {
                    Id = item.Id,
                    State = item.State,
                    Start = item.Start,
                    End = item.End,
                    Title = item.Owner.FullName
                });
            }
            return Ok(dates);
        }

        /// <summary>
        /// Vypíše předměty, které probíhající výpůjčka obsahuje (seznam k vrácení)
        /// </summary>
        [HttpGet("Items/{id}")]
        public async Task<ActionResult<List<Item>>> RentingDetail(int id)
        {
            Renting renting = _context.Rentings.SingleOrDefault(x => x.Id == id);
            if (renting != null && renting.State == RentingState.InProgress)
            {
                List<Item> items = _context.RentingItems.Where(x => x.RentingId == id && x.Returned == false).Select(x => x.Item).ToList();
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("History")]
        public async Task<ActionResult<List<RentingHistoryLog>>> GetHistory(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            List<RentingHistoryLog> history = _context.RentingHistoryLogs.Include(x => x.ReturnedItems).Include(x => x.User).Where(x => x.RentingId == item.Id).ToList();
            return Ok(history);
        }

        private string UserId()
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        }
    }
}
