using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return Student.Students;
        }
        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public Student Get(int id)
        {
            return Student.Students.Find(u => u.Id == id);
        }
        [HttpGet("filterbyname")]
        public List<Student> GetByName([FromQuery] string name)
        {
            var filteredStudents = Student.Students.Where(student => student.Name.Contains(name)).ToList();
            return filteredStudents;
        }
        [HttpGet("getdate")]
        public override String getDate([FromHeader] String language)
        {
            CultureInfo culture = new CultureInfo(language);
            string formattedDate = DateTime.Now.ToString("F", culture);
            return formattedDate;
        }
        // POST: api/Student
        [HttpPost]
        public void Post([FromBody] Student obj)
        {
            Student.Students.Add(obj);
        }
        [HttpPost("ChangeName")]
        public ActionResult ChangeStudentName([FromBody] StudentViewModel request)
        {
            Student studentToUpdate = Student.Students.FirstOrDefault(s => s.Id == request.StudentId);
            studentToUpdate.Name = request.NewName;
            return Ok("Student name updated successfully.");
        }
    

        [HttpPost("UploadMultipleImages")]
        public ActionResult UploadMultipleImages(IFormFile[] files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    // Generate a unique filename for the uploaded file
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                    // Save the file to the specified path
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            return Ok();
            }
        

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
          var ObjToDelete=  Student.Students.Find(u => u.Id == id);
          Student.Students.Remove(ObjToDelete);
        }
     
    }
}
