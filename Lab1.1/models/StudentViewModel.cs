using System.ComponentModel.DataAnnotations;
namespace Lab1._1.models;

public class StudentViewModel
{
    [Required(ErrorMessage = "Id is required.")]
    [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than zero.")]
    public int StudentId { get; set; }
    [Required(ErrorMessage = "New Name is required.")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string NewName { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
}