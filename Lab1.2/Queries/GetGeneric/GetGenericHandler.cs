using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1._2.Queries.GetGeneric
{
    public class GetGenericHandler : IQueryHandler<GetGeneric, List<Student>>
    {
        private readonly ApplicationDbContext _context;

        public GetGenericHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> Handle(GetGeneric request, CancellationToken cancellationToken)
        {
            IQueryable<Student> studentsQuery = _context.Students;

            if (!string.IsNullOrEmpty(request.name) && !string.IsNullOrEmpty(request.email))
            {
                studentsQuery = studentsQuery.Where(student =>
                    ( student.Name.Contains(request.name)) || student.Email.Contains(request.email));
                return await studentsQuery.ToListAsync(cancellationToken);

               
            }

            if (!string.IsNullOrEmpty(request.name))
            {
                studentsQuery = studentsQuery.Where(student => student.Name.Contains(request.name));
            }

            if (!string.IsNullOrEmpty(request.email))
            {
                studentsQuery = studentsQuery.Where(student => student.Email.Contains(request.email));
            }

            List<Student> filteredStudents = await studentsQuery.ToListAsync(cancellationToken);

            return filteredStudents;
        }
    }
}