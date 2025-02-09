using Lab1._1.models;
using Microsoft.AspNetCore.Mvc;

namespace Lab1._1.Services;

public interface IStudentService
{
   public IEnumerable<Student> GetAllStudents();
   public Student GetStudentById(int studentId);
   public List<Student> GetStudentsByName(string name);
   public void AddStudent(Student student);
   public void ChangeStudentName(StudentViewModel request);
   public void UploadMultipleImages(IFormFile[] files);
   public void DeleteStudent(int studentId);

   public string GetDate(string language);


}