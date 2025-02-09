using Lab1._2.models;
using Lab1._2.Query;

namespace Lab1._2.Queries.GetGeneric;

public class GetGeneric :IQuery<List<Student>>
{
    public string name;
    public string email;

    public GetGeneric(string email, string name)
    {
        this.email = email;
        this.name = name;
       
    }
}