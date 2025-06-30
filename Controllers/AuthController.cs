using GameVerse.Data;
using GameVerse.DTOs;
using GameVerse.Models;
using GameVerse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Cryptography;
using System.Text;

namespace GameVerse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GameVerseDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IStringLocalizer<AuthController> _localizer;

        public AuthController(GameVerseDbContext context, JwtService jwtService, IStringLocalizer<AuthController> localizer)
        {
            _context = context;
            _jwtService = jwtService;
            _localizer = localizer;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return Conflict(new { message = _localizer["EmailAlreadyRegistered"] });

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                Rol = dto.Rol,
                Password = HashPassword(dto.Password!)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = _localizer["UserRegistered"] });
        }


        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User credentials)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);
            if (user == null || !VerifyPassword(credentials.Password!, user.Password!))
                return Unauthorized(new { message = _localizer["InvalidCredentials"] });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        // Encripta la contraseña usando SHA256 (puedes cambiar a BCrypt si quieres más seguridad)
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return HashPassword(inputPassword) == hashedPassword;
        }
    }
}
