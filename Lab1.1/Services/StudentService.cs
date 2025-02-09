using System.Globalization;
using Lab1._1.Exceptions;
using Lab1._1.models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;



namespace Lab1._1.Services
{
    public class StudentService : IStudentService
    {
        public IEnumerable<Student> GetAllStudents()
        {
            return Student.Students;
        }

        public Student GetStudentById(int studentId)
        {
            try
            {
                return Student.Students.First(u => u.Id == studentId);
            }
            catch (InvalidOperationException)
            {

                throw new NotFoundException("Student not found."+ nameof(studentId),404);
            }
        }

        public List<Student> GetStudentsByName(string name)
        {
            try
            {
                var filteredStudents = Student.Students.Where(student => student.Name.Contains(name)).ToList();
                return filteredStudents;
            }
            catch (Exception ex)
            {

                throw new NotFoundException($"Error while filtering students by name: {ex.Message}"+ ex,404);
            }
        }

        public void AddStudent(Student student)
        {
            try
            {
                Student.Students.Add(student);
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error while adding student: {ex.Message}", ex);
            }
        }

        public void ChangeStudentName(StudentViewModel request)
        {
            try
            {
                Student studentToUpdate = Student.Students.First(s => s.Id == request.StudentId);
                studentToUpdate.Name = request.NewName;
            }
            catch (InvalidOperationException)
            {
                throw new NotFoundException("Student not found."+ nameof(request.StudentId),404);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error while changing student name: {ex.Message}", ex);
            }
        }

        public void UploadMultipleImages(IFormFile[] files)
        {
            try
            {
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error while uploading multiple images: {ex.Message}", ex);
            }
        }

        public void DeleteStudent(int studentId)
        {
            try
            {
                var studentToDelete = Student.Students.First(u => u.Id == studentId);
                Student.Students.Remove(studentToDelete);
            }
            catch (InvalidOperationException)
            {

                throw new NotFoundException("Student not found."+ nameof(studentId),404);
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"Error while deleting student: {ex.Message}", ex);
            }
        }

        public string GetDate(string language)
        {
            try
            {
                CultureInfo culture = new CultureInfo(language);
                string formattedDate = DateTime.Now.ToString("F", culture);
                return formattedDate;
            }
            catch (Exception ex)
            {
                return $"Error occurred: {ex.Message}";
              
            }
        }

    }
}
