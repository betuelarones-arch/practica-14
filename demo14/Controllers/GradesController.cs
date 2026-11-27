using demo14.Data;
using demo14.DTO;
using demo14.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo14.Controllers;

[ApiController]
[Route("api/grades")]
public class GradesController(AcademiaContext context) : ControllerBase
{
    [HttpPost("insertar")]
    public ActionResult<Grade> InsertarGrado(GradeRequest request)
    {
        var grade = new Grade { Name = request.Name, Description = request.Description };
        context.Grades.Add(grade);
        context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = grade.IdGrade }, grade);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Grade> Get(int id)
    {
        var grade = context.Grades.Find(id);
        return grade is null ? NotFound() : grade;
    }

    [HttpDelete("eliminar")]
    public IActionResult EliminarGrado(IdRequest request)
    {
        var grade = context.Grades.Find(request.Id);
        if (grade is null) return NotFound();
        grade.IsDeleted = true;
        context.SaveChanges();
        return NoContent();
    }
}
