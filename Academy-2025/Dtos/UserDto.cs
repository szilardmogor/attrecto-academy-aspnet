using System.ComponentModel.DataAnnotations;

namespace Academy_2025.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string? Email { get; set; }

        [Required]
        public required string? Password { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public string? Role { get; set; }

        public byte? Age { get; set; }

    }
}