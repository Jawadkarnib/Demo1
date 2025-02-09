using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Queries.GetStudentById
{
    public class GetStudentbyIdHandler : IQueryHandler<GetStudentbyId, Student>
    {
        private readonly ApplicationDbContext _context;

        public GetStudentbyIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public  Task<Student> Handle(GetStudentbyId request, CancellationToken cancellationToken)
        {
            try
            {
                var student = _context.Students.Find(request.id);

                if (student == null)
                {

                    throw new CustomApplicationException($"Student with ID {request.id} not found", 404);
                }

                return Task.FromResult<Student>(student);
            }
            catch (Exception ex)
            {
                if (ex == null)
                {
                   
                    throw new CustomApplicationException("Unexpected null exception", 500);
                }


                throw ex;
            }
        }
    }
}