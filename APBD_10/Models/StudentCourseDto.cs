namespace APBD_10.Models;

public class StudentCourseDto
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}