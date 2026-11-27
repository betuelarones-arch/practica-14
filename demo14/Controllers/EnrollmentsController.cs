using demo14.Data;
using demo14.DTO;
using demo14.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo14.Controllers;

[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController(AcademiaContext context) : ControllerBase
{
    [HttpPost("matricular")]
    public ActionResult<IEnumerable<Enrollment>> Matricular(EnrollmentRequest request)
    {
        if (request.List is null || request.List.Count == 0)
            return BadRequest("La lista de cursos no puede estar vacía.");

        if (context.Students.Find(request.IdStudent) is null) return BadRequest("El estudiante no existe.");

        var courseIds = request.List.Select(x => x.IdCourse).Distinct().ToList();
        var courses = context.Courses.Where(x => courseIds.Contains(x.IdCourse)).ToList();
        if (courses.Count != courseIds.Count) return BadRequest("Uno o más cursos no existen.");

        var existing = context.Enrollments
            .Where(x => x.StudentIdStudent == request.IdStudent && courseIds.Contains(x.CourseIdCourse))
            .Select(x => x.CourseIdCourse)
            .ToHashSet();

        var enrollments = courses
            .Where(course => !existing.Contains(course.IdCourse))
            .Select(course => new Enrollment
            {
                StudentIdStudent = request.IdStudent,
                CourseIdCourse = course.IdCourse,
                Date = DateTime.Now
            })
            .ToList();

        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
        return Ok(enrollments);
    }
}
