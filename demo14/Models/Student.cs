using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo14.Models;

public class Student
{
    public int IdStudent { get; set; }
    public int GradeIdGrade { get; set; }

    [Required, StringLength(45)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(45)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(45)]
    public string Phone { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(45)]
    public string Email { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public Grade? Grade { get; set; }

    [JsonIgnore]
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
