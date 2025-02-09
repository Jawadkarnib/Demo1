using Lab1._2.Abstraction;
using MediatR;

namespace Lab1._2.Commands.DeleteStudent;

public class DeleteStudentCommand :ICommand<Unit>
{
    public long StudentId { get; set; }
}