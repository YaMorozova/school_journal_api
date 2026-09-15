using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Api.Data;
using SchoolJournal.Api.Dtos;
using SchoolJournal.Api.Models;

namespace SchoolJournal.Api.Controllers;

/// <summary>
/// Операции с классами: список, поиск, сортировка по среднему баллу,
/// создание и удаление.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Конструктор контроллера. <see cref="AppDbContext"/> внедряется через DI.
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    public ClassesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/classes
    /// <summary>
    /// Возвращает список всех классов.
    /// </summary>
    /// <response code="200">Список классов успешно получен</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClassDto>>> GetAll()
    {
        var classes = await _db.Classes
            .OrderBy(c => c.Name)
            .Select(c => new ClassDto(c.Id, c.Name, c.TeacherName))
            .ToListAsync();

        return Ok(classes);
    }

    // GET: api/classes/5
    /// <summary>
    /// Возвращает класс по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор класса</param>
    /// <response code="200">Класс найден</response>
    /// <response code="404">Класс с указанным идентификатором не найден</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClassDto>> GetById(int id)
    {
        var cls = await _db.Classes
            .Where(c => c.Id == id)
            .Select(c => new ClassDto(c.Id, c.Name, c.TeacherName))
            .FirstOrDefaultAsync();

        if (cls is null)
            return NotFound(new { message = $"Класс с id={id} не найден" });

        return Ok(cls);
    }

    // GET: api/classes/by-average-desc
    /// <summary>
    /// Возвращает список классов, отсортированных по убыванию среднего балла
    /// всех оценок всех учеников класса.
    /// </summary>
    /// <response code="200">Список классов со средними баллами</response>
    [HttpGet("by-average-desc")]
    public async Task<ActionResult<IEnumerable<ClassWithAverageDto>>> GetByAverageDesc()
    {
        var classes = await _db.Classes
            .Select(c => new
            {
                c.Id,
                c.Name,
                c.TeacherName,
                Average = c.Students
                    .SelectMany(s => s.Grades)
                    .Average(g => (double?)g.Value) ?? 0
            })
            .OrderByDescending(x => x.Average)
            .Select(x => new ClassWithAverageDto(x.Id, x.Name, x.TeacherName, x.Average))
            .ToListAsync();

        return Ok(classes);
    }

    /// <summary>
    /// Создаёт новый класс.
    /// </summary>
    /// <param name="dto">Данные нового класса</param>
    /// <response code="201">Класс успешно создан</response>
    /// <response code="400">Ошибка валидации входных данных</response>
    /// <response code="409">Класс с таким именем уже существует</response>
    // POST: api/classes
    [HttpPost]
    public async Task<ActionResult<ClassDto>> Create([FromBody] CreateClassDto dto)
    {
        if (await _db.Classes.AnyAsync(c => c.Name == dto.Name))
            return Conflict(new { message = $"Класс '{dto.Name}' уже существует" });

        var cls = new SchoolClass
        {
            Name = dto.Name.Trim(),
            TeacherName = dto.TeacherName.Trim()
        };

        _db.Classes.Add(cls);
        await _db.SaveChangesAsync();

        var result = new ClassDto(cls.Id, cls.Name, cls.TeacherName);
        return CreatedAtAction(nameof(GetById), new { id = cls.Id }, result);
    }

    // DELETE: api/classes/5
    /// <summary>
    /// Удаляет класс по идентификатору. Удаление запрещено, если в классе есть ученики.
    /// </summary>
    /// <param name="id">Идентификатор класса</param>
    /// <response code="204">Класс удалён</response>
    /// <response code="404">Класс не найден</response>
    /// <response code="409">В классе есть ученики — удаление невозможно</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cls = await _db.Classes.FindAsync(id);
        if (cls is null)
            return NotFound(new { message = $"Класс с id={id} не найден" });

        // Проверка: есть ли в классе ученики?
        var hasStudents = await _db.Students.AnyAsync(s => s.ClassId == id);
        if (hasStudents)
            return Conflict(new { message = "Нельзя удалить класс, в котором есть ученики" });

        _db.Classes.Remove(cls);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
