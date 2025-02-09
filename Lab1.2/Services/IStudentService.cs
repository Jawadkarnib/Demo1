
using Lab1._2.models;
using MediatR;
using Student = Lab1._2.models.Student;

namespace Lab1._2.Services;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllStudents();
    Task<Student> GetStudentById(int studentId);
    Task<List<Student>> GetStudentsByName(string name);
  
    public  Task<Unit> AddStudent(StudentAddDTO student);
    public Task<Unit> ChangeStudentName(StudentViewModel request);
    public Task<Unit> DeleteStudent(long studentId);
    public Task<Unit> UploadMultipleImages(IFormFile[] files);
    public Task<string> GetDate(string language);

    public Task<List<Student>> GetStudentsByFilter(string name, string email);
}