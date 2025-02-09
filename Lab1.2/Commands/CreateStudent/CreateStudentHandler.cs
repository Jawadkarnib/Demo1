using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Commands.CreateStudent
{
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateStudentHandler> _logger;

        public CreateStudentHandler(ApplicationDbContext context, ILogger<CreateStudentHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Student == null)
                {
                    _logger.LogError("CreateStudentHandler: Student information is required.");
                    throw new CustomApplicationException("Student information is required.", 400);
                }

                var newStudent = new Student
                {
                    Name = request.Student.Name,
                    Email = request.Student.Email,
                   
                };

                _context.Students.Add(newStudent);
                _context.SaveChanges();

                return Task.FromResult(Unit.Value);
            }
            catch (CustomApplicationException)
            {
             
                throw;
            }
            catch (Exception ex)
            {
              
                _logger.LogError($"Exception occurred in CreateStudentHandler: {ex.Message}");
                throw new CustomApplicationException("Internal server error.", ex, 500);
            }
        }
    }
}