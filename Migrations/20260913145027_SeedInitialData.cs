using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolJournal_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "classes",
                columns: new[] { "id", "name", "teacher_name" },
                values: new object[,]
                {
                    { 1, "5А", "Иванова Мария Петровна" },
                    { 2, "5Б", "Петров Сергей Иванович" },
                    { 3, "9А", "Сидорова Анна Викторовна" }
                });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Математика" },
                    { 2, "Русский язык" },
                    { 3, "Физика" },
                    { 4, "История" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "class_id", "full_name" },
                values: new object[,]
                {
                    { 1, 1, "Алексеев Иван" },
                    { 2, 1, "Борисова Анна" },
                    { 3, 1, "Власов Пётр" },
                    { 4, 2, "Громова Ольга" },
                    { 5, 2, "Дмитриев Никита" },
                    { 6, 2, "Егорова Светлана" },
                    { 7, 3, "Жуков Артём" },
                    { 8, 3, "Захарова Дарья" },
                    { 9, 3, "Иванов Максим" },
                    { 10, 3, "Кузнецова Полина" }
                });

            migrationBuilder.InsertData(
                table: "grades",
                columns: new[] { "id", "date", "student_id", "subject_id", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 5 },
                    { 2, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 5 },
                    { 3, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 4 },
                    { 4, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, 5 },
                    { 5, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, 5 },
                    { 6, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 4 },
                    { 7, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 5 },
                    { 8, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 5 },
                    { 9, new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, 5 },
                    { 10, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 5 },
                    { 11, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 5 },
                    { 12, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 5 },
                    { 13, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 5 },
                    { 14, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 4 },
                    { 15, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1, 5 },
                    { 16, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 4 },
                    { 17, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 4 },
                    { 18, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 5 },
                    { 19, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 4 },
                    { 20, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, 4 },
                    { 21, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, 4 },
                    { 22, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 4 },
                    { 23, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 2, 4 },
                    { 24, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 3, 4 },
                    { 25, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 4 },
                    { 26, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 3 },
                    { 27, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 1, 3 },
                    { 28, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 3 },
                    { 29, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 4 },
                    { 30, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 3, 3 },
                    { 31, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 4, 3 },
                    { 32, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, 4 },
                    { 33, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, 4 },
                    { 34, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 3 },
                    { 35, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 2, 4 },
                    { 36, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 3, 3 },
                    { 37, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 4 },
                    { 38, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, 3 },
                    { 39, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, 4 },
                    { 40, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 4 },
                    { 41, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 2, 3 },
                    { 42, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 3, 4 },
                    { 43, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4, 4 },
                    { 44, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1, 5 },
                    { 45, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 2, 5 },
                    { 46, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 3, 5 },
                    { 47, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 4, 5 },
                    { 48, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1, 5 },
                    { 49, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 1, 4 },
                    { 50, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 2, 5 },
                    { 51, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 3, 4 },
                    { 52, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 4, 5 },
                    { 53, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, 3 },
                    { 54, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, 4 },
                    { 55, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 2, 3 },
                    { 56, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 3, 3 },
                    { 57, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 4, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "grades",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "subjects",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "classes",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
