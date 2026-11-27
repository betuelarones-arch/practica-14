using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace demo14.Models;

public class Grade
{
    public int IdGrade { get; set; }

    [Required, StringLength(45)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(45)]
    public string Description { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    [JsonIgnore]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
