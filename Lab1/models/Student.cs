using System.Collections.Generic;

namespace Lab1.models
{
    public class Student
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, Name = "jawad", Email = "jawad@gmail.com" }
        };
    }
}