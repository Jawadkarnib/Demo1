using System.Globalization;
using Lab1._1.Filters;
using Lab1._1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab1._1.models;

namespace Lab1._1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(CallerHeaderFilter))]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService,ILogger<StudentsController>logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        // GET: api/Student
        [HttpGet]
        public IActionResult Get()
        {
            var students = _studentService.GetAllStudents();
            return Ok(students);
        }
        // GET: api/Student/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            
            if (id <= 0)
            {
                return BadRequest("Invalid student ID. Please provide a valid ID.");
            }

            var student = _studentService.GetStudentById(id);

           
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return Ok(student);
        }

        [HttpGet("filter")]
        public IActionResult GetByName([FromQuery] string name)
        {
       
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Invalid name. Please provide a valid name.");
            }

            var filteredStudents = _studentService.GetStudentsByName(name);
            return Ok(filteredStudents);
        }

        // POST: api/Student
        [HttpPost]
        public IActionResult Post([FromBody] Student obj)
        {
            
            if (obj == null)
            {
                return BadRequest("Invalid request body.");
            }

            _studentService.AddStudent(obj);
            return Ok("Student added successfully.");
        }

        [HttpPost("ChangeName")]
        public IActionResult ChangeStudentName([FromBody] StudentViewModel request)
        {
         
            if (request == null || request.StudentId <= 0 || string.IsNullOrEmpty(request.NewName))
            {
                return BadRequest("Invalid request body.");
            }

            _studentService.ChangeStudentName(request);
            return Ok("Student name updated successfully.");
        }

        [HttpPost("UploadMultipleImages")]
        public IActionResult UploadMultipleImages([FromForm] IFormFile[] files)
        {
            if (files == null || files.Length == 0)
            {
                return BadRequest("No files provided for upload.");
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                  
                    var allowedExtensions = new[] { ".png", ".jpeg", ".jpg" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest($"File with extension {fileExtension} is not allowed. Allowed extensions are {string.Join(", ", allowedExtensions)}.");
                    }

             
                    var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }

            return Ok("Files uploaded successfully.");
        }


        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
       
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }

            _studentService.DeleteStudent(id);
            return Ok("Student deleted successfully.");
        }

        [HttpGet("getdate")]
        public IActionResult GetDate(string language)
        {
            
            if (!IsLanguageValid(language))
            {
                return BadRequest("Invalid language specified.");
            }

            string formattedDate = _studentService.GetDate(language);

            if (formattedDate != null)
            {
                return Ok(formattedDate);
            }
            else
            {
                return BadRequest("Invalid language or other issue in date formatting.");
            }
        }

        private bool IsLanguageValid(string language)
        {
            try
            {
                CultureInfo.GetCultureInfo(language);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
