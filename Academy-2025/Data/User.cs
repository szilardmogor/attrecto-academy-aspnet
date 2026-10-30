using System.ComponentModel.DataAnnotations;

namespace Academy_2025.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public string? Name { get; set; }

        public string? Role { get; set; }


        public byte? Age { get; set; }

        public ICollection<Course> Courses { get; set; } = [];

    }
}
