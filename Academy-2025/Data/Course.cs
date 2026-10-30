using System.ComponentModel.DataAnnotations;

namespace Academy_2025.Data
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; } = null!;

        public ICollection<User> Users { get; set; } = [];
    }
}