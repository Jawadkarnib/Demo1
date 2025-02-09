using Lab1._2.models;
using Lab1._2.Query;

namespace Lab1._2.Queries.GetStudentByName;

public record GetStudentByName(string name):IQuery<List<Student>>
{
    
}