namespace eLearningAPI.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Course Course { get; set; }
        public ICollection<Video> videos { get; set; }
        public ICollection<File> files { get; set; }
        public ICollection<Exam> exams { get; set; }
    }
}
