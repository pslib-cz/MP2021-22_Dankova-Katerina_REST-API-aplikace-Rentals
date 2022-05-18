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


namespace Rentals_API_NET6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RentalsDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger<RentingController> _logger;

        public UserController(RentalsDbContext context, IAuthorizationService authorizationService, ILogger<RentingController> logger)
        {
            _context = context;
            _authorizationService = authorizationService;
            _logger = logger;
        }

        /// <summary>
        /// Kontrola uživatele
        /// </summary>
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

                try
                {
                    //P-2021-2025(kz)A
                    if (request.Department != null)
                    {
                        var department = request.Department.Split("-");
                        user.Specialization = department[0];
                        user.Class = department[2].Last() != ')' ? department[2].Last().ToString() : null;
                        user.YearOfEntry = int.Parse(department[1]);
                    }
                    else
                    {
                        user.Specialization = "ZAM";
                    }
                }
                catch (Exception)
                {
                    user.Specialization = "ZAM";
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        /// <summary>
        /// Košík uživatele
        /// </summary>
        [HttpGet("Cart")]
        public async Task<ActionResult<IEnumerable<Item>>> GetUserCart()
        {
            var userId = UserId();
            User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (user != null)
            {
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
        [HttpPost("Cart/{Item}")]
        public async Task<ActionResult<IEnumerable<Item>>> AddToCart(int Item)
        {
            var userId = UserId();
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
        [HttpDelete("Cart/{Item}")]
        public async Task<ActionResult<IEnumerable<Item>>> RemoveItemfromCart(int Item)
        {
            var userId = UserId();
            User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (user != null)
            {
                if (_context.CartItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    CartItem cartItem = _context.CartItems.SingleOrDefault(x => x.UserId == user.Id && x.ItemId == Item);
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
        /// Vypíše všechny oblíbené položky uživatele
        /// </summary>
        [HttpGet("Favourites")]
        public async Task<ActionResult<List<Item>>> GetFavourites()
        {
            var userId = UserId();
            User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (user != null)
            {
                IEnumerable<Item> Favourites = _context.FavouriteItems.Where(x => x.Item.IsDeleted == false && x.UserId == user.Id).Select(y => y.Item).AsEnumerable();
                return Ok(Favourites);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Přidat do oblíbených předmětů uživatele
        /// </summary>
        [HttpPost("Favourites/{Item}")]
        public async Task<ActionResult<List<Item>>> AddToFavourites(int Item)
        {
            var userId = UserId();
            User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (user != null && _context.Items.SingleOrDefault(x => x.Id == Item && x.IsDeleted == false) != null)
            {
                if (!_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    FavouriteItem favouriteItem = new FavouriteItem { UserId = user.Id, ItemId = Item };
                    _context.FavouriteItems.Add(favouriteItem);
                    await _context.SaveChangesAsync();
                    return await GetFavourites();
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
        /// Odebrat z oblíbených předmětů uživatele
        /// </summary>
        [HttpDelete("Favourites/{Item}")]
        public async Task<ActionResult<List<Item>>> RemoveItemfromFavourites(int Item)
        {
            var userId = UserId();
            User user = _context.Users.SingleOrDefault(x => x.OauthId == userId);
            if (user != null)
            {
                if (_context.FavouriteItems.Any(x => x.UserId == user.Id && x.ItemId == Item))
                {
                    FavouriteItem favourite = _context.FavouriteItems.SingleOrDefault(x => x.UserId == user.Id && x.ItemId == Item);
                    _context.FavouriteItems.Remove(favourite);
                    await _context.SaveChangesAsync();
                    return await GetFavourites();
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
        public async Task<ActionResult<List<UserObject>>> GetAllUsers()
        {
            List<UserObject> users = _context.Users
                .Select(x => new UserObject {
                    Id = x.OauthId, 
                    Name = x.FullName, 
                    Username = x.Username, 
                    Class = x.Specialization + (DateTime.Now.Year - x.YearOfEntry).ToString() + x.Class
                }).ToList();
            return Ok(users);
        }

        /// <summary>
        /// Vypíše inventář předmětů uživatele
        /// </summary>
        [Authorize(Policy = "Employee")]
        [HttpGet("Inventory/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetInventoryByUser(string id)
        {
            User user = _context.Users.SingleOrDefault(x => x.OauthId == id);
            var isEmployee = await _authorizationService.AuthorizeAsync(User, "Employee");
            var userId = UserId();
            if (user != null || isEmployee.Succeeded)
            {
                IEnumerable<Item> items = _context.RentingItems.Where(x => x.Returned == false && x.Renting.OwnerId == user.Id && x.Renting.State == RentingState.InProgress).Select(x => x.Item);
                return Ok(items);
            }
            else
            {
                return NotFound();
            }
        }

        private string UserId()
        {
            return User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
        }
    }
}
