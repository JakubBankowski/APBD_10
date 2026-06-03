using System.ComponentModel.DataAnnotations;

namespace APBD_10.Backend.Models;

public class Course
{
    public int Id { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required] 
    [Range(1,30)]
    public int Ects;
}