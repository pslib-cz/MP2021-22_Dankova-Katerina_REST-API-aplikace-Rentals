using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals.Context;
using Rentals.Models.DatabaseModel;
using Rentals.Models.InputModel;
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


        //Přidat item ***
        [HttpPost()]
        public async Task<ActionResult<Item>> AddNewItem([FromBody] ItemRequest request)
        {
            Item Item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Note = request.Note,
                State = ItemState.Available,
                Img = Path.Combine("Images", "Placeholder.bmp")
            };
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Ok(Item);
        }

        //Změnit náhled ***
        [HttpPut("{id}/Img")]
        public async Task<ActionResult<Item>> AddImg(int id, [FromBody] IFormFile file)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                //Smaže původní soubor
                if (item.Img != Path.Combine("Images", "Placeholder.bmp") && _context.Items.Where(x => x.Img == item.Img).ToList().Count <= 1)
                {
                    System.IO.File.Delete(item.Img);
                }
                if (file != null)
                {
                    //Uloží obrázek
                    if (!_context.Items.Any(x => x.Img == Path.Combine("Images", file.FileName)))
                    {
                        string filePath = Path.Combine("Images", file.FileName);
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        item.Img = filePath;
                    }
                    else
                    {
                        item.Img = Path.Combine("Images", file.FileName);
                    }
                }
                else
                {
                    item.Img = Path.Combine("Images", "Placeholder.bmp");
                }

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        //Změnit parametry itemu ***
        [HttpPut()]
        public async Task<ActionResult<Item>> ChangeItem([FromBody] ChangeItemRequest request)
        {
            if (ItemExists(request.Id) && !IsDeleted(request.Id))
            {
                Item item = _context.Items.Find(request.Id);
                item.Name = request.Name;
                item.Description = request.Description;
                item.Note = request.Note;

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        //"Smazat" item
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                item.IsDeleted = true;
                _context.Entry(item).State = EntityState.Modified;

                //Smazání všech záznamů
                foreach (var del in _context.CartItems.Where(x => x.ItemId == id))
                {
                    _context.CartItems.Remove(del);
                }
                foreach (var del in _context.CategoryItems.Where(x => x.ItemId == id))
                {
                    _context.CategoryItems.Remove(del);
                }

                foreach (var del in _context.FavouriteItems.Where(x => x.ItemId == id))
                {
                    _context.FavouriteItems.Remove(del);
                }

                foreach (var del in _context.InventoryItems.Where(x => x.ItemId == id))
                {
                    _context.InventoryItems.Remove(del);
                }

                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        //Všechny "smazané" itemy
        [HttpGet("Deleted")]
        public async Task<ActionResult<IEnumerable<Item>>> DeletedItems()
        {
            IEnumerable<Item> items = _context.Items.Where(x => x.IsDeleted == true).AsEnumerable();
            return Ok(items);
        }

        //Detail itemu
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                return Ok(item);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }

        }

        //Obrázek itemu
        [HttpGet("Img/{id}")]
        public async Task<ActionResult<FileStream>> GetItemImg(int id)
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
                    return BadRequest("Náhled nenalezen");
                }

            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }

        }

        //Všechny předměty + filtrování
        [HttpGet()]
        public async Task<ActionResult<List<Item>>> GetAllItems(int? category)
        {
            IEnumerable<Item> Items = _context.Items.Where(x => x.IsDeleted == false).AsEnumerable();
            List<Item> List = new();

            if (category != null)
            {
                foreach (var item in Items)
                {
                    if (_context.CategoryItems.Any(x => x.ItemId == item.Id && x.CategoryId == category))
                    {
                        List.Add(item);
                    }
                }
            }
            else
            {
                List = Items.ToList();
            }

            return Ok(List);
        }

        //Kompatibilní příslušenství (vytvoření seznamu na změnu itemů)
        [HttpGet("Accesories/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAccesories(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                IEnumerable<Item> Items = _context.AccessoryItems.Where(x => x.ItemId == id && x.Item.IsDeleted == false).Select(y => y.Item).AsEnumerable();
                return Ok(Items);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        //Nastavení příslušenství
        [HttpPut("Accesories")]
        public async Task<ActionResult<CreatedAtActionResult>> PutAccesories([FromBody] ItemPropertyRequest request)
        {
            foreach (var item in _context.AccessoryItems.Where(x => x.ItemId == request.Id).ToList())
            {
                _context.Remove(item);
            }
            foreach (var item in request.Items)
            {
                if (ItemExists(item) && !IsDeleted(item))
                {
                    _context.AccessoryItems.Add(new AccessoryItem { ItemId = request.Id, AccessoryId = item });
                }
                else
                {
                    return BadRequest("Tento předmět neexistuje");
                }
            }
            await _context.SaveChangesAsync();
            return Ok(CreatedAtAction("GetItem", request.Id));
        }

        //Kategorie itemu (vytvoření seznamu na změnu itemů) 
        [HttpGet("Categories/{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                IEnumerable<Category> categories = _context.CategoryItems.Where(x => x.ItemId == id).Select(y => y.Category).AsEnumerable();
                return Ok(categories);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        //Nastavení kategorií
        [HttpPut("Categories")]
        public async Task<ActionResult<CreatedAtActionResult>> PutCategories([FromBody] ItemPropertyRequest request)
        {
            foreach (var item in _context.CategoryItems.Where(x => x.ItemId == request.Id).ToList())
            {
                _context.Remove(item);
            }
            foreach (var item in request.Items)
            {
                if (ItemExists(item) && !IsDeleted(item))
                {
                    _context.CategoryItems.Add(new CategoryItem { ItemId = request.Id, CategoryId = item });
                }
                else
                {
                    return BadRequest("Tento předmět neexistuje");
                }
            }
            await _context.SaveChangesAsync();
            return Ok(CreatedAtAction("GetItem", request.Id));
        }

        /* -- Kategorie --*/

        //List kategorií 
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryList()
        {
            IEnumerable<Category> categories = _context.Categories.AsEnumerable();
            return Ok(categories);
        }

        //Nová kategorie 
        [HttpPost("Category")]
        public async Task<ActionResult<Category>> AddNewCategory(string name)
        {
            Category category = new Category { Name = name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        //Změna jména kategorie
        [HttpPut("Category")]
        public async Task<ActionResult<Category>> ChangeCategory([FromBody] Category category)
        {
            if (CategoryExists(category.Id))
            {
                Category oldCategory = _context.Categories.Find(category.Id);
                oldCategory.Name = category.Name;
                _context.Entry(oldCategory).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(oldCategory);
            }
            else
            {
                return NotFound("Tato kategrie neexistuje");
            }
        }

        //Smazání kategorie
        [HttpDelete("Category/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (CategoryExists(id))
            {
                Category category = _context.Categories.Find(id);

                //odebrat itemy z dané kategorie
                foreach (var item in _context.CategoryItems.Where(x => x.CategoryId == id).Select(y => y.Item).ToList())
                {
                    _context.Remove(item);
                }

                _context.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            else
            {
                return NotFound("Tato kategorie neexistuje");
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
