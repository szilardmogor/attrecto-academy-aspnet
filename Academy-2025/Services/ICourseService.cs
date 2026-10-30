using Academy_2025.Data;
using Academy_2025.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Academy_2025.Services
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task CreateAsync(CourseDto data);
        Task<CourseDto?> UpdateAsync(int id, CourseDto data);
        Task<bool> DeleteAsync(int id);



    }
}
