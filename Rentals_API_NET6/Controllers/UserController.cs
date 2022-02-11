using Microsoft.AspNetCore.Authorization;
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
using System.Security.Claims;
using System.Threading.Tasks;


namespace Rentals_API_NET6.Controllers
{
    [Authorize]
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
        /// Kontrola uživatele
        /// </summary>
        [Authorize]
        [HttpPost("User")]
        public async Task<ActionResult<User>> NewUser(UserRequest request)
        {
            var id = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (_context.Users.SingleOrDefault(x => x.OauthId == id) == null)
            {
                User user = new User
                {
                    OauthId = id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }


        /// <summary>
        /// Košík uživatele
        /// </summary>
        [HttpGet("Cart/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetUserCart()
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                IEnumerable<Item> items = _context.CartItems.Where(x => x.UserId == user.Id && x.Item.IsDeleted == false).Select(y => y.Item).AsEnumerable();
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Přidat do košíku uživatele
        /// </summary>
        [HttpPost("Cart")]
        public async Task<ActionResult<IEnumerable<Item>>> AddToCart(int Item)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            //if (UserExists(request.OauthId) && _context.Items.Any(x => x.Id == Item && x.IsDeleted == false))
            if (_context.Items.Any(x => x.Id == Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                if (!_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    CartItem cartItem = new CartItem { UserId = user.Id, ItemId = Item };
                    _context.CartItems.Add(cartItem);
                    await _context.SaveChangesAsync();
                    return await GetUserCart();
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
        /// Odebrat z košíku uživatele
        /// </summary>
        [HttpDelete("Cart")]
        public async Task<ActionResult<IEnumerable<Item>>> RemoveItemfromCart(int Item)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                if (_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    CartItem cartItem = _context.CartItems.Single(x => x.UserId == user.Id && x.ItemId == Item);
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return await GetUserCart();
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Vypíše všechny oblíbené položky uživatele + filtrování podle kategorie (nepovinné)
        /// </summary>
        [HttpGet("Favourites/{id}")]
        public async Task<ActionResult<List<Item>>> GetFavourites(int? filter)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
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
                return NotFound();
            }
        }

        /// <summary>
        /// Přidat do oblíbených předmětů uživatele
        /// </summary>
        [HttpPost("Favourites")]
        public async Task<ActionResult<List<Item>>> AddToFavourites(int Item)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId) && _context.Items.Any(x => x.Id == Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                if (!_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    FavouriteItem favouriteItem = new FavouriteItem { UserId = user.Id, ItemId = Item };
                    _context.FavouriteItems.Add(favouriteItem);
                    await _context.SaveChangesAsync();
                    return await GetFavourites(null);
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
        /// Přidat z oblíbených předmětů uživatele
        /// </summary>
        [HttpDelete("Favourites")]
        public async Task<ActionResult<List<Item>>> RemoveItemfromFavourites(int Item)
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                if (_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    FavouriteItem favourite = _context.FavouriteItems.Single(x => x.UserId == user.Id && x.ItemId == Item);
                    _context.FavouriteItems.Remove(favourite);
                    await _context.SaveChangesAsync();
                    return await GetFavourites(null);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Vypíše všechny uživatele
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<User> users = _context.Users.AsEnumerable();
            return Ok(users);
        }

        /// <summary>
        /// Vypíše inventář předmětů uživatele
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpGet("Inventory/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetInventoryByUser()
        {
            var userId = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            if (UserExists(userId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
                IEnumerable<Item> items = _context.InventoryItems.Where(x => x.UserId == user.Id && x.Item.IsDeleted == false).Select(y => y.Item).AsEnumerable();
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(x => x.OauthId == id);
        }
    }
}
