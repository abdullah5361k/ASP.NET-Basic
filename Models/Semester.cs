using System.ComponentModel.DataAnnotations;

namespace Uni;

public class Semester
{
    [Key]
    public int Id {get; set;}

    public string? SemesterNo {get; set;}

    public double GPA {get; set;}

    public int StudentId {get; set;}

     public Student? Student { get; set; } // Navigation property to Student

}
