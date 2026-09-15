using System.ComponentModel.DataAnnotations;

namespace SchoolJournal.Api.Dtos;

// Чтение
public record ClassDto(int Id, string Name, string TeacherName);
public record ClassWithAverageDto(int Id, string Name, string TeacherName, double AverageGrade);
public record StudentDto(int Id, string FullName, int ClassId, string ClassName);
public record StudentWithAverageDto(int Id, string FullName, string ClassName, double AverageGrade);
public record SubjectDto(int Id, string Name);
public record GradeDto(
    int Id,
    int StudentId,
    string StudentName,
    int SubjectId,
    string SubjectName,
    int Value,
    DateTime Date);

// Создание ученика
public class CreateStudentDto
{
    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "ФИО должно быть от 2 до 255 символов")]
    public string FullName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "ClassId должен быть положительным числом")]
    public int ClassId { get; set; }
}

// Обновление ученика
public class UpdateStudentDto
{
    [Required(ErrorMessage = "ФИО обязательно")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "ФИО должно быть от 2 до 255 символов")]
    public string FullName { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "ClassId должен быть положительным числом")]
    public int ClassId { get; set; }
}

// Создание класса
public class CreateClassDto
{
    [Required(ErrorMessage = "Название класса обязательно")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "Название класса от 1 до 20 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "ФИО классного руководителя обязательно")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "ФИО руководителя от 2 до 255 символов")]
    public string TeacherName { get; set; } = string.Empty;
}

// Создание предмета
public class CreateSubjectDto
{
    [Required(ErrorMessage = "Название предмета обязательно")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Название предмета от 2 до 100 символов")]
    public string Name { get; set; } = string.Empty;
}

// Создание оценки
public class CreateGradeDto
{
    [Range(1, int.MaxValue, ErrorMessage = "StudentId должен быть положительным числом")]
    public int StudentId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "SubjectId должен быть положительным числом")]
    public int SubjectId { get; set; }

    [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5")]
    public int Value { get; set; }

    [Required(ErrorMessage = "Дата обязательна")]
    public DateTime Date { get; set; }
}
