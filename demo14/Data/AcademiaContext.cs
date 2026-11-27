using demo14.Models;
using Microsoft.EntityFrameworkCore;

namespace demo14.Data;

public class AcademiaContext(DbContextOptions<AcademiaContext> options) : DbContext(options)
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Grade> Grades => Set<Grade>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().HasKey(x => x.IdCourse);
        modelBuilder.Entity<Student>().HasKey(x => x.IdStudent);
        modelBuilder.Entity<Grade>().HasKey(x => x.IdGrade);
        modelBuilder.Entity<Enrollment>().HasKey(x => x.IdEnrollment);

        modelBuilder.Entity<Student>()
            .HasOne(x => x.Grade)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.GradeIdGrade)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(x => x.Student)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.StudentIdStudent)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(x => x.Course)
            .WithMany(x => x.Enrollments)
            .HasForeignKey(x => x.CourseIdCourse)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>().HasIndex(x => x.Email).IsUnique();
        modelBuilder.Entity<Enrollment>().HasIndex(x => new { x.StudentIdStudent, x.CourseIdCourse }).IsUnique();

        modelBuilder.Entity<Course>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Student>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Grade>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Enrollment>().HasQueryFilter(x => !x.IsDeleted);
    }
}
