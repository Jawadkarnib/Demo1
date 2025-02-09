using Lab1._2.models;
using Lab1._2.Query;

namespace Lab1._2.Queries.GetStudentById;

public record GetStudentbyId(long id) :IQuery<Student>
{
    
}