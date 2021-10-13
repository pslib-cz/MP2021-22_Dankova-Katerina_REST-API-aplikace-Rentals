using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals.Context;
using Rentals.Models.DatabaseModel;
using Rentals.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentingController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        public RentingController(RentalsDbContext context)
        {
            _context = context;
        }
        
        // *** znamená testováno a funkční


        //Přidat Výpůjčku
        [HttpPost("Renting")]
        public async Task<ActionResult<Renting>> AddNewRenting(RentingRequest renting)
        {
            var error = 0;
            if (_context.Users.Any(x => x.OauthId == renting.Owner) && _context.Users.Any(x => x.OauthId == renting.Approver))
            {
                User owner = _context.Users.SingleOrDefault(x => x.OauthId == renting.Owner);
                User approver = _context.Users.SingleOrDefault(x => x.OauthId == renting.Approver);
                Renting newRenting = new Renting
                {
                    Start = renting.Start,
                    End = renting.End,
                    OwnerId = owner.Id,
                    ApproverId = approver.Id,
                    Note = renting.Note,
                };

                //Stav
                if ((DateTime.Compare(renting.Start, DateTime.Now) > 0))
                {
                    newRenting.State = RentingState.WillStart;
                }
                else
                {
                    newRenting.State = RentingState.InProgress;
                }

                _context.Rentings.Add(newRenting);
                //await _context.SaveChangesAsync();

                //Přidání itemů
                foreach (var item in renting.Items)
                {
                    if (_context.Items.Any(x => x.Id == item) && _context.Items.Find(item).State == ItemState.Available && _context.Items.Find(item).IsDeleted == false)
                    {
                        var rentingItem = new RentingItem { ItemId = item, RentingId = newRenting.Id }; // dostane se sem id i bez řádku 57?
                        _context.RentingItems.Add(rentingItem);

                        //Vložení itemu do uživatelova inventáře
                        var inventoryitem = new InventoryItem { ItemId = item, UserId = owner.Id };
                        _context.InventoryItems.Add(inventoryitem);

                        //Nastavení stavu itemu na půjčený
                        var rentedItem = _context.Items.Find(item);
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
                    return Ok(newRenting);
                }
                else
                {
                    //{error} itemů nelze vypůjčit
                    return Forbid($"Vyskytlo se {error} chyb");
                }
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        //Úprava výpůjčky
        [HttpPut("Renting")]
        public async Task<ActionResult<Renting>> ChangeRenting(ChangeRentingRequest request)
        {
            var errors = 0;
            if (RentingExists(request.Id))
            {
                Renting renting = _context.Rentings.Find(request.Id);
                renting.End = request.End;
                renting.Note = request.Note;

                if (request.ReturnedItems != null)
                {
                    foreach (var item in request.ReturnedItems)
                    {
                        //Pokud se ve výpůjčce nachází - vrácení itemu
                        if (_context.RentingItems.Any(x => x.RentingId == request.Id && x.ItemId == item) && _context.InventoryItems.Any(x => x.UserId == renting.OwnerId && x.ItemId == item))
                        {
                            _context.RentingItems.Remove(_context.RentingItems.Single(x => x.RentingId == request.Id && x.ItemId == item));

                            //Odebrání itemu z uživatelova inventáře
                            _context.InventoryItems.Remove(_context.InventoryItems.Single(x => x.UserId == renting.OwnerId && x.ItemId == item));

                            if (!_context.RentingItems.Any(x => x.RentingId == request.Id))
                            {
                                renting.State = RentingState.Ended;
                                renting.End = DateTime.Now;
                            }

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
                    await _context.SaveChangesAsync();
                    return Ok(renting);
                }
                else
                {
                    //Špatné itemy? počet: {errors}
                    return Forbid($"Vyskytlo se {errors} chyb");
                }
            }
            else
            {
                return NotFound("Tato výpůjčka neexistuje");
            }
        }

        //Zrušení výpůjčky
        [HttpDelete("Renting/{id}")]
        public async Task<ActionResult<Renting>> CancelRenting(int id)
        {
            if (RentingExists(id))
            {
                Renting renting = _context.Rentings.Find(id);

                //Smazání záznamů - dostupné itemy
                foreach (var item in _context.RentingItems.Include(i => i.Item).Where(x => x.RentingId == id))
                {
                    item.Item.State = ItemState.Available;
                    _context.Entry(item).State = EntityState.Modified;
                    _context.RentingItems.Remove(item);

                    //odebrání z inventáře
                    _context.InventoryItems.Remove(_context.InventoryItems.Single(y => y.ItemId == item.ItemId && y.UserId == renting.OwnerId));
                }

                renting.State = RentingState.Cancelled;
                _context.Entry(renting).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Ok(renting);
            }
            else
            {
                return NotFound("Tato výpůjčka neexistuje");
            }
        }

        //Všechny výpůjčky + filtrace dle stavu
        [HttpGet("Renting")]
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

        //Všechny výpůjčky uživatele 
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
                return NotFound("Tento uživatel neexistuje");
            }
        }   

        private bool RentingExists(int id)
        {
            return _context.Rentings.Any(x => x.Id == id);
        }
    }
}
