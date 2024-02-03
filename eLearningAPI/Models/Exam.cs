namespace eLearningAPI.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExamFile { get; set; }
        public Lesson Lesson { get; set; }
    }
}
