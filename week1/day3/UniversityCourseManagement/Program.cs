using Microsoft.EntityFrameworkCore;
using UniversityCourseManagement;

using var db = new UniversityCourseDbContext();

db.Database.Migrate();

if (!db.Users.Any())
{
    SeedData(db);
}

RunAllQueries(db);

// Insert Data

static void SeedData(UniversityCourseDbContext db)
{
    // Add interns 
    var students = new List<User>
    {
        new User { UserName = "fawzy_su",   FirstName = "Fawzy",  LastName = "Sukkar",     EmailAddress = "fawzy@university.com",   PhoneNumber = "+963941374366", Role = "Student" },
        new User { UserName = "moaz_zk",    FirstName = "Moaz",   LastName = "Zakaria",    EmailAddress = "moaz@university.com",    PhoneNumber = "+963956857002", Role = "Student" },
        new User { UserName = "motaz_ma",   FirstName = "Motaz",  LastName = "Al Masri",   EmailAddress = "motaz@university.com",   PhoneNumber = "+963959493837", Role = "Student" },
        new User { UserName = "yehya_ms",   FirstName = "Yehya",  LastName = "Msouty",     EmailAddress = "yehya@university.com",   PhoneNumber = "+963982445158", Role = "Student" },
        new User { UserName = "muhamed_m",  FirstName = "Muhamed",LastName = "Mubaker",    EmailAddress = "muahmed@university.com", PhoneNumber = "+963941374366", Role = "Student" },
        new User { UserName = "hiba_ja",    FirstName = "Hiba",   LastName = "Jazba",      EmailAddress = "hiba@university.com",    PhoneNumber = "+905058615231", Role = "Student" },
        new User { UserName = "marah_al",   FirstName = "Marah",  LastName = "Aljumaat",   EmailAddress = "marah@university.com",   PhoneNumber = "+963957758165", Role = "Student" },
        new User { UserName = "aya_ja",     FirstName = "Aya",    LastName = "Jazba",      EmailAddress = "aya@university.com",     PhoneNumber = "+905528178511", Role = "Student" },
        new User { UserName = "zuhair_al",  FirstName = "Zuhair", LastName = "Alhomsi",    EmailAddress = "zuhair@university.com",  PhoneNumber = "+963992042713", Role = "Student" },
        new User { UserName = "mehyar_kh",  FirstName = "Mehyar", LastName = "Khuder",     EmailAddress = "mehyar@university.com",  PhoneNumber = "+963968378834", Role = "Student" },
        new User { UserName = "ahmad_kh",   FirstName = "Ahmad",  LastName = "Khaled",     EmailAddress = "ahmad@university.com",   PhoneNumber = "+963941374366", Role = "Student" },
        new User { UserName = "masa_ha",    FirstName = "Masa",   LastName = "Hammoud",    EmailAddress = "masa@university.com",    PhoneNumber = "+963947229978", Role = "Student" },
        new User { UserName = "ayman_du",   FirstName = "Ayman",  LastName = "Durra",      EmailAddress = "ayman@university.com",   PhoneNumber = "+963998640545", Role = "Student" },
        new User { UserName = "nawar_tb",   FirstName = "Nawar",  LastName = "Al Tibi",    EmailAddress = "nawar@university.com",   PhoneNumber = "+963957456941", Role = "Student" }
    };

    db.Users.AddRange(students);
    db.SaveChanges();
    Console.WriteLine($"Added {students.Count} students.");

    // Add teachers
    var sami = new User
    {
        UserName = "sami_hi",
        FirstName = "Sami",
        LastName = "Hijazi",
        EmailAddress = "sami@university.com",
        PhoneNumber = "+1(240)381-9639",
        Role = "Teacher"
    };

    var feryal = new User
    {
        UserName = "feryal_tl",
        FirstName = "Feryal",
        LastName = "Tlimat",
        EmailAddress = "feryal@university.com",
        PhoneNumber = "+905523381309",
        Role = "Teacher"
    };

    db.Users.AddRange(sami, feryal);
    db.SaveChanges();
    Console.WriteLine("Added 2 teachers: Sami and Feryal.");

    // Add courses 
    var courses = new List<Course>
    {
        new() { Title = "SQL", TeacherId = sami.Id },
        new() { Title = "C#", TeacherId = feryal.Id },
        new() { Title = "Entity Framework", TeacherId = sami.Id },
        new() { Title = "Web API", TeacherId = feryal.Id },
        new() { Title = "React", TeacherId = sami.Id }
    };

    db.Courses.AddRange(courses);
    db.SaveChanges();
    Console.WriteLine($"Added {courses.Count} courses.");

    // Add assignments 
    var random = new Random();
    var allAssignments = new List<Assignment>();

    foreach (var course in courses)
    {
        for (int i = 1; i <= 5; i++)
        {
            int daysOffset = random.Next(-90, 90);
            allAssignments.Add(new Assignment
            {
                Title = $"{course.Title} Assignment {i}",
                CourseId = course.Id,
                DueDate = DateTime.Now.AddDays(daysOffset)
            });
        }
    }

    db.Assignments.AddRange(allAssignments);
    db.SaveChanges();
    Console.WriteLine($"Added {allAssignments.Count} assignments (5 per course).");

    // Add comments 
    var commentTexts = new[]
    {
        "Need clarification",
        "Done successfully",
        "Assignment is difficult",
        "Submitted",
        "Need extension",
        "Working on it",
        "Finished",
        "Question about requirements",
        "Looks good",
        "Will submit tomorrow",
        "Great assignment!",
        "I need more time"
    };

    var allStudents = db.Users.Where(u => u.Role == "Student").ToList();

    for (int i = 0; i < 12; i++)
    {
        var assignment = allAssignments[random.Next(allAssignments.Count)];
        var student = allStudents[random.Next(allStudents.Count)];

        db.Comments.Add(new Comment
        {
            AssignmentId = assignment.Id,
            UserId = student.Id,
            Content = commentTexts[i % commentTexts.Length],
            CreatedDate = DateTime.Now.AddDays(-random.Next(1, 30))
        });
    }

    db.SaveChanges();
    Console.WriteLine("Added 12 comments.");

    // Add grades 
    var grades = new List<Grade>();

    foreach (var student in allStudents)
    {
        foreach (var assignment in allAssignments)
        {
            grades.Add(new Grade
            {
                StudentId = student.Id,
                AssignmentId = assignment.Id,
                Score = random.Next(0, 101)
            });
        }
    }

    db.Grades.AddRange(grades);
    db.SaveChanges();
    Console.WriteLine($"Added {grades.Count} grades.");


    // Add syllabus 
    var syllabusTexts = new[]
    {
        "Introduction to SQL, Queries, Joins, Stored Procedures",
        "C# Fundamentals, OOP, LINQ, Collections",
        "Entity Framework Core, Migrations, Relationships",
        "ASP.NET Web API, REST APIs, Authentication",
        "React Components, Hooks, Routing, State Management"
    };

    for (int i = 0; i < courses.Count; i++)
    {
        db.Syllabi.Add(new Syllabus
        {
            CourseId = courses[i].Id,
            Outline = syllabusTexts[i]
        });
    }

    db.SaveChanges();
    Console.WriteLine("Added syllabus for each course.");
}

// LINQ Queries

static void RunAllQueries(UniversityCourseDbContext db)
{
    Console.WriteLine("\n--- All Courses ---");
    foreach (var course in GetAllCourses(db))   
        Console.WriteLine($"  [{course.Id}] {course.Title} - Teacher: {course.Teacher.FirstName} {course.Teacher.LastName}");

    Console.WriteLine("\n--- Assignments for Course Id = 1 ---");
    foreach (var a in GetAssignmentsByCourseId(db, 1))
        Console.WriteLine($"  [{a.Id}] {a.Title} - Due: {a.DueDate:yyyy-MM-dd}");

    Console.WriteLine("\n--- All Students ---");
    foreach (var s in GetAllStudents(db))
        Console.WriteLine($"  [{s.Id}] {s.FirstName} {s.LastName} ({s.EmailAddress})");

    Console.WriteLine("\n--- Comments for Assignment Id = 1 ---");
    var comments = GetCommentsByAssignmentId(db, 1);
    if (comments.Count == 0)
        Console.WriteLine("  (no comments for assignment 1, showing first assignment with comments)");
    foreach (var c in comments.Count > 0 ? comments : db.Comments.Take(3).Include(c => c.User).ToList())
        Console.WriteLine($"  [{c.Id}] {c.User.FirstName}: {c.Content}");

    Console.WriteLine("\n--- Grades for Student Id = 1 ---");
    foreach (var g in GetGradesByStudentId(db, 1).Take(5))
        Console.WriteLine($"  Assignment {g.AssignmentId}: {g.Score} ({LetterGrade(g.Score)})");
    Console.WriteLine("  ...");

    Console.WriteLine("\n--- Assignments with Course Name and Teacher Full Name ---");
    foreach (var item in GetAssignmentsWithCourseAndTeacher(db).Take(8))
        Console.WriteLine($"  {item.Title} | Course: {item.CourseName} | Teacher: {item.TeacherName}");

    Console.WriteLine("\n--- Average Grade per Course ---");
    foreach (var item in GetAverageGradePerCourse(db))
        Console.WriteLine($"  {item.CourseName}: {item.Average:F2}");

    Console.WriteLine("\n--- Letter Grade Examples ---");
    int[] sampleScores = { 95, 85, 75, 65, 50 };
    foreach (int score in sampleScores)
        Console.WriteLine($"  Score {score} = {LetterGrade(score)}");

    Console.WriteLine("\n--- GPA for Student Id = 1 ---");
    double gpa = CalculateGpa(db, 1);
    Console.WriteLine($"  GPA: {gpa:F2}");

    Console.WriteLine("\n--- UPDATE: Change Student to Teacher ---");
    var studentToPromote = db.Users.FirstOrDefault(u => u.Role == "Student");
    if (studentToPromote != null)
    {
        Console.WriteLine($"  Before: {studentToPromote.FirstName} {studentToPromote.LastName} = {studentToPromote.Role}");
        UpdateUserRole(db, studentToPromote.Id, "Teacher");
        var updated = db.Users.Find(studentToPromote.Id);
        Console.WriteLine($"  After:  {updated!.FirstName} {updated.LastName} = {updated.Role}");
    }

    Console.WriteLine("\n--- DELETE: Delete a Comment ---");
    var commentToDelete = db.Comments.FirstOrDefault();
    if (commentToDelete != null)
    {
        int deletedId = commentToDelete.Id;
        DeleteComment(db, deletedId);
        Console.WriteLine($"  Deleted comment Id = {deletedId}");
    }
}

static List<Course> GetAllCourses(UniversityCourseDbContext db)
{
    return db.Courses
        .Include(c => c.Teacher)
        .ToList();
}

static List<Assignment> GetAssignmentsByCourseId(UniversityCourseDbContext db, int courseId)
{
    return db.Assignments
        .Where(a => a.CourseId == courseId)
        .ToList();
}

static List<User> GetAllStudents(UniversityCourseDbContext db)
{
    return db.Users
        .Where(u => u.Role == "Student")
        .ToList();
}

static List<Comment> GetCommentsByAssignmentId(UniversityCourseDbContext db, int assignmentId)
{
    return db.Comments
        .Include(c => c.User)
        .Where(c => c.AssignmentId == assignmentId)
        .ToList();
}

static List<Grade> GetGradesByStudentId(UniversityCourseDbContext db, int studentId)
{
    return db.Grades
        .Include(g => g.Assignment)
        .Where(g => g.StudentId == studentId)
        .ToList();
}

static List<(string Title, string CourseName, string TeacherName)> GetAssignmentsWithCourseAndTeacher(UniversityCourseDbContext db)
{
    return db.Assignments
        .Include(a => a.Course)
        .ThenInclude(c => c.Teacher)
        .Select(a => new ValueTuple<string, string, string>(
            a.Title,
            a.Course.Title,
            a.Course.Teacher.FirstName + " " + a.Course.Teacher.LastName))
        .ToList();
}

    static List<(string CourseName, double Average)> GetAverageGradePerCourse(UniversityCourseDbContext db)
{
    return db.Courses
        .Select(c => new ValueTuple<string, double>(
            c.Title,
            c.Assignments.SelectMany(a => a.Grades).Average(g => (double)g.Score)))
        .ToList();
}

// Methods

static string LetterGrade(int score)
{
    if (score >= 90) return "A";
    if (score >= 80) return "B";
    if (score >= 70) return "C";
    if (score >= 60) return "D";
    return "F";
}

static double ScoreToGpaPoints(int score)
{
    if (score >= 90) return 4.0;
    if (score >= 80) return 3.0;
    if (score >= 70) return 2.0;
    if (score >= 60) return 1.0;
    return 0.0;
}

static double CalculateGpa(UniversityCourseDbContext db, int studentId)
{
    var scores = db.Grades
        .Where(g => g.StudentId == studentId)
        .Select(g => g.Score)
        .ToList();

    if (scores.Count == 0)
        return 0;

    double totalPoints = 0;
    foreach (int score in scores)
        totalPoints += ScoreToGpaPoints(score);

    return totalPoints / scores.Count;
}

// Updates & Deletions

static void UpdateUserRole(UniversityCourseDbContext db, int userId, string newRole)
{
    var user = db.Users.Find(userId);
    if (user != null)
    {
        user.Role = newRole;
        db.SaveChanges();
    }
}

static void DeleteComment(UniversityCourseDbContext db, int commentId)
{
    var comment = db.Comments.Find(commentId);
    if (comment != null)
    {
        db.Comments.Remove(comment);
        db.SaveChanges();
    }
}
