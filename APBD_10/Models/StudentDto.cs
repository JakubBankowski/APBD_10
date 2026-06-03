using System.ComponentModel.DataAnnotations;

namespace APBD_10.Models;

public class StudentDto
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Index number is required")]
    public string IndexNumber { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }

    [Range(1,8,  ErrorMessage = "Number must be between 1 and 8")]
    public int Semester { get; set; } = 1;
}