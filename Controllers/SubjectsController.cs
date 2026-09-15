using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Api.Data;
using SchoolJournal.Api.Dtos;
using SchoolJournal.Api.Models;

namespace SchoolJournal.Api.Controllers;

/// <summary>
/// Операции с предметами: список, поиск, создание.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Конструктор контроллера.
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    public SubjectsController(AppDbContext db) => _db = db;

    // GET: api/subjects
    /// <summary>
    /// Возвращает список всех предметов.
    /// </summary>
    /// <response code="200">Список предметов успешно получен</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll()
    {
        var subjects = await _db.Subjects
            .OrderBy(s => s.Name)
            .Select(s => new SubjectDto(s.Id, s.Name))
            .ToListAsync();

        return Ok(subjects);
    }

    // GET: api/subjects/5
    /// <summary>
    /// Возвращает предмет по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор предмета</param>
    /// <response code="200">Предмет найден</response>
    /// <response code="404">Предмет с указанным идентификатором не найден</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<SubjectDto>> GetById(int id)
    {
        var subject = await _db.Subjects
            .Where(s => s.Id == id)
            .Select(s => new SubjectDto(s.Id, s.Name))
            .FirstOrDefaultAsync();

        if (subject is null)
            return NotFound(new { message = $"Предмет с id={id} не найден" });

        return Ok(subject);
    }

    // POST: api/subjects
    /// <summary>
    /// Создаёт новый предмет.
    /// </summary>
    /// <param name="dto">Данные нового предмета</param>
    /// <response code="201">Предмет успешно создан</response>
    /// <response code="400">Ошибка валидации входных данных</response>
    /// <response code="409">Предмет с таким названием уже существует</response>
    [HttpPost]
    public async Task<ActionResult<SubjectDto>> Create([FromBody] CreateSubjectDto dto)
    {
        if (await _db.Subjects.AnyAsync(s => s.Name == dto.Name))
            return BadRequest(new { message = $"Предмет '{dto.Name}' уже существует" });

        var subject = new Subject { Name = dto.Name };
        _db.Subjects.Add(subject);
        await _db.SaveChangesAsync();

        var result = new SubjectDto(subject.Id, subject.Name);
        return CreatedAtAction(nameof(GetById), new { id = subject.Id }, result);
    }
}
