using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameVerse.Models;
using GameVerse.Data;

namespace GameVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        public WishlistsController(GameVerseDbContext context)
        {
            _context = context;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlistByUser(User user)
        {
            var wislists = await _context.Wishlists
                .Where(w => w.IdUser == user.IdUser)
                .Include(w => w.IdGameNavigation)
                .ToListAsync();

            return wislists;
        }

        // POST: api/Wishlists
        [HttpPost("add")]
        public async Task<ActionResult<Wishlist>> AddWishlist([FromQuery] int idUser, [FromQuery] int IdGame)
        {
            var exists = await _context.Wishlists.AnyAsync(w => w.IdUser == idUser && w.IdGame == IdGame);
            if (exists)
                return Ok("Ya existe en wishlist");

            var wishlist = new Wishlist
            {
                IdUser = idUser,
                IdGame = IdGame,
                AddedAt = DateTime.Now
            };

            _context.Wishlists.Add(wishlist);
            await _context.SaveChangesAsync();

            return Ok("Agregado a wishlist");
        }

        // DELETE: api/Wishlists/5
        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteWishlist([FromQuery] int userId, [FromQuery] int gameId)
        {
            var wishlist = await _context.Wishlists
               .FirstOrDefaultAsync(w => w.IdUser == userId && w.IdGame == gameId);

            if (wishlist == null)
                return NotFound();

            _context.Wishlists.Remove(wishlist);
            await _context.SaveChangesAsync();

            return Ok("Eliminado de wishlist");
        }

        private bool WishlistExists(int id)
        {
            return _context.Wishlists.Any(e => e.IdUser == id);
        }
    }
}
