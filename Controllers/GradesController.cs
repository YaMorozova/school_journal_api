using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Api.Data;
using SchoolJournal.Api.Dtos;
using SchoolJournal.Api.Models;

namespace SchoolJournal.Api.Controllers;

/// <summary>
/// Операции с оценками: полный список, оценки конкретного ученика,
/// выставление новой оценки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Конструктор контроллера.
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    public GradesController(AppDbContext db) => _db = db;

    // GET: api/grades
    /// <summary>
    /// Возвращает список всех оценок с именами учеников и предметов,
    /// отсортированный по дате выставления (сначала самые свежие).
    /// </summary>
    /// <response code="200">Список оценок успешно получен</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GradeDto>>> GetAll()
    {
        var grades = await _db.Grades
            .OrderByDescending(g => g.Date)
            .Select(g => new GradeDto(
                g.Id,
                g.StudentId,
                g.Student.FullName,
                g.SubjectId,
                g.Subject.Name,
                g.Value,
                g.Date))
            .ToListAsync();

        return Ok(grades);
    }

    // GET: api/grades/by-student/5
    /// <summary>
    /// Возвращает список оценок конкретного ученика,
    /// отсортированный по дате выставления (сначала самые свежие).
    /// </summary>
    /// <param name="studentId">Идентификатор ученика</param>
    /// <response code="200">Список оценок ученика</response>
    /// <response code="404">Ученик с указанным идентификатором не найден</response>
    [HttpGet("by-student/{studentId:int}")]
    public async Task<ActionResult<IEnumerable<GradeDto>>> GetByStudent(int studentId)
    {
        var grades = await _db.Grades
            .Where(g => g.StudentId == studentId)
            .OrderByDescending(g => g.Date)
            .Select(g => new GradeDto(
                g.Id,
                g.StudentId,
                g.Student.FullName,
                g.SubjectId,
                g.Subject.Name,
                g.Value,
                g.Date))
            .ToListAsync();

        return Ok(grades);
    }

    // POST: api/grades
    /// <summary>
    /// Выставляет новую оценку ученику по предмету.
    /// </summary>
    /// <param name="dto">Данные новой оценки</param>
    /// <response code="201">Оценка успешно выставлена</response>
    /// <response code="400">Ошибка валидации, несуществующий ученик/предмет или дата в будущем</response>
    [HttpPost]
    public async Task<ActionResult<GradeDto>> Create([FromBody] CreateGradeDto dto)
    {
        // Проверка: оценка в допустимом диапазоне (дублирует атрибут, но пусть будет)
        if (dto.Value < 2 || dto.Value > 5)
            return BadRequest(new { message = "Оценка должна быть от 2 до 5" });

        var student = await _db.Students.FindAsync(dto.StudentId);
        if (student is null)
            return BadRequest(new { message = $"Ученик с id={dto.StudentId} не найден" });

        var subject = await _db.Subjects.FindAsync(dto.SubjectId);
        if (subject is null)
            return BadRequest(new { message = $"Предмет с id={dto.SubjectId} не найден" });

        // Проверка: дата не в будущем
        if (dto.Date.Date > DateTime.UtcNow.Date)
            return BadRequest(new { message = "Дата выставления не может быть в будущем" });

        var grade = new Grade
        {
            StudentId = dto.StudentId,
            SubjectId = dto.SubjectId,
            Value = dto.Value,
            Date = dto.Date
        };

        _db.Grades.Add(grade);
        await _db.SaveChangesAsync();

        var result = new GradeDto(
            grade.Id,
            grade.StudentId,
            student.FullName,
            grade.SubjectId,
            subject.Name,
            grade.Value,
            grade.Date);

        return CreatedAtAction(nameof(GetAll), result);
    }
}
