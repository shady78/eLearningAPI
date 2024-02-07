using eLearningAPI.Models;

namespace eLearningAPI.DTO
{
    public class AddVideoDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoFile { get; set; }
        public int LessonId { get; set; }
    }
}
