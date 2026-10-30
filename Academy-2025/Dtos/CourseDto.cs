using System.ComponentModel.DataAnnotations;

namespace Academy_2025.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public AuthorDto? Author { get; set; }
    }
}