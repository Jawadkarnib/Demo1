using Lab1._2.Abstraction;
using Lab1._2.Data;
using Lab1._2.models;
using MediatR;

namespace Lab1._2.Commands.AddImage;

public class AddImageCommandHandler : ICommandHandler<AddImageCommand,Unit>
{
    private readonly ApplicationDbContext _context;

    public AddImageCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Unit> Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            foreach (var file in request.files)
            {
                if (file.Length > 0)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);
                    Image newImage = new Image
                    {
                        path = filePath,
                    };
                    _context.Add(newImage);
                    _context.SaveChanges();
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Error while uploading multiple images: {ex.Message}", ex);
        }

        return Task.FromResult(Unit.Value);
    }
}

