using Lab1._2.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeleteStudentCommandHandler> _logger;

        public DeleteStudentCommandHandler(ApplicationDbContext context, ILogger<DeleteStudentCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.StudentId <= 0)
                {
                
                    _logger.LogError("Invalid student ID in DeleteStudentCommandHandler.");
                    throw new CustomApplicationException("Invalid student ID.", 400);
                }

                var studentToDelete = _context.Students.Find(request.StudentId);

                if (studentToDelete != null)
                {
                    _context.Students.Remove(studentToDelete);
                    _context.SaveChanges();
                }
                else
                {
                  
                    _logger.LogError($"Student with ID {request.StudentId} not found in DeleteStudentCommandHandler.");
                    throw new CustomApplicationException($"Student with ID {request.StudentId} not found.", 404);
                }

                return Task.FromResult(Unit.Value);
            }
            catch (CustomApplicationException)
            {
            
                throw;
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                _logger.LogError($"Exception occurred in DeleteStudentCommandHandler: {ex.Message}");
                throw new CustomApplicationException("Internal server error.",  ex,500);
            }
        }
    }
}
