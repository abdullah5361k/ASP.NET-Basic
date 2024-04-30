using System.ComponentModel.DataAnnotations;

namespace Uni;

public class SemesterDto
{
    [Required]
    public string? SemesterNo { get; set; }
    [Required]
    public double GPA { get; set; }
}
