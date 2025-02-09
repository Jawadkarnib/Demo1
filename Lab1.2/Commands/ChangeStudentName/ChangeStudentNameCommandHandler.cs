using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Commands.ChangeStudentName
{
    public class ChangeStudentNameCommandHandler : ICommandHandler<ChangeStudentNameCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ChangeStudentNameCommandHandler> _logger;

        public ChangeStudentNameCommandHandler(ApplicationDbContext context, ILogger<ChangeStudentNameCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Unit> Handle(ChangeStudentNameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null || request.newStudent == null)
                {
                    _logger.LogError("Invalid or null request in ChangeStudentNameCommandHandler.");
                    throw new CustomApplicationException("Invalid or null request.", 400);
                }

                var studentToUpdate = _context.Students
                    .Where(u => u.Id == request.newStudent.StudentId)
                    .FirstOrDefault();

                if (studentToUpdate != null)
                {
                    if (!string.IsNullOrEmpty(request.newStudent.NewName))
                    {
                        studentToUpdate.Name = request.newStudent.NewName;
                        _context.SaveChanges();
                    }
                    else
                    {
                        _logger.LogError("ChangeStudentNameCommandHandler: NewName is null or empty.");
                        throw new CustomApplicationException("NewName is null or empty.", 400);
                    }
                }
                else
                {
                    _logger.LogError($"ChangeStudentNameCommandHandler: Student with ID {request.newStudent.StudentId} not found.");
                    throw new CustomApplicationException($"Student with ID {request.newStudent.StudentId} not found.", 404);
                }

                return Task.FromResult(Unit.Value);
            }
            catch (CustomApplicationException)
            {
                
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in ChangeStudentNameCommandHandler: {ex.Message}");
               
                throw new CustomApplicationException("Internal server error.", ex, 50);
            }
        }
    }
}
