using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Rentals_API_NET6.Context;
using Rentals_API_NET6.Models.DatabaseModel;
using Rentals_API_NET6.Models.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Rentals_API_NET6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        public UserController(RentalsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Přidání uživatele - testovací účely
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<User>> NewUser([FromBody] UserRequest user)
        {
            if (!UserExists(user.OauthId))
            {
                User newUser = new User
                {
                    OauthId = user.OauthId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username
                };
                newUser.Trustfulness = 100;
                if (string.IsNullOrEmpty(newUser.OauthId)) newUser.OauthId = new Guid().ToString();
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(newUser);
            }
            else
            {
                return BadRequest("Uživatel již existuje");
            }

        }

        /// <summary>
        /// Košík uživatele
        /// </summary>
        [HttpGet("Cart/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetUserCart(string id)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                IEnumerable<Item> items = _context.CartItems.Where(x => x.UserId == user.Id && x.Item.IsDeleted == false).Select(y => y.Item).AsEnumerable();
                return Ok(items);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        /// <summary>
        /// Přidat do košíku uživatele
        /// </summary>
        [HttpPost("Cart")]
        public async Task<ActionResult<IEnumerable<Item>>> AddToCart([FromBody] ItemToUserRequest request)
        {
            if (UserExists(request.OauthId) && _context.Items.Any(x => x.Id == request.Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (!_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    CartItem cartItem = new CartItem { UserId = user.Id, ItemId = request.Item };
                    _context.CartItems.Add(cartItem);
                    await _context.SaveChangesAsync();
                    return await GetUserCart(user.OauthId);
                }
                else
                {
                    return BadRequest("Tento předmět již máš v košíku");
                }
            }
            else
            {
                return NotFound("Uživatel nebo předmět neexistuje");
            }
        }

        /// <summary>
        /// Odebrat z košíku uživatele
        /// </summary>
        [HttpDelete("Cart")]
        public async Task<ActionResult<IEnumerable<Item>>> RemoveItemfromCart([FromBody] ItemToUserRequest request)
        {
            if (UserExists(request.OauthId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    CartItem cartItem = _context.CartItems.Single(x => x.UserId == user.Id && x.ItemId == request.Item);
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return await GetUserCart(user.OauthId);
                }
                else
                {
                    return NotFound("Tento předmět není v košíku tohoto uživatele");
                }
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }

        }

        /// <summary>
        /// Vypíše všechny oblíbené položky uživatele + filtrování podle kategorie (nepovinné)
        /// </summary>
        [HttpGet("Favourites/{id}")]
        public async Task<ActionResult<List<Item>>> GetFavourites(string id, int? filter)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                Category category = _context.Categories.Find(filter);
                IEnumerable<Item> Favourites = _context.FavouriteItems.Where(x => x.Item.IsDeleted == false && x.UserId == user.Id).Select(y => y.Item).AsEnumerable();
                List<Item> List = new();
                if (filter != null)
                {
                    foreach (var item in Favourites)
                    {
                        if (_context.CategoryItems.Any(x => x.ItemId == item.Id && x.CategoryId == filter))
                        {
                            List.Add(item);
                        }
                    }
                }
                else
                {
                    List = Favourites.ToList();
                }

                return Ok(List);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        /// <summary>
        /// Přidat do oblíbených předmětů uživatele
        /// </summary>
        [HttpPost("Favourites")]
        public async Task<ActionResult<List<Item>>> AddToFavourites([FromBody] ItemToUserRequest request)
        {
            if (UserExists(request.OauthId) && _context.Items.Any(x => x.Id == request.Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (!_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    FavouriteItem favouriteItem = new FavouriteItem { UserId = user.Id, ItemId = request.Item };
                    _context.FavouriteItems.Add(favouriteItem);
                    await _context.SaveChangesAsync();
                    return await GetFavourites(user.OauthId, null);
                }
                else
                {
                    return BadRequest("Tento předmět máš již v oblíbených");
                }
            }
            else
            {
                return NotFound("Tento uživatel nebo předmět neexistuje");
            }
        }

        /// <summary>
        /// Přidat z oblíbených předmětů uživatele
        /// </summary>
        [HttpDelete("Favourites")]
        public async Task<ActionResult<List<Item>>> RemoveItemfromFavourites([FromBody] ItemToUserRequest request)
        {
            if (UserExists(request.OauthId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    FavouriteItem favourite = _context.FavouriteItems.Single(x => x.UserId == user.Id && x.ItemId == request.Item);
                    _context.FavouriteItems.Remove(favourite);
                    await _context.SaveChangesAsync();
                    return await GetFavourites(user.OauthId, null);
                }
                else
                {
                    return NotFound("Tento předmět nemáš v oblíbených");
                }
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }

        }

        /// <summary>
        /// Vypíše všechny uživatele
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<User> users = _context.Users.AsEnumerable();
            return Ok(users);
        }

        /// <summary>
        /// Vypíše inventář předmětů uživatele
        /// </summary>
        [HttpGet("Inventory/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetInventoryByUser(string id)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                IEnumerable<Item> items = _context.InventoryItems.Where(x => x.UserId == user.Id && x.Item.IsDeleted == false).Select(y => y.Item).AsEnumerable();
                return Ok(items);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(x => x.OauthId == id);
        }
    }
}
