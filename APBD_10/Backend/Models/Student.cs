using System.ComponentModel.DataAnnotations;

namespace APBD_10.Backend.Models;

public class Student
{
    public int Id { get; set; }
    
    [Required, MaxLength(8)]
    public string IndexNumber { get; set; }
    
    [Required, MaxLength(20)]
    public string FirstName { get; set; }
    
    [Required, MaxLength(50)]
    public string LastName { get; set; }
    
    [Required, MaxLength(50), EmailAddress]
    public string Email { get; set; }

    [Required, Range(1,8)]
    public int Semester { get; set; } = 1;
}