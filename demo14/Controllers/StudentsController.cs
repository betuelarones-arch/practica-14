using demo14.Data;
using demo14.DTO;
using demo14.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo14.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController(AcademiaContext context) : ControllerBase
{
    [HttpPost("insertar")]
    public ActionResult<Student> InsertarEstudiante(StudentRequest request)
    {
        if (context.Grades.Find(request.Grade) is null) return BadRequest("El grado no existe.");
        var student = new Student
        {
            GradeIdGrade = request.Grade,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Email = request.Email
        };
        context.Students.Add(student);
        context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = student.IdStudent }, student);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> Get(int id)
    {
        var student = context.Students.Find(id);
        return student is null ? NotFound() : student;
    }

    [HttpPut("actualizar-contacto")]
    public IActionResult ActualizarDatosContacto(ContactRequest request)
    {
        var student = context.Students.Find(request.Id);
        if (student is null) return NotFound();
        student.Phone = request.Phone;
        student.Email = request.Email;
        context.SaveChanges();
        return NoContent();
    }

    [HttpPut("actualizar-personales")]
    public IActionResult ActualizarDatosPersonales(PersonalRequest request)
    {
        var student = context.Students.Find(request.Id);
        if (student is null) return NotFound();
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        context.SaveChanges();
        return NoContent();
    }

    [HttpPost("lista-por-grado")]
    public ActionResult<IEnumerable<Student>> InsertarListaEstudiantesPorGrado(StudentListRequest request)
    {
        if (context.Grades.Find(request.IdGrade) is null)
            return BadRequest("El grado no existe.");

        if (request.List is null || request.List.Count == 0)
            return BadRequest("La lista de estudiantes no puede estar vacía.");

        var students = request.List.Select(item => new Student
        {
            GradeIdGrade = request.IdGrade,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Phone = item.Phone,
            Email = item.Email
        }).ToList();

        context.Students.AddRange(students);
        context.SaveChanges();
        return Ok(students);
    }
}
