using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameVerse.Models;
using GameVerse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace GameVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        private readonly IStringLocalizer<ShoppingCartsController> _localizer;

        public ShoppingCartsController(GameVerseDbContext context, IStringLocalizer<ShoppingCartsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/ShoppingCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetShoppingCarts()
        {
            return await _context.ShoppingCarts.ToListAsync();
        }

        // GET: api/ShoppingCarts/user/5
        [HttpGet("user/{idUser}")]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetCartByUser(int idUser)
        {
            var cart = await _context.ShoppingCarts
                                     .Include(c => c.IdGameNavigation)
                                     .Where(c => c.IdUser == idUser)
                                     .ToListAsync();

            if (cart == null || cart.Count == 0)
                return NotFound(new { message = _localizer["ShoppingCartNotFound"] });

            return Ok(cart);
        }

        // POST: api/ShoppingCarts/add
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int idUser, int idGame, int amount)
        {
            var existing = await _context.ShoppingCarts.FindAsync(idUser, idGame);
            if (existing != null)
                return Conflict(new { message = _localizer["GameAlreadyInCart"] });

            var game = await _context.Games.FindAsync(idGame);
            if (game == null)
                return NotFound(new { message = _localizer["GameNotFound"] });

            var cartItem = new ShoppingCart
            {
                IdUser = idUser,
                IdGame = idGame,
                Amount = amount,
                AddDate = DateTime.Now
            };

            _context.ShoppingCarts.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["GameAddedToCart"], data = cartItem });
        }


        // DELETE: api/ShoppingCarts/5
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveFromCart(int userId, int gameId)
        {
            var cartItem = await _context.ShoppingCarts.FindAsync(userId, gameId);

            if (cartItem == null)
                return NotFound(new { message = _localizer["ItemNotInCart"] });

            _context.ShoppingCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["ItemRemovedFromCart"] });
        }

        [HttpDelete("clear/{idUser}")]
        public async Task<IActionResult> ClearCart(int idUser)
        {
            var cartItems = await _context.ShoppingCarts
                                          .Where(c => c.IdUser == idUser)
                                          .ToListAsync();


            if (!cartItems.Any())
                return NotFound(new { message = _localizer["NoItemsToRemove"] });

            _context.ShoppingCarts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["CartCleared"] });
        }

        // GET: api/ShoppingCarts/total/5
        [HttpGet("total/{idUser}")]
        public async Task<ActionResult<decimal>> GetTotal(int idUser)
        {
            var cartItems = await _context.ShoppingCarts
                                          .Include(c => c.IdGameNavigation)
                                          .Where(c => c.IdUser == idUser)
                                          .ToListAsync();


            if (cartItems.Count == 0)
                return NotFound(new { message = _localizer["CartIsEmpty"] });

            var total = cartItems.Sum(item => (item.IdGameNavigation.PriceBuy ?? 0) * item.Amount);
            return Ok(new { message = _localizer["TotalCalculated"], total });
        }

        private bool ShoppingCartExists(int id)
        {
            return _context.ShoppingCarts.Any(e => e.IdUser == id);
        }
    }
}
