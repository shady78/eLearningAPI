using eLearningAPI.DTO;
using eLearningAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("addclass")]
        public async Task<IActionResult> AddClass([FromBody] ClassDTO classDTO)
        {
            if (classDTO == null)
            {
                return BadRequest("Invalid class data.");
            }
            if (_context.Class.Any(c => c.Name == classDTO.Name))
            {
                return BadRequest($"class with name '{classDTO.Name}' already exists.");
            }

            var category = new Class
            {
                Name = classDTO.Name
            };

            _context.Class.Add(category);
            await _context.SaveChangesAsync();

            return Ok("Class added successfully.");
        }
        [HttpPut("updateclass/{id}")]
        public async Task<IActionResult> UpdateClass(int id, [FromBody] ClassDTO updatedClassDTO)
        {
            var existingClass = await _context.Class.FindAsync(id);

            if (existingClass == null)
            {
                return NotFound("Class not found.");
            }

            if (_context.Class.Any(c => c.Name == updatedClassDTO.Name && c.Id != id))
            {
                return BadRequest($"Class with name '{updatedClassDTO.Name}' already exists.");
            }

            existingClass.Name = updatedClassDTO.Name;

            await _context.SaveChangesAsync();

            return Ok("Class updated successfully.");
        }
        [HttpDelete("deleteclass/{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var classs = await _context.Class.FindAsync(id);

            if (classs == null)
            {
                return NotFound("Class not found.");
            }

            _context.Class.Remove(classs);
            await _context.SaveChangesAsync();

            return Ok("Class deleted successfully.");
        }

        [HttpGet("getclassbyId/{id}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var classs = await _context.Class.FindAsync(id);

            if (classs == null)
            {
                return NotFound("Class not found.");
            }

            return Ok(classs);
        }

        [HttpGet("getallclass")]
        public IActionResult GetAllClass()
        {
            var classs = _context.Class.ToList();
            return Ok(classs);
        }
    }
}
