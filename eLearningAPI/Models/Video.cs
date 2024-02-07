using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;

namespace eLearningAPI.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string VideoFile { get; set; }   
        public Lesson Lesson { get; set; }
        public int LessonId { get; set; }
    }
}
