# School Journal API

Web API для журнала оценок.

**Стек:** ASP.NET Core 8, SQLite, Swagger, OpenAPI 3.0.

---

## Содержание

- [Описание](#описание)
- [Структура БД](#структура-бд)
- [Запуск проекта](#запуск-проекта)
- [API](#api)
- [SQL-запросы из задания](#sql-запросы-из-задания)
- [Валидация и обработка ошибок](#валидация-и-обработка-ошибок)

---

## Описание

Сервис хранит информацию об успеваемости учеников: ФИО ученика, номер класса,
предмет, оценка, дата выставления оценки, ФИО классного руководителя.

Реализовано как REST API с автоматически генерируемой OpenAPI-спецификацией
(доступна через Swagger UI).

**Основные возможности:**

- CRUD-операции над учениками, классами, предметами и оценками.
- Три аналитических эндпоинта (см. раздел [API](#api)).
- Валидация входных данных (атрибуты + бизнес-проверки).
- Автогенерируемая документация OpenAPI / Swagger.

---

## Структура БД

### ER-диаграмма

![Схема БД](school_db.jpeg)

### Таблицы

#### `classes` — классы

| Поле | Тип | Ограничения | Описание |
|---|---|---|---|
| `id` | INTEGER | PK, autoincrement | Идентификатор |
| `name` | VARCHAR(20) | NOT NULL, UNIQUE | Название класса (5А, 9Б, ...) |
| `teacher_name` | VARCHAR(255) | NOT NULL | ФИО классного руководителя |

#### `students` — ученики

| Поле | Тип | Ограничения | Описание |
|---|---|---|---|
| `id` | INTEGER | PK, autoincrement | Идентификатор |
| `full_name` | VARCHAR(255) | NOT NULL | ФИО ученика |
| `class_id` | INTEGER | FK → `classes.id`, NOT NULL | Класс ученика |

#### `subjects` — предметы

| Поле | Тип | Ограничения | Описание |
|---|---|---|---|
| `id` | INTEGER | PK, autoincrement | Идентификатор |
| `name` | VARCHAR(100) | NOT NULL, UNIQUE | Название предмета |

#### `grades` — оценки

| Поле | Тип | Ограничения | Описание |
|---|---|---|---|
| `id` | INTEGER | PK, autoincrement | Идентификатор |
| `student_id` | INTEGER | FK → `students.id`, NOT NULL | Ученик |
| `subject_id` | INTEGER | FK → `subjects.id`, NOT NULL | Предмет |
| `value` | INTEGER | NOT NULL | Оценка (2–5) |
| `date` | DATE | NOT NULL | Дата выставления |

### Связи

- `classes 1 — M students` — в одном классе много учеников.
- `students 1 — M grades` — у одного ученика много оценок.
- `subjects 1 — M grades` — по одному предмету много оценок.

### Правила удаления

- Удаление класса запрещено, если в нём есть ученики.
- Удаление ученика удаляет все его оценки.
- Удаление предмета запрещено, если по нему есть оценки.

---

## Запуск проекта

### Требования

- .NET 8 SDK
- Опционально: Visual Studio 2022 или VS Code

### Через командную строку

```bash
git clone https://github.com/YaMorozova/school_journal_api.git
cd school_journal__api
dotnet restore
dotnet run
```

Открыть в браузере: **https://localhost:7125/swagger**

### Через Visual Studio

1. Открыть файл `SchoolJournal.Api.sln`.
3. Нажать **F5**.

Swagger откроется автоматически.

### Тестовые данные

База наполняется тестовыми данными автоматически при миграции:

- 3 класса: 5А, 5Б, 9А
- 10 учеников
- 4 предмета: Математика, Русский язык, Физика, История
- 57 оценок

---

## API

### Ученики — `/api/students`

| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/students` | Список всех учеников |
| GET | `/api/students/{id}` | Ученик по идентификатору |
| GET | `/api/students/excellent-only` | **Ученики, которые учатся только на 4 и 5** |
| GET | `/api/students/average-between?min=3.5&max=4.5` | **Ученики со средним баллом в диапазоне** |
| POST | `/api/students` | Создать ученика |
| PUT | `/api/students/{id}` | Обновить ученика |
| DELETE | `/api/students/{id}` | Удалить ученика |

### Классы — `/api/classes`

| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/classes` | Список всех классов |
| GET | `/api/classes/{id}` | Класс по идентификатору |
| GET | `/api/classes/by-average-desc` | **Классы по убыванию среднего балла** |
| POST | `/api/classes` | Создать класс |
| DELETE | `/api/classes/{id}` | Удалить класс (если нет учеников) |

### Предметы — `/api/subjects`

| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/subjects` | Список предметов |
| GET | `/api/subjects/{id}` | Предмет по идентификатору |
| POST | `/api/subjects` | Создать предмет |

### Оценки — `/api/grades`

| Метод | Путь | Описание |
|---|---|---|
| GET | `/api/grades` | Список всех оценок |
| GET | `/api/grades/by-student/{studentId}` | Оценки конкретного ученика |
| POST | `/api/grades` | Выставить оценку |

### Примеры запросов

**Создать ученика:**

```http
POST /api/students
Content-Type: application/json

{
  "fullName": "Сидоров Пётр",
  "classId": 1
}
```

**Ответ (201 Created):**

```json
{
  "id": 11,
  "fullName": "Сидоров Пётр",
  "classId": 1,
  "className": "5А"
}
```

**Выставить оценку:**

```http
POST /api/grades
Content-Type: application/json

{
  "studentId": 1,
  "subjectId": 2,
  "value": 5,
  "date": "2026-09-15"
}
```

---

## SQL-запросы

### Ученики, которые учатся только на 4 и 5

```sql
SELECT s.id, s.full_name, c.name AS class_name
FROM students s
JOIN classes c ON c.id = s.class_id
JOIN grades g ON g.student_id = s.id
GROUP BY s.id, s.full_name, c.name
HAVING MIN(g.value) >= 4
ORDER BY s.full_name;
```

**Эндпоинт:** `GET /api/students/excellent-only`

### Ученики со средним баллом > 3.5 и < 4.5

```sql
SELECT
    s.id,
    s.full_name,
    c.name AS class_name,
    AVG(g.value) AS average_grade
FROM students s
JOIN classes c ON c.id = s.class_id
JOIN grades g ON g.student_id = s.id
GROUP BY s.id, s.full_name, c.name
HAVING AVG(g.value) > 3.5 AND AVG(g.value) < 4.5
ORDER BY average_grade DESC;
```

**Эндпоинт:** `GET /api/students/average-between?min=3.5&max=4.5`

### Классы, отсортированные по убыванию среднего балла

```sql
SELECT
    c.id,
    c.name AS class_name,
    c.teacher_name,
    AVG(g.value) AS average_grade
FROM classes c
JOIN students s ON s.class_id = c.id
JOIN grades g ON g.student_id = s.id
GROUP BY c.id, c.name, c.teacher_name
ORDER BY average_grade DESC;
```

**Эндпоинт:** `GET /api/classes/by-average-desc`

---

## Валидация и обработка ошибок

### Валидация входных данных

Реализована на двух уровнях:

1. **Атрибуты валидации** на DTO (`[Required]`, `[StringLength]`, `[Range]`) —
   срабатывают автоматически благодаря `[ApiController]`. При невалидной
   модели возвращается `400 Bad Request` со списком ошибок.
2. **Бизнес-проверки** в контроллерах:
   - существование связанных сущностей (класс, ученик, предмет);
   - уникальность (класс с таким именем, ученик с таким ФИО в классе, предмет с таким названием);
   - оценка в диапазоне 1–5;
   - дата оценки не в будущем;
   - запрет удаления класса с учениками.

### Коды ответов

| Код | Когда |
|---|---|
| 200 OK | Успешный GET |
| 201 Created | Успешный POST |
| 204 No Content | Успешный PUT / DELETE |
| 400 Bad Request | Ошибка валидации, несуществующий FK, некорректный диапазон |
| 404 Not Found | Сущность не найдена |
| 409 Conflict | Дубликат или запрет удаления |
| 500 Internal Server Error | Непредвиденная ошибка |
