namespace eLearningAPI.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FileFile { get; set; }
        public Lesson Lesson { get; set; }
    }
}
