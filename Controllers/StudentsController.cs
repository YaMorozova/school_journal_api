using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolJournal.Api.Data;
using SchoolJournal.Api.Dtos;
using SchoolJournal.Api.Models;

namespace SchoolJournal.Api.Controllers;

/// <summary>
/// Операции с учениками: список, поиск, фильтрация, создание, изменение, удаление.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _db;

    /// <summary>
    /// Конструктор контроллера. <see cref="AppDbContext"/> внедряется через DI.
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    public StudentsController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/students
    /// <summary>
    /// Возвращает список всех учеников с названием их класса.
    /// </summary>
    /// <response code="200">Список учеников</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
    {
        var students = await _db.Students
            .Include(s => s.Class)
            .OrderBy(s => s.FullName)
            .Select(s => new StudentDto(s.Id, s.FullName, s.ClassId, s.Class.Name))
            .ToListAsync();

        return Ok(students);
    }

    // GET: api/students/5
    /// <summary>
    /// Возвращает ученика по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор ученика</param>
    /// <response code="200">Найденный ученик</response>
    /// <response code="404">Ученик не найден</response>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _db.Students
            .Include(s => s.Class)
            .Where(s => s.Id == id)
            .Select(s => new StudentDto(s.Id, s.FullName, s.ClassId, s.Class.Name))
            .FirstOrDefaultAsync();

        if (student is null)
            return NotFound(new { message = $"Ученик с id={id} не найден" });

        return Ok(student);
    }

    /// <summary>
    /// Возвращает учеников, которые учатся только на 4 и 5.
    /// </summary>
    /// <remarks>Реализует запрос 2a из задания.</remarks>
    /// <response code="200">Список отличников и хорошистов</response>
    [HttpGet("excellent-only")]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetExcellentOnly()
    {
        var students = await _db.Students
            .Include(s => s.Class)
            .Where(s => s.Grades.Any() && s.Grades.All(g => g.Value >= 4))
            .OrderBy(s => s.FullName)
            .Select(s => new StudentDto(s.Id, s.FullName, s.ClassId, s.Class.Name))
            .ToListAsync();

        return Ok(students);
    }

    /// <summary>
    /// Возвращает учеников, средний балл которых находится строго между min и max.
    /// </summary>
    /// <param name="min">Нижняя граница среднего балла (не включительно). По умолчанию 3.5.</param>
    /// <param name="max">Верхняя граница среднего балла (не включительно). По умолчанию 4.5.</param>
    /// <remarks>Реализует запрос 2b из задания.</remarks>
    /// <response code="200">Список учеников с их средними баллами</response>
    /// <response code="400">Некорректный диапазон (min &gt;= max)</response>
    [HttpGet("average-between")]
    public async Task<ActionResult<IEnumerable<StudentWithAverageDto>>> GetByAverage(
        [FromQuery] double min = 3.5,
        [FromQuery] double max = 4.5)
    {
        var students = await _db.Students
            .Include(s => s.Class)
            .Where(s => s.Grades.Any())
            .Select(s => new
            {
                s.Id,
                s.FullName,
                ClassName = s.Class.Name,
                Average = s.Grades.Average(g => (double)g.Value)
            })
            .Where(x => x.Average > min && x.Average < max)
            .OrderByDescending(x => x.Average)
            .Select(x => new StudentWithAverageDto(x.Id, x.FullName, x.ClassName, x.Average))
            .ToListAsync();

        return Ok(students);
    }

    // POST: api/students
    /// <summary>
    /// Создаёт нового ученика.
    /// </summary>
    /// <param name="dto">Данные нового ученика</param>
    /// <response code="201">Ученик успешно создан. В заголовке Location — ссылка на созданный ресурс.</response>
    /// <response code="400">Ошибка валидации или указанный класс не существует</response>
    /// <response code="409">Ученик с таким ФИО уже есть в указанном классе</response>
    [HttpPost]
    public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto dto)
    {
        // Проверка: класс существует?
        var schoolClass = await _db.Classes.FindAsync(dto.ClassId);
        if (schoolClass is null)
            return BadRequest(new { message = $"Класс с id={dto.ClassId} не найден" });

        // Дополнительно: не создаём дубликата ученика с тем же ФИО в том же классе
        var duplicate = await _db.Students
            .AnyAsync(s => s.FullName == dto.FullName && s.ClassId == dto.ClassId);
        if (duplicate)
            return Conflict(new { message = $"Ученик '{dto.FullName}' уже есть в классе '{schoolClass.Name}'" });

        var student = new Student
        {
            FullName = dto.FullName.Trim(),
            ClassId = dto.ClassId
        };

        _db.Students.Add(student);
        await _db.SaveChangesAsync();

        var result = new StudentDto(student.Id, student.FullName, student.ClassId, schoolClass.Name);
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, result);
    }

    // PUT: api/students/5
    /// <summary>
    /// Обновляет данные существующего ученика.
    /// </summary>
    /// <param name="id">Идентификатор ученика</param>
    /// <param name="dto">Новые данные</param>
    /// <response code="204">Данные успешно обновлены</response>
    /// <response code="400">Ошибка валидации или указанный класс не существует</response>
    /// <response code="404">Ученик не найден</response>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
    {
        var student = await _db.Students.FindAsync(id);
        if (student is null)
            return NotFound(new { message = $"Ученик с id={id} не найден" });

        var schoolClass = await _db.Classes.FindAsync(dto.ClassId);
        if (schoolClass is null)
            return BadRequest(new { message = $"Класс с id={dto.ClassId} не найден" });

        student.FullName = dto.FullName;
        student.ClassId = dto.ClassId;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/students/5
    /// <summary>
    /// Удаляет ученика по идентификатору.
    /// Вместе с учеником удаляются все его оценки.
    /// </summary>
    /// <param name="id">Идентификатор ученика</param>
    /// <response code="204">Ученик удалён</response>
    /// <response code="404">Ученик не найден</response>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _db.Students.FindAsync(id);
        if (student is null)
            return NotFound(new { message = $"Ученик с id={id} не найден" });

        _db.Students.Remove(student);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
