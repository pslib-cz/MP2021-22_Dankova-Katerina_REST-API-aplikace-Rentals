using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals.Context;
using Rentals.Models.DatabaseModel;
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
        [HttpPost("AddNewRenting")]
        public async Task<ActionResult<Renting>> AddNewRenting(
            [BindRequired] string owner,
            [BindRequired] string approver,
            [BindRequired] List<int> items,
            [BindRequired] DateTime start,
            [BindRequired] DateTime end,
            string note = null)
        {
            var error = 0;
            Renting renting = new Renting
            {
                Start = start,
                End = end,
                OwnerId = owner,
                ApproverId = approver,
                Note = note,
            };

            //Stav
            if ((DateTime.Compare(start, DateTime.Now) > 0))
            {
                renting.State = RentingState.WillStart;
            }
            else
            {
                renting.State = RentingState.InProgress;
            }

            _context.Rentings.Add(renting);
            await _context.SaveChangesAsync();

            //Přidání itemů
            foreach (var item in items)
            {
                if (_context.Items.Find(item).State == ItemState.Available && _context.Items.Find(item).IsDeleted == false)
                {
                    var rentingItem = new RentingItem { ItemId = item, RentingId = renting.Id };
                    _context.RentingItems.Add(rentingItem);

                    //Vložení itemu do uživatelova inventáře
                    var inventoryitem = new InventoryItem { ItemId = item, UserId = owner };
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
                return Ok();
            }
            else
            {
                //{error} itemů nelze vypůjčit
                return Forbid(error.ToString());
            }

        }

        //Úprava výpůjčky
        [HttpPut("ChangeRenting/{id}")]
        public async Task<ActionResult<Renting>> ChangeRenting(
            int id, 
            DateTime end = default(DateTime),
            string note = null, 
            RentingState state = 0,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] List<int> itemsReturned = null)
        {
            var errors = 0;
            if (RentingExists(id))
            {
                Renting renting = _context.Rentings.Find(id);
                if (end != default(DateTime))
                {
                    renting.End = end;
                }
                if (note != null)
                {
                    renting.Note = note;
                }
                if (state != 0)
                {
                    renting.State = state;
                }

                if (itemsReturned != null)
                {
                    foreach (var item in itemsReturned)
                    {
                        //Pokud se ve výpůjčce nachází - vrácení itemu
                        if (_context.RentingItems.Any(x => x.RentingId == id && x.ItemId == item))
                        {
                            _context.RentingItems.Remove(_context.RentingItems.Single(x => x.RentingId == id && x.ItemId == item));

                            //Odebrání itemu z uživatelova inventáře
                            _context.InventoryItems.Remove(_context.InventoryItems.Single(x => x.UserId == renting.OwnerId && x.ItemId == item));


                            if (!_context.RentingItems.Any(x => x.RentingId == id))
                            {
                                renting.State = RentingState.Ended;
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
                    return Ok();
                }
                else
                {
                    //Špatné itemy? počet: {errors}
                    return Forbid(errors.ToString());
                }
            }
            else
            {
                return NotFound();
            }
        }

        //Zrušení výpůjčky
        [HttpDelete("CancelRenting/{id}")]
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
                    //_context.RentingItems.Remove(item);

                    //odebrání z inventáře
                    _context.InventoryItems.Remove(_context.InventoryItems.Single(y => y.ItemId == item.ItemId && y.UserId == renting.OwnerId));
                }

                renting.State = RentingState.Cancelled;
                _context.Entry(renting).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //Všechny výpůjčky + filtrace dle stavu + hledání dle půjčujícího       ***
        [HttpPost("GetAllRentings")]
        public ActionResult GetAllRentings([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] List<RentingState> states = null, string term = null)
        {
            List<Renting> rentings = new();
            //Pokud jsou vybrané filtrace
            if (states != null)
            {
                //pro každou v seznamu
                foreach (var state in states)
                {
                    //přidá item s daným filtrem, pokud zde již není 
                    foreach (var item in _context.Rentings.Include(o => o.Owner).Where(x => x.State == state))
                    {
                        if (!rentings.Contains(item))
                        {
                            rentings.Add(item);
                        }
                    }
                }
            }
            //Jinak všechny
            else
            {
                rentings = _context.Rentings.Include(o => o.Owner).ToList();
            }

            //Vyhledávání ve jménu i příjmení
            if (term != null)
            {
                rentings = rentings.Where(x => x.Owner.FirstName.ToLower().Contains(term.ToLower()) && x.Owner.LastName.ToLower().Contains(term.ToLower())).ToList();
            }
            return Ok(rentings);
        }

        //Všechny výpůjčky uživatele
        [HttpGet("GetRentingsByUser/{id}")]
        public ActionResult GetRentings(string id)
        {
            if (_context.Users.Any(x => x.Id == id))
            {
                List<Renting> rentings = _context.Rentings.Where(y => y.OwnerId == id).ToList();
                return Ok(rentings);
            }
            else
            {
                return NotFound();
            }
        }

        //Data všechn výpůjček
        [HttpGet("GetRentingDates")]
        public ActionResult GetRentingDates()
        {
            List<Renting> rentingDates = _context.Rentings.Select(x => new Renting { Start = x.Start, End = x.End }).ToList();
            return Ok(rentingDates);
        }        

        private bool RentingExists(int id)
        {
            return _context.Rentings.Any(x => x.Id == id);
        }
    }
}
