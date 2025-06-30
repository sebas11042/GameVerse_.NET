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
    [Authorize(Roles = "admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        private readonly IStringLocalizer<CategoriesController> _localizer;

        public CategoriesController(GameVerseDbContext context, IStringLocalizer<CategoriesController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.Include(c => c.IdGames).ToListAsync();

            if (categories == null || categories.Count == 0)
            {
                return NotFound(new { message = _localizer["NoCategoriesFound"] });
            }

            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _context.Categories.Include(c => c.IdGames).FirstOrDefaultAsync(c => c.IdCategory == id);

            if (category == null)
            {
                return NotFound(new { message = _localizer["CategoryNotFound"] });
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.IdCategory)
            {
                return BadRequest(new { message = _localizer["CategoryIdMismatch"] });
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound(new { message = _localizer["CategoryNotFound"] });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = _localizer["CategoryUpdated"] });
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<Category>> StoreCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.IdCategory }, new { message = _localizer["CategoryCreated"], data = category });
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new { message = _localizer["CategoryNotFound"] });
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["CategoryDeleted"] });
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Category>>> Search(string name, bool exact = false)
        {
            List<Category> result;
            if (exact)
            {
                result = await _context.Categories
                   .Where(c => c.Name == name)
                   .Include(c => c.IdGames)
                   .ToListAsync();
            }
            else
            {
                result = await _context.Categories
                   .Where(c => c.Name.Contains(name))
                   .Include(c => c.IdGames)
                   .ToListAsync();
            }

            if (result == null || result.Count == 0)
            {
                return NotFound(new { message = _localizer["NoMatchesFound"] });
            }

            return Ok(result);
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.IdCategory == id);
        }
    }
}
