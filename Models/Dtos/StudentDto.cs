using System.ComponentModel.DataAnnotations;

namespace Uni;



public class StudentDto : SemesterDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public int RollNo { get; set; }
    
}

