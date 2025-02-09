using Lab1._2.Abstraction;
using Lab1._2.models;
using MediatR;

namespace Lab1._2.Commands.CreateStudent;

public class CreateStudentCommand : ICommand<Unit>
{
    public CreateStudentCommand(StudentAddDTO student)
    {
        Student = student;
    }

    public StudentAddDTO Student { get; set; } 



}