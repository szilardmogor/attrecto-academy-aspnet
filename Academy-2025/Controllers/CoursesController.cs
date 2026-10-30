using Academy_2025.Dtos;
using Academy_2025.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Academy_2025.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<IEnumerable<CourseDto>> GetAsync()
        {
            return await _courseService.GetAllAsync();
        }

        // GET api/<CourseController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetAsync(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            return course == null ? NotFound() : Ok(course);
        }

        // POST api/<CourseController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] CourseDto data)
        {
            await _courseService.CreateAsync(data);
            return NoContent();
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> PutAsync(int id, [FromBody] CourseDto data)
        {
            var course = await _courseService.UpdateAsync(id, data);

            return course == null ? NotFound() : Ok(course);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _courseService.DeleteAsync(id);

            return result ? NoContent() : NotFound();
        }

        [HttpGet("{id}/author")]
        public async Task<ActionResult<AuthorDto>> GetAuthorAsync(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            return course.Author is null
                ? NotFound()
                : Ok(course.Author);
        }
    }
}
