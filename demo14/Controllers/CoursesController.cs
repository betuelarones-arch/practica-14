using demo14.Data;
using demo14.DTO;
using demo14.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo14.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(AcademiaContext context) : ControllerBase
{
    [HttpPost("insertar")]
    public ActionResult<Course> InsertarCurso(CourseRequest request)
    {
        var course = new Course { Name = request.Name, Credit = request.Credit };
        context.Courses.Add(course);
        context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = course.IdCourse }, course);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Course> Get(int id)
    {
        var course = context.Courses.Find(id);
        return course is null ? NotFound() : course;
    }

    [HttpDelete("eliminar")]
    public IActionResult EliminarCurso(IdRequest request)
    {
        var course = context.Courses.Find(request.Id);
        if (course is null) return NotFound();
        course.IsDeleted = true;
        context.SaveChanges();
        return NoContent();
    }

    [HttpPost("eliminar-lista")]
    public IActionResult EliminarListaCursos(CourseListRequest request)
    {
        if (request.List is null || request.List.Count == 0)
            return BadRequest("La lista de cursos no puede estar vacía.");

        var ids = request.List.Select(x => x.IdCourse).ToHashSet();
        var courses = context.Courses.Where(x => ids.Contains(x.IdCourse)).ToList();
        courses.ForEach(x => x.IsDeleted = true);
        context.SaveChanges();
        return Ok(new { Deleted = courses.Count });
    }
}
