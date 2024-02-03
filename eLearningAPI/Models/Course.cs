﻿namespace eLearningAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}
