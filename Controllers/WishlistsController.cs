using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameVerse.Models;
using GameVerse.Data;
using Microsoft.Extensions.Localization;

namespace GameVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistsController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        private readonly IStringLocalizer<WishlistsController> _localizer;

        public WishlistsController(GameVerseDbContext context, IStringLocalizer<WishlistsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlistByUser(User user)
        {
            var wishlists = await _context.Wishlists
                .Where(w => w.IdUser == user.IdUser)
                .Include(w => w.IdGameNavigation)
                .ToListAsync();

            if (wishlists == null || !wishlists.Any())
                return NotFound(new { message = _localizer["WishlistNotFound"] });

            return Ok(wishlists);
        }

        // POST: api/Wishlists/add
        [HttpPost("add")]
        public async Task<ActionResult<Wishlist>> AddWishlist([FromQuery] int idUser, [FromQuery] int IdGame)
        {
            var exists = await _context.Wishlists.AnyAsync(w => w.IdUser == idUser && w.IdGame == IdGame);

            if (exists)
            {
                return Ok(new { message = _localizer["AlreadyInWishlist"] });
            }


            var wishlist = new Wishlist
            {
                IdUser = idUser,
                IdGame = IdGame,
                AddedAt = DateTime.Now
            };

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["AddedToWishlist"] });
        }

        // DELETE: api/Wishlists/remove
        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteWishlist([FromQuery] int userId, [FromQuery] int gameId)
        {
            var wishlist = await _context.Wishlists
               .FirstOrDefaultAsync(w => w.IdUser == userId && w.IdGame == gameId);

            if (wishlist == null)
                return NotFound(new { message = _localizer["WishlistItemNotFound"] });

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["RemovedFromWishlist"] });
        }

        private bool WishlistExists(int id)
        {
            return _context.Wishlists.Any(e => e.IdUser == id);
        }
    }
}
