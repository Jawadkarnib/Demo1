using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab1._1.models
{
    public class Student
    {
        [Required(ErrorMessage = "Id is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than zero.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        public static List<Student> Students = new List<Student>
        {
            new Student { Id = 1, Name = "jawad", Email = "jawad@gmail.com" }
        };
    }
}