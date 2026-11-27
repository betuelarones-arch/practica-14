using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo14.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    IdCourse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.IdCourse);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    IdGrade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.IdGrade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    IdStudent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeIdGrade = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.IdStudent);
                    table.ForeignKey(
                        name: "FK_Students_Grades_GradeIdGrade",
                        column: x => x.GradeIdGrade,
                        principalTable: "Grades",
                        principalColumn: "IdGrade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    IdEnrollment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentIdStudent = table.Column<int>(type: "int", nullable: false),
                    CourseIdCourse = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.IdEnrollment);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseIdCourse",
                        column: x => x.CourseIdCourse,
                        principalTable: "Courses",
                        principalColumn: "IdCourse",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentIdStudent",
                        column: x => x.StudentIdStudent,
                        principalTable: "Students",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseIdCourse",
                table: "Enrollments",
                column: "CourseIdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentIdStudent_CourseIdCourse",
                table: "Enrollments",
                columns: new[] { "StudentIdStudent", "CourseIdCourse" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_GradeIdGrade",
                table: "Students",
                column: "GradeIdGrade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Grades");
        }
    }
}
