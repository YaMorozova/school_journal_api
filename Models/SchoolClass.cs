using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolJournal.Api.Models;

[Table("classes")]
public class SchoolClass
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("teacher_name")]
    [MaxLength(255)]
    public string TeacherName { get; set; } = string.Empty;

    // Навигационное свойство: в классе много учеников
    public List<Student> Students { get; set; } = new();
}
