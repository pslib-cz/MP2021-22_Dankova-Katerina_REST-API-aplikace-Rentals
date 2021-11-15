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
using Action = Rentals_API_NET6.Models.DatabaseModel.Action;

namespace Rentals_API_NET6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        public RentingController(RentalsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Vytvoření nové výpůjčky
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Renting>> AddNewRenting([FromBody] RentingRequest renting)
        {
            var error = 0;
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            //if (_context.Users.Any(x => x.OauthId == userId) && _context.Users.Any(x => x.OauthId == renting.Approver))
            if (_context.Users.Any(x => x.OauthId == userId))
            {
                User owner = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                //User approver = _context.Users.SingleOrDefault(x => x.OauthId == renting.Approver);
                Renting newRenting = new Renting
                {
                    Start = renting.Start,
                    End = renting.End,
                    OwnerId = owner.Id,
                    //ApproverId = approver.Id,
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

                if (error == 0)
                {
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
        [HttpPut]
        public async Task<ActionResult<Renting>> ChangeRenting([FromBody] ChangeRentingRequest request)
        {
            var errors = 0;
            if (RentingExists(request.Id))
            {
                Renting renting = _context.Rentings.Find(request.Id);
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
                    await _context.SaveChangesAsync();

                    //Ukončení pokud vše vráceno
                    if (!_context.RentingItems.Any(x => x.RentingId == request.Id && x.Returned == false))
                    {
                        renting.State = RentingState.Ended;
                        renting.End = DateTime.Now;
                    }

                    //Historie - neošetřuji zda uživatel existuje
                    var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                    RentingHistoryLog log = new RentingHistoryLog
                    {
                        RentingId = request.Id,
                        UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                        ChangedTime = DateTime.Now,
                        Action = Action.PickedUpItems
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
        /// Zrušení neuskutečněné výpůjčky
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Renting>> CancelRenting(int id)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (RentingExists(id))
            {
                Renting renting = _context.Rentings.Find(id);

                //Odebrání itemů
                foreach (var item in _context.RentingItems.Include(i => i.Item).Where(x => x.RentingId == id))
                {
                    _context.Entry(item).State = EntityState.Modified;
                    _context.RentingItems.Remove(item);
                }

                _context.Remove(renting);

                //Historie - neošetřuji zda uživatel existuje
                RentingHistoryLog log = new RentingHistoryLog
                {
                    RentingId = id,
                    UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                    ChangedTime = DateTime.Now,
                    Action = Action.Cancel
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
        /// Vypíše všechny výpůjčky + filtrování podle stavu (nepovinné)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Renting>>> GetAllRentings(RentingState? state)
        {
            IEnumerable<Renting> rentings = Enumerable.Empty<Renting>();
            if (state != null)
            {
                rentings = _context.Rentings.Include(o => o.Owner).Where(x => x.State == state).AsEnumerable();
            }
            else
            {
                rentings = _context.Rentings.Include(o => o.Owner).AsEnumerable();
            }
            return Ok(rentings);
        }

        /// <summary>
        /// Vypíše všechny výpůjčky daného uživatele
        /// </summary>
        [HttpGet("RentingsByUser/{id}")]
        public async Task<ActionResult<IEnumerable<Renting>>> GetRentings(string id)
        {
            if (_context.Users.Any(x => x.OauthId == id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                IEnumerable<Renting> rentings = _context.Rentings.Where(y => y.OwnerId == user.Id).AsEnumerable();
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
        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<Renting>> ActivateRenting(int id)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            var error = 0;
            if (RentingExists(id) && _context.Rentings.SingleOrDefault(x => x.Id == id).State == RentingState.WillStart)
            {
                Renting renting = _context.Rentings.Find(id);
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
                    //Historie - neošetřuji zda uživatel existuje
                    RentingHistoryLog log = new RentingHistoryLog
                    {
                        RentingId = id,
                        UserId = _context.Users.SingleOrDefault(x => x.OauthId == userId).Id,
                        ChangedTime = DateTime.Now,
                        Action = Action.Rented
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
                    Start = item.Start,
                    End = item.End,
                    Owner = item.Owner.FullName
                });
            }

            return Ok(dates);
        }

        private bool RentingExists(int id)
        {
            return _context.Rentings.Any(x => x.Id == id);
        }
    }
}
