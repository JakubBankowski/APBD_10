namespace APBD_10.Backend.Models;

public class StudentCourse
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}