using APBD_10.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_10.Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasData(
            new Course
            {
                Id = 1,
                Name = "APBD",
                Ects = 3
            },
            new Course
            {
                Id = 2,
                Name = "PRI",
                Ects = 4
            },
            new Course
            {
                Id = 3,
                Name = "NAI",
                Ects = 2
            }
            );
    }
}