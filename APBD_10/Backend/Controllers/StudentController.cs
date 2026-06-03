using APBD_10.Backend.Data;
using APBD_10.Backend.DTOs;
using APBD_10.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Backend.Controllers;

[ApiController]
[Route("api")]
public class StudentController : ControllerBase
{
    private readonly AppDbContext _context;
    public StudentController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("students")]
    public async Task<ActionResult<List<Student>>> GetStudents()
    {
        var students = await _context.Students.ToListAsync();
        
        return Ok(students);
    }

    [HttpGet("students/{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        
        return Ok(student);
    }

    [HttpPost("students")]
    public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpGet("courses")]
    public async Task<ActionResult<Course>> GetCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        return Ok(courses);
    }

    [HttpPost("students/{id}/courses")]
    public async Task<ActionResult> AssignCourse(int id, [FromBody] StudentCourseDTO request)
    {
        var studentExists = await _context.Students.AnyAsync(s => s.Id == id);
        var courseExists = await _context.Courses.AnyAsync(c => c.Id == request.CourseId);

        if (studentExists || courseExists) return NotFound();

        var alreadyAssigned = await _context.StudentCourses.AnyAsync(s => s.StudentId == id &&
                                                                          s.CourseId == request.CourseId);
        if (alreadyAssigned) return BadRequest("Already exists");

        var studentCourse = new StudentCourse
        {
            CourseId = request.CourseId,
            StudentId = id,
            AssignedAt = DateTime.UtcNow
        };

        _context.StudentCourses.Add(studentCourse);
        await _context.SaveChangesAsync();
        
        return Ok(new { Message = "Course assigned successfully" });
    }
}