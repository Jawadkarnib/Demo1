using Lab1._2.Abstraction;
using MediatR;

namespace Lab1._2.Commands.AddImage;

public class AddImageCommand: ICommand<Unit>
{
   public AddImageCommand( IFormFile[] files)
   {
      this.files = files;
   }

   public  IFormFile[] files { get; set; }
}