using Microsoft.EntityFrameworkCore;
using SchoolJournal.Api.Models;

namespace SchoolJournal.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SchoolClass> Classes => Set<SchoolClass>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связей

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Class)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Subject)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SchoolClass>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Subject>().HasIndex(s => s.Name).IsUnique();

        // Тестовые данные
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Классы
        modelBuilder.Entity<SchoolClass>().HasData(
            new SchoolClass { Id = 1, Name = "5А", TeacherName = "Иванова Мария Петровна" },
            new SchoolClass { Id = 2, Name = "5Б", TeacherName = "Петров Сергей Иванович" },
            new SchoolClass { Id = 3, Name = "9А", TeacherName = "Сидорова Анна Викторовна" }
        );

        // Предметы
        modelBuilder.Entity<Subject>().HasData(
            new Subject { Id = 1, Name = "Математика" },
            new Subject { Id = 2, Name = "Русский язык" },
            new Subject { Id = 3, Name = "Физика" },
            new Subject { Id = 4, Name = "История" }
        );

        // Ученики
        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Алексеев Иван", ClassId = 1 },
            new Student { Id = 2, FullName = "Борисова Анна", ClassId = 1 },
            new Student { Id = 3, FullName = "Власов Пётр", ClassId = 1 },
            new Student { Id = 4, FullName = "Громова Ольга", ClassId = 2 },
            new Student { Id = 5, FullName = "Дмитриев Никита", ClassId = 2 },
            new Student { Id = 6, FullName = "Егорова Светлана", ClassId = 2 },
            new Student { Id = 7, FullName = "Жуков Артём", ClassId = 3 },
            new Student { Id = 8, FullName = "Захарова Дарья", ClassId = 3 },
            new Student { Id = 9, FullName = "Иванов Максим", ClassId = 3 },
            new Student { Id = 10, FullName = "Кузнецова Полина", ClassId = 3 }
        );

        // Оценки
        var d = new DateTime(2026, 9, 1);
        int id = 1;

        void Add(int studentId, int subjectId, int value, int dayOffset)
        {
            modelBuilder.Entity<Grade>().HasData(
                new Grade
                {
                    Id = id++,
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Value = value,
                    Date = d.AddDays(dayOffset)
                });
        }

        // Ученик 1 (Алексеев Иван) — только 4 и 5
        Add(1, 1, 5, 1); Add(1, 1, 5, 15);
        Add(1, 2, 4, 2); Add(1, 2, 5, 16);
        Add(1, 3, 5, 3);
        Add(1, 4, 4, 4);

        // Ученик 2 (Борисова Анна) — только 5
        Add(2, 1, 5, 1); Add(2, 1, 5, 15); Add(2, 1, 5, 30);
        Add(2, 2, 5, 2); Add(2, 2, 5, 16);
        Add(2, 3, 5, 3);
        Add(2, 4, 5, 4);

        // Ученик 3 (Власов Пётр) — только 4 и 5
        Add(3, 1, 4, 1); Add(3, 1, 5, 15);
        Add(3, 2, 4, 2); Add(3, 2, 4, 16);
        Add(3, 3, 5, 3);
        Add(3, 4, 4, 4);

        // Ученик 4 (Громова Ольга) — только 4 и 5
        Add(4, 1, 4, 1); Add(4, 1, 4, 15);
        Add(4, 2, 4, 2); Add(4, 2, 4, 16);
        Add(4, 3, 4, 3);
        Add(4, 4, 4, 4);

        // Ученик 5 (Дмитриев Никита) — есть 3
        Add(5, 1, 3, 1); Add(5, 1, 3, 15);
        Add(5, 2, 3, 2); Add(5, 2, 4, 16);
        Add(5, 3, 3, 3);
        Add(5, 4, 3, 4);

        // Ученик 6 (Егорова Светлана) — только 3 и 4
        Add(6, 1, 4, 1); Add(6, 1, 4, 15);
        Add(6, 2, 3, 2); Add(6, 2, 4, 16);
        Add(6, 3, 3, 3);
        Add(6, 4, 4, 4);

        // Ученик 7 (Жуков Артём) — есть 3 и 5
        Add(7, 1, 3, 1); Add(7, 1, 4, 15);
        Add(7, 2, 4, 2); Add(7, 2, 3, 16);
        Add(7, 3, 4, 3);
        Add(7, 4, 4, 4);

        // Ученик 8 (Захарова Дарья) — только 5
        Add(8, 1, 5, 1);
        Add(8, 2, 5, 2);
        Add(8, 3, 5, 3);
        Add(8, 4, 5, 4);

        // Ученик 9 (Иванов Максим) — только 4 и 5
        Add(9, 1, 5, 1); Add(9, 1, 4, 15);
        Add(9, 2, 5, 2);
        Add(9, 3, 4, 3);
        Add(9, 4, 5, 4);

        // Ученик 10 (Кузнецова Полина) — есть 3
        Add(10, 1, 3, 1); Add(10, 1, 4, 15);
        Add(10, 2, 3, 2);
        Add(10, 3, 3, 3);
        Add(10, 4, 3, 4);
    }
}
