using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using Microsoft.EntityFrameworkCore;

namespace Lab1._2.Queries.GetStudentByName;

public class GetStudentbyNameHandler:IQueryHandler<GetStudentByName,List<Student>>
{
    private readonly ApplicationDbContext _context;

    public GetStudentbyNameHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Student>> Handle(GetStudentByName request, CancellationToken cancellationToken)
    {
        try
        {
            var students = _context.Students.Where(student => student.Name.Contains(request.name)).ToList();

         
            if (students == null || students.Count == 0)
            {
                throw new CustomApplicationException($"No students found with name '{request.name}'", 404);
            }

            return Task.FromResult(students);
        }
        catch (Exception ex)
        {
            throw new CustomApplicationException($"Error occurred in GetStudentByNameHandler: {ex.Message}", ex, 500);
        }
    }

}