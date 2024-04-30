using System.Data.Common;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Uni;

[Route("api/uni")]
[ApiController]
public class UniApiController(UniDbContext db): ControllerBase
{
    private readonly UniDbContext _db = db;
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateStudent([FromBody] StudentDto newStudent)
    {
        if(newStudent.RollNo == 0)
        {
            return BadRequest("Provide valid rollNo");
        }
        if(newStudent.GPA > 4.0)
        {
            return BadRequest("Please provid valid GPA");
        }
        var alreadyExists = _db.Students.FirstOrDefault(st => st.RollNo == newStudent.RollNo);
        if(alreadyExists != null)  
        {
            return BadRequest("Student with this rollNo already exists!");
        }
        var student = new Student
        {
            Name = newStudent.Name,
            RollNo = newStudent.RollNo,
            Semesters = new List<Semester>
            {
                new Semester {SemesterNo = newStudent.SemesterNo, GPA = newStudent.GPA}
            }
        };
        _db.Students.Add(student);
        _db.SaveChanges();
        return Ok();
    }

    [HttpGet("{roll:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetStudentCgpa(int roll)
    {
       Student? student =  _db.Students.Include(s => s.Semesters).FirstOrDefault(s => s.RollNo == roll);
       int semestersCount = student!.Semesters.Count;
       double gpa = 0;
       foreach (var semester in student.Semesters) 
        {
            gpa += semester.GPA;
        }
       return Ok(student.Name + "  " +  gpa / semestersCount );
    }

    [HttpPost("{roll:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult AddGpa(int roll, SemesterDto semester)
    {
        Student? student =  _db.Students.FirstOrDefault(s => s.RollNo == roll);
        if(student == null)  
        {
            return BadRequest("Student with this rollNo not exists!");
        }
        var newSemester = new Semester
        {
            SemesterNo = semester.SemesterNo,
            GPA = semester.GPA,
            StudentId = student!.Id
        };
        student.Semesters.Add(newSemester);
        _db.SaveChanges();
        return NoContent();
    }

}
