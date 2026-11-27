namespace demo14.Models;

public class Enrollment
{
    public int IdEnrollment { get; set; }
    public int StudentIdStudent { get; set; }
    public int CourseIdCourse { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }

    public Student? Student { get; set; }
    public Course? Course { get; set; }
}
