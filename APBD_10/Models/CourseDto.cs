using System.ComponentModel.DataAnnotations;

namespace APBD_10.Models;

public class CourseDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Range(1, 30, ErrorMessage = "Ects must be between 1 and 30")]
    public int Ects { get; set; }
}