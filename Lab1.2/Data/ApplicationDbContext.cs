using Lab1._2.models;
using Microsoft.EntityFrameworkCore;

namespace Lab1._2.Data;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions options):base(options)
    {
     
    }

    public DbSet<Student> Students { get; set; }    
    public DbSet<Image> Images { get; set; }


    
}