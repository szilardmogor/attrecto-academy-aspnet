using Academy_2025.Data;
using Academy_2025.Dtos;
using Academy_2025.Repositories;

namespace Academy_2025.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Task CreateAsync(CourseDto data)
        {
            return _courseRepository.CreateAsync(MapToModel(data));
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _courseRepository.DeleteAsync(id);
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();

            return courses
                .Select(MapToDto)
                .ToList();
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            return course is null ? null : MapToDto(course);
        }

        public async Task<CourseDto?> UpdateAsync(int id, CourseDto data)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course is null)
            {
                return null;
            }

            course.Name = data.Name;
            course.Description = data.Description;
            course.AuthorId = data.AuthorId;

            await _courseRepository.UpdateAsync();

            return MapToDto(course);
        }

        private static CourseDto MapToDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                AuthorId = course.AuthorId,
                Author = course.Author is null
                    ? null
                    : new AuthorDto
                    {
                        Id = course.Author.Id,
                        Name = course.Author.Name,
                        Email = course.Author.Email
                    }
            };
        }

        private static Course MapToModel(CourseDto courseDto)
        {
            return new Course
            {
                Id = courseDto.Id,
                Name = courseDto.Name,
                Description = courseDto.Description,
                AuthorId = courseDto.AuthorId
            };
        }
    }
}