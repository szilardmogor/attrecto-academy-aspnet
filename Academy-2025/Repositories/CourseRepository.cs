using Academy_2025.Data;
using Microsoft.EntityFrameworkCore;

namespace Academy_2025.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Course>> GetAllAsync()
        {
            return _context.Courses
                .Include(course => course.Author)
                .ToListAsync();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            return _context.Courses
                .Include(course => course.Author)
                .FirstOrDefaultAsync(course => course.Id == id);
        }

        public async Task CreateAsync(Course data)
        {
            _context.Courses.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task<Course?> UpdateAsync(int id, Course data)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(course => course.Id == id);

            if (course is null)
            {
                return null;
            }

            course.Name = data.Name;
            course.Description = data.Description;
            course.AuthorId = data.AuthorId;

            await _context.SaveChangesAsync();

            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses
                .FirstOrDefaultAsync(course => course.Id == id);

            if (course is null)
            {
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<int> UpdateAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}