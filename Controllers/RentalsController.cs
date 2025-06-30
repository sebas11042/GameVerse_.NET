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
    public class RentalsController : ControllerBase
    {
        private readonly GameVerseDbContext _context;
        private readonly IStringLocalizer<RentalsController> _localizer;

        public RentalsController(GameVerseDbContext context, IStringLocalizer<RentalsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals
                .Include(r => r.IdUserNavigation)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.IdGameNavigation)
                .ToListAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRentalById(int id)
        {
            var rental = await _context.Rentals
                .Include(r => r.IdUserNavigation)
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.IdGameNavigation)
                .FirstOrDefaultAsync(r => r.IdRent == id);

            if (rental == null)
                return NotFound(new { message = _localizer["RentalNotFound"] });

            return rental;
        }

        // PUT: api/Rentals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, Rental rental)
        {
            if (id != rental.IdRent)
                return BadRequest(new { message = _localizer["RentalIdMismatch"] });

            var existingRental = await _context.Rentals
                .Include(r => r.RentalDetails)
                .FirstOrDefaultAsync(r => r.IdRent == id);

            if (existingRental == null)
                return NotFound(new { message = _localizer["RentalNotFound"] });

            existingRental.RentDays = rental.RentDays;
            existingRental.EndDate = existingRental.StartDate?.AddDays(rental.RentDays ?? 0);
            existingRental.IdUser = rental.IdUser;

            existingRental.RentalDetails.Clear();
            foreach (var detail in rental.RentalDetails)
            {
                detail.IdRental = existingRental.IdRent;
                detail.IdRentalNavigation = existingRental;
                detail.RentalDate = existingRental.StartDate;
                detail.ExpireDate = existingRental.EndDate;
                existingRental.RentalDetails.Add(detail);
            }

            existingRental.Total = existingRental.RentalDetails.Sum(d =>
                (d.Price ?? 0) * (d.Amount ?? 0) * (rental.RentDays ?? 0));

            await _context.SaveChangesAsync();
            return Ok(new { message = _localizer["RentalUpdated"] });
        }

        // POST: api/Rentals
        [HttpPost]
        public async Task<ActionResult<Rental>> StoreRental(Rental rental)
        {
            if (rental.RentDays != 3 && rental.RentDays != 7 && rental.RentDays != 15)
                return BadRequest(new { message = _localizer["InvalidRentDays"] });

            if (rental.IdUser == null)
                return BadRequest(new { message = _localizer["UserRequired"] });

            rental.StartDate = DateTime.Today;
            rental.EndDate = DateTime.Today.AddDays(rental.RentDays ?? 0);
            rental.Status = true;

            foreach (var detail in rental.RentalDetails)
            {
                detail.IdRentalNavigation = rental;
                detail.RentalDate = rental.StartDate;
                detail.ExpireDate = rental.EndDate;
            }

            rental.Total = rental.RentalDetails.Sum(d =>
                (d.Price ?? 0) * (d.Amount ?? 0) * (rental.RentDays ?? 0));

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentalById), new { id = rental.IdRent }, new { message = _localizer["RentalCreated"], data = rental });
        }

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
                return NotFound(new { message = _localizer["RentalNotFound"] });

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["RentalDeleted"] });
        }

        [HttpGet("user/{idUser}")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentalsByUser(int idUser)
        {
            var rentals = await _context.Rentals
                .Include(r => r.RentalDetails)
                    .ThenInclude(rd => rd.IdGameNavigation)
                .Where(r => r.IdUser == idUser)
                .ToListAsync();

            if (rentals == null || rentals.Count == 0)
            {
                return NotFound(new { message = _localizer["NoRentalsFoundForUser"] });
            }

            return Ok(rentals);
        }
        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.IdRent == id);
        }
    }
}
