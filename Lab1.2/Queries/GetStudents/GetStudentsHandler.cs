using System.Collections;
using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using Microsoft.EntityFrameworkCore;

namespace Lab1._2.Queries.GetStudents;

public class GetStudentsHandler:IQueryHandler<GetStudents,List<Student>>

{
    private readonly ApplicationDbContext _context;

    public GetStudentsHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> Handle(GetStudents request, CancellationToken cancellationToken)
    {
        try
        {
            var students = _context.Students.ToList();

         
            if (students == null || students.Count == 0)
            {
                throw new CustomApplicationException("No students found", 404);
            }

            return Task.FromResult(students);
        }
        catch (Exception ex)
        {
            throw new CustomApplicationException($"Error occurred in GetStudentsHandler: {ex.Message}", ex, 500);
        }
    }

}