using System.ComponentModel.DataAnnotations;

namespace demo14.DTO;

public record CourseRequest([Required, StringLength(45)] string Name, int Credit);
public record GradeRequest([Required, StringLength(45)] string Name, [Required, StringLength(45)] string Description);
public record StudentRequest(int Grade, [Required, StringLength(45)] string FirstName, [Required, StringLength(45)] string LastName, [Required, StringLength(45)] string Phone, [Required, EmailAddress, StringLength(45)] string Email);
public record IdRequest(int Id);
public record ContactRequest(int Id, [Required, StringLength(45)] string Phone, [Required, EmailAddress, StringLength(45)] string Email);
public record PersonalRequest(int Id, [Required, StringLength(45)] string FirstName, [Required, StringLength(45)] string LastName);
public record StudentItem(int IdStudent, int GradeIdGrade, string FirstName, string LastName, string Phone, string Email);
public record CourseItem(int IdCourse, string Name, int Credit);
public record StudentListRequest(int IdGrade, List<StudentItem> List);
public record CourseListRequest(List<CourseItem> List);
public record EnrollmentRequest(int IdStudent, List<CourseItem> List);
