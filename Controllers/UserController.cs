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
    public class UserController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        public UserController(RentalsDbContext context)
        {
            _context = context;
        }

        // >>> opraveno
        // *** testováno a funkční


        //Přidat uživatele      >>>
        [HttpPost("User")]
        public async Task<ActionResult<User>> NewUser(UserRequest user)
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
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(newUser);
            }
            else
            {
                return Forbid("Uživatel již existuje");
            }

        }

        //Smazat uživatele      >>>
        [HttpDelete("User/{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        //Změnit uživatele      >>>
        [HttpPut("User")]
        public async Task<ActionResult<User>> ChangeUser(UserRequest user)
        {
            if (UserExists(user.OauthId))
            {
                User modifiedUser = _context.Users.SingleOrDefault(x => x.OauthId == user.OauthId);
                modifiedUser.FirstName = user.FirstName;
                modifiedUser.LastName = user.LastName;
                modifiedUser.Username = user.Username;
                _context.Entry(modifiedUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok(modifiedUser);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        //Košík     >>>
        [HttpGet("Cart/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetUserCart(string id)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                IEnumerable<Item> items = _context.CartItems.Where(x => x.UserId == user.Id).Select(y => y.Item).AsEnumerable();
                return Ok(items);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        //Přidat do košíku      >>>
        [HttpPost("Cart")]
        public async Task<ActionResult<HttpGetAttribute>> AddToCart(ItemToUserRequest request)
        {
            if (UserExists(request.OauthId) && _context.Items.Any(x => x.Id == request.Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (!_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    CartItem cartItem = new CartItem { UserId = user.Id, ItemId = request.Item };
                    _context.CartItems.Add(cartItem);
                    await _context.SaveChangesAsync();
                    return Ok(GetUserCart(user.OauthId));
                }
                else
                {
                    return Forbid("Tento předmět již máš v košíku");
                }
            }
            else
            {
                return NotFound("Uživatel nebo předmět neexistuje");
            }
        }

        //Odebrat z košíku      >>>
        [HttpDelete("Cart")]
        public async Task<ActionResult<HttpGetAttribute>> RemoveItemfromCart(ItemToUserRequest request)
        {
            if (UserExists(request.OauthId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    CartItem cartItem = _context.CartItems.Single(x => x.UserId == user.Id && x.ItemId == request.Item);
                    _context.CartItems.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return Ok(GetUserCart(user.OauthId));
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

        //Všechny oblíbené položky + filtrace       >>>
        [HttpGet("Favourites")]
        public async Task<ActionResult<IEnumerable<Item>>> GetFavourites(FavouritesRequest request)
        {
            if (UserExists(request.OauthId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                Category category = _context.Categories.Find(request.Category);
                IEnumerable<Item> Favourites = _context.FavouriteItems.Where(x => x.Item.IsDeleted == false && x.UserId == user.Id).Select(y => y.Item).AsEnumerable();
                IEnumerable<Item> List = Enumerable.Empty<Item>();
                if (request.Category != null)
                {
                    foreach (var item in Favourites)
                    {
                        if (_context.CategoryItems.Any(x => x.ItemId == item.Id && x.CategoryId == request.Category))
                        {
                            List.Append(item);
                        }
                    }
                }
                else
                {
                    List = Favourites;
                }

                return Ok(List);
            }
            else
            {
                return NotFound("Tento uživatel neexistuje");
            }
        }

        //Přidat do oblíbených      >>>
        [HttpPost("Favourites")]
        public async Task<ActionResult<HttpGetAttribute>> AddToFavourites(ItemToUserRequest request)
        {
            if (UserExists(request.OauthId) && _context.Items.Any(x => x.Id == request.Item && x.IsDeleted == false))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (!_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    FavouriteItem favouriteItem = new FavouriteItem { UserId = user.Id, ItemId = request.Item };
                    _context.FavouriteItems.Add(favouriteItem);
                    await _context.SaveChangesAsync();
                    return Ok(GetFavourites(new FavouritesRequest { OauthId = user.OauthId }));
                }
                else
                {
                    return Forbid("Tento předmět máš již v oblíbených");
                }
            }
            else
            {
                return NotFound("Tento uživatel nebo předmět neexistuje");
            }
        }

        //Odebrat z oblíbených      >>>
        [HttpDelete("Favourites")]
        public async Task<ActionResult<HttpGetAttribute>> RemoveItemfromFavourites(ItemToUserRequest request)
        {
            if (UserExists(request.OauthId))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == request.OauthId);
                if (_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == request.Item))
                {
                    FavouriteItem favourite = _context.FavouriteItems.Single(x => x.UserId == user.Id && x.ItemId == request.Item);
                    _context.FavouriteItems.Remove(favourite);
                    await _context.SaveChangesAsync();
                    return Ok(GetFavourites(new FavouritesRequest { OauthId = user.OauthId }));
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

        //Všichni uživatelé     >>>
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            IEnumerable<User> users = _context.Users.AsEnumerable();
            return Ok(users);
        }

        //Inventář      >>>
        [HttpGet("Inventory/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetInventoryByUser(string id)
        {
            if (UserExists(id))
            {
                User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
                IEnumerable<Item> items = _context.InventoryItems.Where(x => x.UserId == user.Id).Select(y => y.Item).AsEnumerable();
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
