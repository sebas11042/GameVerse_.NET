using Microsoft.AspNetCore.Mvc;
using GameVerse.Models;
using Microsoft.EntityFrameworkCore;
using GameVerse.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace GameVerseSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GamesController : ControllerBase
    {
        private readonly GameVerseDbContext _context;

        private readonly IStringLocalizer<GamesController> _localizer;
        public GamesController(GameVerseDbContext context, IStringLocalizer<GamesController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            var games = await _context.Games.Include(g => g.IdCategories).ToListAsync();

            if (games == null || games.Count == 0)
            {
                return NotFound(new { message = _localizer["NoGamesFound"] });
            }

            return Ok(games);
        }

        // GET: api/games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            var game = await _context.Games.Include(g => g.IdCategories).FirstOrDefaultAsync(g => g.IdGame == id);

            if (game == null)
            {
                return NotFound(new { message = _localizer["GamesNotFound"] });
            }

            return Ok(game);
        }

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult<Game>> StoreGame(Game game, [FromQuery] List<int> idCategories)
        {
            var categories = await _context.Categories.Where(c => idCategories.Contains(c.IdCategory)).ToListAsync();
            game.IdCategories = categories;

            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = game.IdGame }, new { message = _localizer["GamesCreated"], data = game });
        }

        // PUT: api/games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, Game game, [FromQuery] List<int> idCategories)
        {
            if (id != game.IdGame)
            {
                return BadRequest(new { message = _localizer["GameIdMismatch"] });
            }

            var existingGame = await _context.Games.Include(g => g.IdCategories).FirstOrDefaultAsync(g => g.IdGame == id);

            if (existingGame == null)
            {
                return NotFound(new { message = _localizer["GameNotFound"] });
            }

            existingGame.Name = game.Name;
            existingGame.Description = game.Description;
            existingGame.Title = game.Title;
            existingGame.Url = game.Url;
            existingGame.Image = game.Image;
            existingGame.PriceBuy = game.PriceBuy;
            existingGame.PriceRental = game.PriceRental;
            existingGame.IdCategories = await _context.Categories.Where(c => idCategories.Contains(c.IdCategory)).ToListAsync();

            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["GameUpdated"] });
        }

        // DELETE: api/games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound(new { message = _localizer["GameNotFound"] });
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["GameDeleted"] });
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Game>>> SearchGames(String name, bool exact = false, int? idCategory = null)
        {
            IQueryable<Game> query = exact ? _context.Games.Where(g => g.Name == name) : _context.Games.Where(g => g.Name.Contains(name));

            if (idCategory != null)
            {
                query = query.Where(g => g.IdCategories.Any(c => c.IdCategory == idCategory));
            }

            var result = await query.Include(g => g.IdCategories).ToListAsync();

            return result;
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Game>>> FilterByCategory([FromQuery] int idCategory)
        {
            var games = await _context.Games
                .Where(g => g.IdCategories.Any(c => c.IdCategory == idCategory))
                .Include(g => g.IdCategories)
                .ToListAsync();

            if (games == null || !games.Any())
            {
                return NotFound(new { message = _localizer["GameNotFound"] });
            }
            return Ok(games);
        }
    }
}
