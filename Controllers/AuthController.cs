using GameVerse.Data;
using GameVerse.DTOs;
using GameVerse.Models;
using GameVerse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AuthController(GameVerseDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return Conflict("El correo ya está registrado.");

            var user = new User
            {
                Email = dto.Email,
                Username = dto.Username,
                Rol = dto.Rol,
                Password = HashPassword(dto.Password!)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado con éxito.");
        }


        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User credentials)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);
            if (user == null || !VerifyPassword(credentials.Password!, user.Password!))
                return Unauthorized("Credenciales inválidas.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                idUser = user.IdUser,
                username = user.Username 
            });
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
