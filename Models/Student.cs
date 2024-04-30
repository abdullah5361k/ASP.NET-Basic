using System.ComponentModel.DataAnnotations;

namespace Uni;

public class Student
{
    [Key]
    public int Id {get; set;}

    public string? Name {get; set;}

    public int RollNo {get;set;}

    public List<Semester> Semesters {get; set;} = new List<Semester>();
}
