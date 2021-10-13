using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals.Context;
using Rentals.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        public ItemController(RentalsDbContext context)
        {
            _context = context;
        }
        // *** znamená testováno a funkční


        //Přidat item       ***
        [HttpPost("AddNewItem")]
        public async Task<ActionResult<Item>> AddNewItem(
            [BindRequired] IFormFile file,
            [BindRequired] string name,
            string description = null,
            string note = null)
        {
            string filePath;
            //Uloží obrázek
            if (file != null)
            {
                filePath = Path.Combine("Images", file.FileName);
                if (!_context.Items.Any(x => x.Img == filePath))
                {
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            else
            {
                filePath = null;
            }

            Item Item = new Item
            {
                Name = name,
                Description = description,
                Note = note,
                Img = filePath,
                State = ItemState.Available
            };
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Změnit parametry itemu
        [HttpPut("ChangeItem/{id}")]
        public async Task<ActionResult<Item>> ChangeItem(
            int id,
            //IFormFile file = null,
            string name = null,
            string description = null,
            string note = null,
            int? accesoryId = null,
            ItemState state = 0,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] List<int> categories = null)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                //Pokud přiložen obrázek
                //if (file != null)
                //{
                //    //Pokud item už má obrázek smaže ho
                //    if (item.Img != null)
                //    {
                //        Directory.Delete(item.Img);
                //    }

                //    //Uloží obrázek
                //    string filePath = Path.Combine("Images", file.FileName);
                //    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await file.CopyToAsync(fileStream);
                //    }
                //    item.Img = filePath;
                //}

                //změna nastavených parametrů
                item.AccessoryId = accesoryId;
                if (description != null)
                {
                    item.Description = description;
                }
                if (name != null)
                {
                    item.Name = name;
                }
                if (note != null)
                {
                    item.Note = note;
                }
                if (state != 0)
                {
                    item.State = state;
                }

                _context.Entry(item).State = EntityState.Modified;

                List<CategoryItem> oldCategories = _context.CategoryItems.Where(x => x.ItemId == id).ToList();

                //Smazání starých nově neobsahujících kategorií
                foreach (var cat in oldCategories)
                {
                    if (!categories.Contains(cat.CategoryId))
                    {
                        _context.CategoryItems.Remove(cat);
                    }
                }

                //Přidání nových kategorií
                foreach (var cat in categories)
                {
                    //Pokud je již neobsahují
                    if (!oldCategories.Any(x => x.CategoryId == cat))
                    {
                        CategoryItem categoryItem = new CategoryItem { ItemId = id, CategoryId = cat };
                        _context.CategoryItems.Add(categoryItem);
                    }
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        //"Smazat" item     ***
        [HttpPut("DeleteItem/{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                item.IsDeleted = true;
                _context.Entry(item).State = EntityState.Modified;


                //Smazání všech záznamů

                //foreach (var del in _context.CartItems.Where(x => x.ItemId == id))
                //{
                //    _context.CartItems.Remove(del);
                //}

                //foreach (var del in _context.CategoryItems.Where(x => x.ItemId == id))
                //{
                //    _context.CategoryItems.Remove(del);
                //}

                //foreach (var del in _context.FavouriteItems.Where(x => x.ItemId == id))
                //{
                //    _context.FavouriteItems.Remove(del);
                //}

                //foreach (var del in _context.InventoryItems.Where(x => x.ItemId == id))
                //{
                //    _context.InventoryItems.Remove(del);
                //}

                //foreach (var del in _context.RentingItems.Where(x => x.ItemId == id))
                //{
                //    _context.RentingItems.Remove(del);
                //}

                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        //Všechny "smazané" itemy       ***
        [HttpGet("Deleted")]
        public ActionResult DeletedItems()
        {
            List<Item> items = _context.Items.Where(x => x.IsDeleted == true).ToList();
            return Ok(items);
        }

        //Detail itemu      ***
        [HttpGet("ItemDetail/{id}")]
        public ActionResult GetItem(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                return Ok(item);
            }
            else
            {
                return NotFound();
            }

        }

        //Obrázek itemu     ***
        [HttpGet("img/{id}")]
        public ActionResult GetItemImg(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                var path = _context.Items.Find(id).Img;

                //Exituje fyzicky soubor?
                try
                {
                    FileStream img = new FileStream(path, FileMode.Open);
                    return Ok(img);
                }
                catch (Exception)
                {
                    return Forbid();
                }

            }
            else
            {
                return NotFound();
            }

        }

        //List kategorií        ***
        [HttpGet("Categories")]
        public ActionResult GetCategoryList()
        {
            List<Category> categories = new();
            categories = _context.Categories.ToList();
            return Ok(categories);
        }

        //Všechny předměty + filtrování + hledání       ***
        [HttpPost("Items")]
        public ActionResult<List<Item>> GetAllItems(string term = "", [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] List<int> categories = null)
        {
            List<Item> Items = new();

            //Pokud vybrané kategorie
            if (categories != null)
            {
                //pro všechny kategorie z parametru
                foreach (var cat in categories)
                {
                    //Přidá do seznamu všechny předměty obsahující danou kategorii
                    foreach (var item in _context.CategoryItems.Where(x => x.Item.IsDeleted == false && x.CategoryId == cat))
                    {
                        var itemek = _context.Items.Find(item.ItemId);
                        //Pokud ho již neobsahuje
                        if (!Items.Contains(itemek))
                        {
                            Items.Add(itemek);
                        }
                    }
                }
            }
            //Bez kategorií
            else
            {
                Items = _context.Items.Where(x => x.IsDeleted == false).ToList();
            }

            //Vyhledávání
            Items = Items.Where(x => x.Name.ToLower().Contains(term.ToLower())).ToList();
            return Ok(Items);
        }

        //Kompatibilní příslušenství        ***
        [HttpGet("Accesories/{id}")]
        public ActionResult GetAccesories(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                List<Item> Items = _context.Items.Where(x => x.AccessoryId == id).ToList();
                return Ok(Items);
            }
            else
            {
                return NotFound();
            }
        }

        //Kategorie itemu (vytvoření seznamu na změnu itemů)        ***
        [HttpGet("Categories/{id}")]
        public ActionResult GetCategories(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                List<Category> categories = _context.CategoryItems.Where(x => x.ItemId == id).Select(y => y.Category).ToList();
                return Ok(categories);
            }
            else
            {
                return NotFound();
            }

        }


        /* -- Kategorie --*/

        //Nová kategorie + přidání předmětů // Pokud v pořádku Ok, jinak vrátí počet neúspěšných itemů      ***
        [HttpPost("AddNewCategory")]
        public async Task<ActionResult<Category>> AddNewCategory([BindRequired] string name, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] List<int> items = null)
        {
            Category category = new Category { Name = name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            var errors = 0;

            //Přidání itemů k nově vytvořené kategorii
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (ItemExists(item) && !IsDeleted(item))
                    {
                        CategoryItem categoryItem = new CategoryItem { ItemId = item, CategoryId = category.Id };
                        _context.CategoryItems.Add(categoryItem);
                    }

                    //Pokud nenalezeno / smazáno
                    else
                    {
                        errors++;
                    }
                }
            }

            await _context.SaveChangesAsync();
            if (errors == 0)
            {
                return Ok();
            }
            else
            {
                //Nepodařilo se přidat {errors} předmětů.
                return Ok(errors);
            }

        }

        //Změna jména kategorie     ***
        [HttpPut("ChangeCategory/{id}")]
        public async Task<ActionResult<Category>> ChangeCategory(int id, [BindRequired] string name)
        {
            if (CategoryExists(id))
            {
                Category category = _context.Categories.Find(id);
                category.Name = name;
                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }

        //Smazání kategorie     ***
        [HttpDelete("RemoveCategory/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (CategoryExists(id))
            {
                Category category = _context.Categories.Find(id);

                //odebrat itemy z dané kategorie

                _context.Remove(category);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(x => x.Id == id);
        }
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(x => x.Id == id);
        }
        private bool IsDeleted(int id)
        {
            return _context.Items.Single(x => x.Id == id).IsDeleted;
        }
    }
}
