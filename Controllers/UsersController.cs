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
    public class UsersController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        private readonly IStringLocalizer<UsersController> _localizer;
        public UsersController(GameVerseDbContext context, IStringLocalizer<UsersController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = _localizer["UserNotFound"] });
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest(new { message = _localizer["UserIdMismatch"] });
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound(new { message = _localizer["UserNotFound"] });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = _localizer["UserUpdate"] });
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> StoreUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict(new { message = _localizer["EmailAlreadyExists"] });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.IdUser }, new { message = _localizer["UserCreated"], data = user });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = _localizer["UserNotFound"] });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["UserDeleted"] });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }

        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        [HttpGet("library/{id}")]
        public async Task<ActionResult<object>> GetUserLibrary(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = _localizer["UserNotFound"] });

            var wishlistGames = await _context.Wishlists
                .Where(w => w.IdUser == id)
                .Include(w => w.IdGameNavigation)
                .Select(w => w.IdGameNavigation)
                .ToListAsync();

            var cartGames = await _context.ShoppingCarts
                .Where(sc => sc.IdUser == id)
                .Include(sc => sc.IdGameNavigation)
                .Select(sc => sc.IdGameNavigation)
                .ToListAsync();

            return Ok(new
            {
                Usuario = user,
                Wishlist = wishlistGames,
                ShoppingCart = cartGames
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new { message = _localizer["AdminWelcome"] });
        }

    }
}
