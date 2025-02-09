
using Lab1._2.Services;
using Student = Lab1._2.models.Student;
using System.Globalization;
using Lab1._2.Commands.AddImage;
using Lab1._2.Commands.ChangeStudentName;
using Lab1._2.Commands.CreateStudent;
using Lab1._2.Commands.DeleteStudent;
using Lab1._2.models;
using Lab1._2.Queries.GetDate;
using Lab1._2.Queries.GetStudentById;
using Lab1._2.Queries.GetStudents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Lab1._2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentsController> _logger;
        private readonly IMediator _mediator;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger, IMediator mediator)
        {
            _studentService = studentService;
            _logger = logger;
            _mediator = mediator;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<List<Student>> Get()
        {
            var query = new GetStudents();
            return await _mediator.Send(query);
        }

        [HttpGet("genericGet")]
        public async Task<IActionResult> GenericGet(int? id = null, string name = null, string email = null)
        {
            if (id.HasValue)
            {
                var student = await _studentService.GetStudentById(id.Value);

                if (student == null)
                {
                    return NotFound("Student not found.");
                }

                return Ok(student);
            }
            else
            {
                var filteredStudents = await _studentService.GetStudentsByFilter(name, email);

                if (filteredStudents.Count == 0)
                {
                    return NotFound("No students found with the provided filter criteria.");
                }

                return Ok(filteredStudents);
            }

        }

        // GET: api/Student/5
        [HttpGet("{id}", Name = "GetbyId")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID. Please provide a valid ID.");
            }
            var query = new GetStudentbyId(id);
            var student  = await _mediator.Send(query);
            
            
            
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
        public async Task<IActionResult> Post([FromBody] StudentAddDTO obj)
        {
            if (obj == null)
            {
                return BadRequest("Invalid request body.");
            }
            var command = new CreateStudentCommand( obj);
            await _mediator.Send(command);
            
     
           
            return Ok("Studentttt added successfully.");
        }

        [HttpPost("ChangeName")]
        public async Task<IActionResult> ChangeStudentName([FromBody] StudentViewModel request)
        {
            if (request == null || request.StudentId <= 0 || string.IsNullOrEmpty(request.NewName))
            {
                return BadRequest("Invalid request body.");
            }
            var command = new ChangeStudentNameCommand(request);
             await _mediator.Send(command);
          
            return Ok("Student name updated successfully.");
        }

        [HttpPost("UploadMultipleImages")]
        public async Task<IActionResult> UploadMultipleImages([FromForm] IFormFile[] files)
        {
            if (files == null || files.Length == 0)
            {
                return BadRequest("No files provided for upload.");
            }

            var command = new AddImageCommand(files);
            await _mediator.Send(command); 
                 
           

            return Ok("Files uploaded successfully.");
        }


        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID.");
            }
          
            var command = new DeleteStudentCommand { StudentId = id };
            await _mediator.Send(command);
            return Ok("Student succcesfully deleted");
        }

        [HttpGet("getdate")]
        public async Task<IActionResult> GetDate(string language)
        {
            if (!IsLanguageValid(language))
            {
                return BadRequest("Invalid language specified.");
            }

            
            var query = new GetDate(language);
            var formattedDate=  await _mediator.Send(query);
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