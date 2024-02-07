using AutoMapper;
using eLearningAPI.DTO;
using eLearningAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eLearningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public VideoController(ApplicationDbContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var videos = _context.Videos.ToList();
            if (videos is null)
            {
                return NotFound();
            }
            return Ok(videos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _context.Videos.FirstOrDefault(v => v.Id == id);
            if (video is null)
            {
                return NotFound();
            }
            return Ok(video);
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(AddVideoDto videoDto)
        {
            if (videoDto is null)
            {
                return BadRequest("Invalid Video Data");
            }

            var lesson = await _context.Lessons.FindAsync(videoDto.LessonId);
            if (lesson is null)
            {
                return BadRequest("Invalid Lesson Id");
            }
            if (_context.Videos.Any(c => c.Name == videoDto.Name))
            {
                return BadRequest($"Video with name '{videoDto.Name}' is already exist.");
            }

            var video =  _mapper.Map<Video>(videoDto);

            _context.Add(video!);
            await _context.SaveChangesAsync();
            return Ok("Video added successfully.");
        }

        [HttpPut("updatevideo/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromForm] AddVideoDto videoUpdateDTO)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video is null)
            {
                return NotFound();
            }

            var vid = await _context.Videos.FindAsync(videoUpdateDTO.LessonId);

            if (video is null)
            {
                return BadRequest("Invalid category Id");
            }

            if (_context.Videos.Any(c => c.Name == videoUpdateDTO.Name && c.Id != id))
            {
                return BadRequest($"Course with name '{videoUpdateDTO.Name}' is already exists.");
            }

            video.Name = videoUpdateDTO.Name;
            video.Description = videoUpdateDTO.Description;
            video.VideoFile = videoUpdateDTO.VideoFile;
            video.LessonId = videoUpdateDTO.LessonId;

            _context.Update(video);
            await _context.SaveChangesAsync();

            return Ok("Video Updated successfully.");
        }


        [HttpDelete("deletevideo/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video is null)
            {
                return NotFound();
            }

            _context.Remove(video);
            await _context.SaveChangesAsync();

            return Ok("video deleted successfully.");
        }
    }
}
