using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SchoolJournal.Api.Models;

[Table("students")]
public class Student
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("full_name")]
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;

    [Column("class_id")]
    public int ClassId { get; set; }

    // Навигационное свойство: класс ученика
    public SchoolClass Class { get; set; } = null!;

    // Навигационное свойство: все оценки ученика
    public List<Grade> Grades { get; set; } = new();
}
