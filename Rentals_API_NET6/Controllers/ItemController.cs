using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models.DatabaseModel;
using Rentals_API_NET6.Models.InputModel;

namespace Rentals_API_NET6.Controllers
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

        /// <summary>
        /// Vytvoření nového předmětu
        /// </summary>
        [HttpPost]
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

        /// <summary>
        /// Úprava předmětu
        /// </summary>
        [HttpPatch("{id}")]
        public async Task<ActionResult<Item>> ChangeItem(int id, [FromBody] JsonPatchDocument<Item> patch)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                patch.ApplyTo(item, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                foreach (var op in patch.Operations)
                {
                    if (op.path.ToLower() == "/id" || op.path.ToLower() == "/img" || op.path.ToLower() == "/isdeleted")
                    {
                        return BadRequest("Toto nelze změnit");
                    }
                    if (op.path.ToLower() == "/state" && (int)op.value > 2)
                    {
                        return BadRequest("Tento stav neexistuje");
                    }
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

        /// <summary>
        /// Nastaví předmět na smazaný
        /// </summary>
        [HttpPatch("Delete/{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id, [FromBody] JsonPatchDocument<Item> patch)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                Item item = _context.Items.Find(id);
                patch.ApplyTo(item, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (patch.Operations.Count > 1 || patch.Operations[0].path.ToLower() != "/isdeleted" || (string)patch.Operations[0].value != "true")
                {
                    return BadRequest("Toto zde nelze změnit");
                }

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

        /// <summary>
        /// Vypíše všechny předměty, které jsou smazané (pro případ navrácení)
        /// </summary>
        [HttpGet("Deleted")]
        public async Task<ActionResult<IEnumerable<Item>>> DeletedItems()
        {
            IEnumerable<Item> items = _context.Items.Where(x => x.IsDeleted == true).AsEnumerable();
            return Ok(items);
        }

        /// <summary>
        /// Vypíše detaily předmětu
        /// </summary>
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

        /// <summary>
        /// Získá náhled předmětu
        /// </summary>
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

        /// <summary>
        /// Vypíše všechny předměty + filtrování podle kategorie (nepovinné)
        /// </summary>
        [HttpGet]
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

        /// <summary>
        /// Vypíše všechny předměty, které jsou příslušenstvím k danému předmětu
        /// </summary>
        [HttpGet("Accesories/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAccesories(int id)
        {
            if (ItemExists(id) && !IsDeleted(id))
            {
                IEnumerable<Item> Items = _context.AccessoryItems.Where(x => x.ItemId == id && x.Item.IsDeleted == false).Select(y => y.Accessory).AsEnumerable();
                return Ok(Items);
            }
            else
            {
                return NotFound("Tento předmět neexistuje");
            }
        }

        /// <summary>
        /// Přidá (nevybranné odebere) k předmětu příslušenství
        /// </summary>
        [HttpPatch("Accesories")]
        public async Task<ActionResult<Item>> PutAccesories([FromBody] ItemAccesoriesRequest request)
        {
            var errors = 0;
            if (ItemExists(request.Id) && !IsDeleted(request.Id))
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
                        errors++;
                    }
                }
                if (errors == 0)
                {
                    await _context.SaveChangesAsync();
                    return Ok(_context.Items.Find(request.Id));
                }
                else
                {
                    return BadRequest($"U {errors} předmětů se vyskytla chyba");
                }
            }
            else
            {
                return BadRequest("Tento předmět neexistuje");
            }

        }

        /// <summary>
        /// Vypíše všechny kategorie, které předmět obsahuje
        /// </summary>
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

        /// <summary>
        /// Přidá (nevybranné odebere) k předmětu kategorie
        /// </summary>
        [HttpPut("Categories")]
        public async Task<ActionResult<Item>> PutCategories([FromBody] ItemPropertyRequest request)
        {
            var errors = 0;
            if (ItemExists(request.Id) && !IsDeleted(request.Id))
            {
                foreach (var item in _context.CategoryItems.Where(x => x.ItemId == request.Id).ToList())
                {
                    _context.Remove(item);
                }

                foreach (var item in request.Categories)
                {
                    if (_context.Categories.Any(x => x.Id == item))
                    {
                        _context.CategoryItems.Add(new CategoryItem { ItemId = request.Id, CategoryId = item });
                    }
                    else
                    {
                        errors++;
                    }
                }

                if (errors == 0)
                {
                    await _context.SaveChangesAsync();
                    return Ok(_context.Items.Find(request.Id));
                }
                else
                {
                    return BadRequest($"U {errors} kategorií se vyskytla chyba");
                }
            }
            else
            {
                return BadRequest("Tento předmět neexistuje");
            }
        }

        /* -- Kategorie --*/

        /// <summary>
        /// Vypíše všechny kategorie
        /// </summary>
        [HttpGet("Category")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryList()
        {
            IEnumerable<Category> categories = _context.Categories.AsEnumerable();
            return Ok(categories);
        }

        /// <summary>
        /// Vytvoří novou kategorii
        /// </summary>
        [HttpPost("Category")]
        public async Task<ActionResult<Category>> AddNewCategory(string name)
        {
            Category category = new Category { Name = name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        /// <summary>
        /// Úprava kategorie
        /// </summary>
        [HttpPatch("Category")]
        public async Task<ActionResult<Category>> ChangeCategory(int id, [FromBody] JsonPatchDocument<Category> patch)
        {
            if (CategoryExists(id))
            {
                Category category = _context.Categories.Find(id);
                patch.ApplyTo(category, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (patch.Operations.Count > 1 || patch.Operations[0].path.ToLower() != "/name")
                {
                    return BadRequest("Lze změnit pouze jméno");
                }

                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            else
            {
                return NotFound("Tato kategrie neexistuje");
            }
        }

        /// <summary>
        /// Smaže kategorii
        /// </summary>
        [HttpDelete("Category/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            if (CategoryExists(id))
            {
                Category category = _context.Categories.Find(id);

                //odebrat itemy z dané kategorie
                foreach (var item in _context.CategoryItems.Where(x => x.CategoryId == id).ToList())
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
