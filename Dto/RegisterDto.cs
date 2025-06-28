using System.ComponentModel.DataAnnotations;

namespace GameVerse.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Rol { get; set; } = null!;

        [Required]
        public string Username { get; set; } = null!;
    }
}
