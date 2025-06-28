using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameVerse.Models;
using GameVerse.Data;
using Microsoft.AspNetCore.Authorization; // 👈 IMPORTANTE


namespace GameVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingsController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        public ShoppingsController(GameVerseDbContext context)
        {
            _context = context;
        }

        // GET: api/Shoppings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shopping>>> GetShoppings()
        {
            return await _context.Shoppings
                .Include(s => s.IdUserNavigation)
                .Include(s => s.PurchaseDetails)
                    .ThenInclude(pd => pd.IdGameNavigation)
                .ToListAsync();
        }

        // GET: api/Shoppings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shopping>> GetShoppingById(int id)
        {
            var shopping = await _context.Shoppings
                .Include(s => s.IdUserNavigation)
                .Include(s => s.PurchaseDetails)
                    .ThenInclude(pd => pd.IdGameNavigation)
                .FirstOrDefaultAsync(s => s.IdBuy == id);

            if (shopping == null)
                return NotFound();

            return shopping;
        }

        [HttpGet("history/{idUser}")]
        public async Task<ActionResult<IEnumerable<Shopping>>> GetHistoryByUser(int idUser)
        {
            var user = await _context.Users.FindAsync(idUser);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var history = await _context.Shoppings
                .Where(s => s.IdUser == idUser)
                .Include(s => s.PurchaseDetails)
                    .ThenInclude(p => p.IdGameNavigation)
                .ToListAsync();

            return history;
        }

        // PUT: api/Shoppings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShopping(int id, Shopping shopping)
        {
            if (id != shopping.IdBuy)
                return BadRequest();

            _context.Entry(shopping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Shoppings/checkout/5
        [HttpPost("checkout/{idUser}")]
        public async Task<ActionResult<Shopping>> CheckoutCart(int idUser)
        {
            var user = await _context.Users.FindAsync(idUser);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            var cart = await _context.ShoppingCarts
                .Include(c => c.IdGameNavigation)
                .Where(c => c.IdUser == idUser)
                .ToListAsync();

            if (!cart.Any())
                return BadRequest("El carrito está vacío.");

            var shopping = new Shopping
            {
                IdUser = idUser,
                BuyDate = DateTime.Now,
                Total = 0
            };

            _context.Shoppings.Add(shopping);
            await _context.SaveChangesAsync();

            int total = 0;

            foreach (var item in cart)
            {
                var detail = new PurchaseDetail
                {
                    IdBuy = shopping.IdBuy,
                    IdGame = item.IdGame,
                    Amount = item.Amount,
                    Price = item.IdGameNavigation.PriceBuy ?? 0
                };
                total += detail.Amount.GetValueOrDefault() * detail.Price.GetValueOrDefault();
                _context.PurchaseDetails.Add(detail);
            }

            shopping.Total = total;
            await _context.SaveChangesAsync();

            _context.ShoppingCarts.RemoveRange(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetShoppingById), new { id = shopping.IdBuy }, shopping);
        }

        // DELETE: api/Shoppings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShopping(int id)
        {
            var shopping = await _context.Shoppings.FindAsync(id);

            if (shopping == null)
                return NotFound();

            _context.Shoppings.Remove(shopping);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoppingExists(int id)
        {
            return _context.Shoppings.Any(e => e.IdBuy == id);
        }
    }
}
