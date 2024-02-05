using eLearningAPI.DTO;
using eLearningAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eLearningAPI.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment webHostEnvironment;
        public CourseController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("search")]
        public IActionResult SearchCoursesByName([FromQuery] string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                return BadRequest("No result.");
            }
            var courses = _context.Courses
                .Where(c => c.Name.ToLower().Contains(courseName.ToLower()))
                .ToList();

            if (courses.Count == 0)
            {
                return NotFound("No result.");
            }

            var courseNames = courses.Select(c => c.Name).ToList();
            return Ok(courseNames);
        }
        [HttpGet("getallcourses")]
        public IActionResult GetCourses()
        {
            var courses = _context.Courses.ToList();
            var courseDTOs = courses.Select(c => new AddCourseDTO
            {
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
            }).ToList();
            return Ok(courseDTOs);
        }
        [HttpGet("getcoursebyID/{id}")]
        public IActionResult GetCourseById(int id)
        {
            var course = _context.Courses.Find(id);

            if (course == null)
            {
                return NotFound();
            }

            var courseDTO = new AddCourseDTO { Name = course.Name, Description = course.Description, Price = course.Price, };
            return Ok(courseDTO);
        }

        [HttpPost("addcourse")]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseDTO courseDTO)
        {
            if (courseDTO == null)
            {
                return BadRequest("Invalid course data.");
            }

            var clas = await _context.Class.FindAsync(courseDTO.Class_Id);

            if (clas == null)
            {
                return BadRequest("Invalid class Id");
            }

            if (_context.Courses.Any(c => c.Name == courseDTO.Name))
            {
                return BadRequest($"Course with name '{courseDTO.Name}' already exists.");
            }

            var course = new Course
            {
                Name = courseDTO.Name,
                Description = courseDTO.Description,
                Price = courseDTO.Price,
                Class = clas
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok("Course added successfully.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatecourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm] AddCourseDTO courseUpdateDTO)
        {
            var existingCourse = await _context.Courses.FindAsync(id);

            if (existingCourse == null)
            {
                return NotFound();
            }

            var clas = await _context.Class.FindAsync(courseUpdateDTO.Class_Id);

            if (clas == null)
            {
                return BadRequest("Invalid category Id");
            }

            if (_context.Courses.Any(c => c.Name == courseUpdateDTO.Name && c.Id != id))
            {
                return BadRequest($"Course with name '{courseUpdateDTO.Name}' already exists.");
            }

            existingCourse.Name = courseUpdateDTO.Name;
            existingCourse.Description = courseUpdateDTO.Description;
            existingCourse.Price = courseUpdateDTO.Price;
            existingCourse.Class = clas;

            await _context.SaveChangesAsync();

            return Ok("Course Updated successfully.");
        }

        // DELETE: api/courses/1
        [Authorize(Roles = "Admin")]
        [HttpDelete("deletecourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok("Course delete successfully.");
        }
    }
}