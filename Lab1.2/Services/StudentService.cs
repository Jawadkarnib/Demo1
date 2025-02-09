using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Lab1._2.Data;

using Microsoft.AspNetCore.Http;
using System.Linq;
using Lab1._2.Commands.AddImage;
using Lab1._2.Commands.ChangeStudentName;
using Lab1._2.Commands.CreateStudent;
using Lab1._2.Commands.DeleteStudent;
using Lab1._2.models;
using Lab1._2.Queries.GetDate;
using Lab1._2.Queries.GetGeneric;
using Lab1._2.Queries.GetStudentById;
using Lab1._2.Queries.GetStudentByName;
using Lab1._2.Queries.GetStudents;
using MediatR;
using Student = Lab1._2.models.Student;

namespace Lab1._2.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMediator _mediator;


        public StudentService(IMediator mediator)
        {
            _mediator = mediator;
         
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var query = new GetStudents();
            return await _mediator.Send(query);
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            var query = new GetStudentbyId(studentId);
            return await _mediator.Send(query);
        }

        public async Task<List<Student>> GetStudentsByName(string name)
        {
            var query = new GetStudentByName(name);
            return await _mediator.Send(query);
        }


        public async Task<Unit> AddStudent(StudentAddDTO student)
        {
            var command = new CreateStudentCommand( student);
            await _mediator.Send(command);
            return Unit.Value;
        }

        public async Task<Unit> ChangeStudentName(StudentViewModel request)
        {
            var command = new ChangeStudentNameCommand(request);
            return await _mediator.Send(command);
        }

        public async Task<Unit>UploadMultipleImages(IFormFile[] files)
        {
            var command = new AddImageCommand(files);
            return await _mediator.Send(command); 
        }

        public async Task<Unit>DeleteStudent(long studentId)
        {
            var command = new DeleteStudentCommand { StudentId = studentId };
            return await _mediator.Send(command);
        }

        public async Task<string> GetDate(string language)
        {
            var query = new GetDate(language);
            return await _mediator.Send(query);
        }
        
        public async Task<List<Student>> GetStudentsByFilter(string name = null, string email = null)
        {
            var query = new GetGeneric(email,name);
            return await _mediator.Send(query);
           
          
        }
    }
}
