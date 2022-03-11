using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models.DatabaseModel;
using Rentals_API_NET6.Models.InputModel;
using Rentals_API_NET6.Models.OutputModel;
using System.Security.Claims;

namespace Rentals_API_NET6.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        public ItemController(RentalsDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Vytvoření nového předmětu
        /// </summary>
        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public async Task<ActionResult<Item>> AddNewItem([FromBody] ItemRequest request)
        {
            Item Item = new Item
            {
                Name = request.Name,
                Description = request.Description,
                Note = request.Note,
                State = ItemState.Available,
                Img = request.Img == null ? "Placeholder.bmp" : request.Img
            };

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();
            return Ok(Item);
        }

        /// <summary>
        /// Úprava předmětu
        /// </summary>
        //[Authorize(Policy = "Employee")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<Item>> ChangeItem(int id, [FromBody] JsonPatchDocument<Item> patch)
        {
            var isAdmin = await _authorizationService.AuthorizeAsync(User, "Administrator");
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                patch.ApplyTo(item, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                foreach (var op in patch.Operations)
                {
                    if (op.path.ToLower() == "/id" || op.path.ToLower() == "/isdeleted")
                    {
                        return BadRequest();
                    }
                    else if ((op.path.ToLower() != "note" || op.path.ToLower() != "description") && !isAdmin.Succeeded)
                    {
                        return Unauthorized();
                    }
                }

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Nastaví předmět na smazaný
        /// </summary>
        [Authorize(Policy = "Administrator")]
        [HttpPatch("Delete/{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id, [FromBody] JsonPatchDocument<Item> patch)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                patch.ApplyTo(item, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (patch.Operations.Count > 1 || patch.Operations[0].path.ToLower() != "/isdeleted" || (string)patch.Operations[0].value != "true")
                {
                    return BadRequest();
                }

                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vypíše všechny předměty, které jsou smazané (pro případ navrácení)
        /// </summary>
        //[Authorize(Policy = "Employee")]
        [HttpGet("Deleted")]
        public async Task<ActionResult<IEnumerable<Item>>> DeletedItems()
        {
            IEnumerable<Item> items = _context.Items.Where(x => x.IsDeleted == true).AsEnumerable();
            return Ok(items);
        }

        /// <summary>
        /// Navrátí smazaný předmět
        /// </summary>
        [Authorize(Policy = "Administrator")]
        [HttpPut("Restore/{id}")]
        public async Task<ActionResult<Item>> RestoreItem(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && item.IsDeleted)
            {
                item.IsDeleted = false;
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vypíše detaily předmětu
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                return Ok(item);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Získá náhled předmětu
        /// </summary>
        [HttpGet("Img/{id}")]
        public async Task<ActionResult<FileStream>> GetItemImg(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                var path = "";

                if (item.Img == null)
                {
                    path = "Images/Placeholder.jpg";
                }
                else
                {
                    path = $"Images/{item.Img}";
                }

                //Exituje fyzicky soubor?
                try
                {
                    FileStream img = new FileStream(path, FileMode.Open);
                    return Ok(img);
                }
                catch (Exception)
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
        /// Vypíše všechny předměty
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            IEnumerable<Item> Items = _context.Items.Where(x => !x.IsDeleted).AsEnumerable();
            return Ok(Items);
        }

        /// <summary>
        /// Vypíše všechny předměty, které jsou příslušenstvím k danému předmětu
        /// </summary>
        [HttpGet("Accesories/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAccesories(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                IEnumerable<Item> Items = _context.AccessoryItems.Where(x => x.ItemId == id && !x.Item.IsDeleted).Select(y => y.Accessory).AsEnumerable();
                return Ok(Items);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Přidá (nevybranné odebere) k předmětu příslušenství
        /// </summary>
        [Authorize(Policy = "Administrator")]
        [HttpPatch("Accesories")]
        public async Task<ActionResult<Item>> PutAccesories([FromBody] ItemAccesoriesRequest request)
        {
            var errors = 0;
            Item itemForAccesory = _context.Items.SingleOrDefault(x => x.Id == request.Id);
            if (itemForAccesory != null && !itemForAccesory.IsDeleted)
            {
                foreach (var item in _context.AccessoryItems.Where(x => x.ItemId == itemForAccesory.Id).ToList())
                {
                    _context.Remove(item);
                }
                foreach (var item in request.Items)
                {
                    Item tempItem = _context.Items.SingleOrDefault(x => x.Id == item);
                    if (tempItem != null && !tempItem.IsDeleted)
                    {
                        _context.AccessoryItems.Add(new AccessoryItem { ItemId = itemForAccesory.Id, AccessoryId = item });
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
                    return BadRequest();
                }
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Kontroluje zda má přihlášený uživatel daný předmět v oblíbených
        /// </summary>
        [HttpGet("IsFavourite/{id}")]
        public async Task<ActionResult<bool>> IsItemFavourite(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
                var result = _context.FavouriteItems.SingleOrDefault(x => x.User.OauthId == userId && x.ItemId == id) != null;
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Vrací data, kdy je předmět vypůjčen
        /// </summary>
        [HttpGet("{id}/Dates")]
        public async Task<ActionResult<List<DatesResponse>>> GetItemDates(int id)
        {
            Item item = _context.Items.SingleOrDefault(x => x.Id == id);
            if (item != null && !item.IsDeleted)
            {
                List<DatesResponse> dates = new();
                foreach (var i in _context.RentingItems.Where(x => x.ItemId == id).Select(x => x.Renting))
                {
                    dates.Add(new DatesResponse
                    {
                        Id = i.Id,
                        State = i.State,
                        Start = i.Start,
                        End = i.End,
                        Title = _context.Users.SingleOrDefault(x => x.Id == i.OwnerId).FullName
                    });
                }
                return Ok(dates);
            }
            else
            {
                return NotFound();
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
        [Authorize(Policy = "Administrator")]
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
        [Authorize(Policy = "Administrator")]
        [HttpPatch("Category")]
        public async Task<ActionResult<Category>> ChangeCategory(int id, [FromBody] JsonPatchDocument<Category> patch)
        {
            Category category = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (category != null)
            {
                patch.ApplyTo(category, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (patch.Operations.Count > 1 || patch.Operations[0].path.ToLower() != "/name")
                {
                    return BadRequest();
                }

                _context.Entry(category).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Smaže kategorii
        /// </summary>
        [Authorize(Policy = "Administrator")]
        [HttpDelete("Category/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            Category category = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (category != null)
            {
                //odebrat itemy z dané kategorie
                foreach (var item in _context.Items.Where(x => x.CategoryId == id))
                {
                    item.CategoryId = null;
                }

                _context.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
