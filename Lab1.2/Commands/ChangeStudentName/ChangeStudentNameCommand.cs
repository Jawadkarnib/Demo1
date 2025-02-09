
using Lab1._2.Abstraction;
using Lab1._2.models;
using MediatR;

namespace Lab1._2.Commands.ChangeStudentName;

public class ChangeStudentNameCommand :  ICommand<Unit>
{
   public StudentViewModel newStudent { get; set; }

   public ChangeStudentNameCommand(StudentViewModel newStudent)
   {
      this.newStudent = newStudent;
   }
}