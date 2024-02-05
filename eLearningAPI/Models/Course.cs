namespace eLearningAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public Class Class { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
