using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolJournal.Api.Models;

[Table("grades")]
public class Grade
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [Column("subject_id")]
    public int SubjectId { get; set; }

    [Column("value")]
    public int Value { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    // Навигационные свойства
    public Student Student { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
}
