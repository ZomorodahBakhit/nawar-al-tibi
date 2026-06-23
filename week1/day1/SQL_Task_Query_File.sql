
CREATE DATABASE University;
GO

USE University;
GO

-- Users 
CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    UserName VARCHAR(64) NOT NULL,
    FirstName VARCHAR(64) NOT NULL,
    LastName VARCHAR(64) NOT NULL,
    EmailAddress VARCHAR(128) NOT NULL UNIQUE, 
    PhoneNumber VARCHAR(16) NOT NULL,
    Role VARCHAR(32) NOT NULL
);

-- Courses
CREATE TABLE Courses (
    CourseId INT PRIMARY KEY,
    CourseName VARCHAR(100) NOT NULL,
    TeacherId INT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    SyllabusId INT NULL
);


-- Assignments
CREATE TABLE Assignments (
    AssignmentId INT PRIMARY KEY,
    CourseId INT NOT NULL,
    AssignmentTitle VARCHAR(128) NOT NULL,
    Description TEXT NULL,
    Weight FLOAT NOT NULL,
    MaxGrade INT NOT NULL,
    DueDate DATE NOT NULL
);

-- Comments
CREATE TABLE Comments (
    CommentId INT PRIMARY KEY,
    AssignmentId INT NOT NULL,
    CreatedByUserId INT NOT NULL,
    CreatedDate DATETIME NOT NULL,
    CommentContent TEXT NULL
);


-- Grades
CREATE TABLE Grades (
    GradeId INT PRIMARY KEY,
    AssignmentId INT NOT NULL,
    StudentId INT NOT NULL,
    Grade INT NULL
);

-- Syllabus 
CREATE TABLE Syllabus (
    SyllabusId INT PRIMARY KEY,
    Description TEXT NULL
);

USE University;
GO

INSERT INTO Users (UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
VALUES 
(1, 'fawzy_su', 'Fawzy', 'Sukkar', 'fawzy@university.com', '+963941374366', 'Student'),
(2, 'moaz_zk', 'Moaz', 'Zakaria', 'moaz@university.com', '+963956857002', 'Student'),
(3, 'motaz_ma', 'Motaz', 'Al Masri', 'motaz@university.com', '+963959493837', 'Student'),
(4, 'yehya_ms', 'Yehya', 'Msouty', 'yehya@university.com', '+963982445158', 'Student'),
(5, 'muhamed_m', 'Muhamed', 'Mubaker', 'muahmed@university.com', '+963941374366', 'Student'),
(6, 'hiba_ja', 'Hiba', 'Jazba', 'hiba@university.com', '+905058615231', 'Student'),
(7, 'marah_al', 'Marah', 'Aljumaat', 'marah@university.com', '+963957758165', 'Student'),
(8, 'aya_ja', 'Aya', 'Jazba', 'aya@university.com', '+905528178511', 'Student'),
(9, 'zuhair_al', 'Zuhair', 'Alhomsi', 'zuhair@university.com', '+963992042713', 'Student'),
(10, 'mehyar_kh', 'Mehyar', 'Khuder', 'mehyar@university.com', '+963968378834', 'Student'),
(11, 'ahmad_kh', 'Ahmad', 'Khaled', 'ahmad@university.com', '+963941374366', 'Student'),
(12, 'masa_ha', 'Masa', 'Hammoud', 'masa@university.com', '+963947229978', 'Student'),
(13, 'ayman_du', 'Ayman', 'Durra', 'ayman@university.com', '+963998640545', 'Student'),
(14, 'nawar_tb', 'Nawar', 'Al Tibi', 'nawar@university.com', '+963957456941', 'Student');


INSERT INTO Users (UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
VALUES 
(15, 'sami_hi', 'Sami', 'Hijazi', 'sami@university.com', '+1(240)381-9639', 'Teacher'),
(16, 'feryal_tl', 'Feryal', 'Tlimat', 'feryal@university.com', '+905523381309', 'Teacher');

INSERT INTO Syllabus (SyllabusId, Description)
VALUES
(1, 'Introduction to SQL, Queries, Joins, Stored Procedures'),
(2, 'C# Fundamentals, OOP, LINQ, Collections'),
(3, 'Entity Framework Core, Migrations, Relationships'),
(4, 'ASP.NET Web API, REST APIs, Authentication'),
(5, 'React Components, Hooks, Routing, State Management');


INSERT INTO Courses
(CourseId, CourseName, TeacherId, StartDate, EndDate, SyllabusId)
VALUES
(1,'SQL',15,'2026-02-01','2026-06-01',1),
(2,'C#',16,'2026-02-01','2026-06-01',2),
(3,'Entity Framework',15,'2026-02-15','2026-06-15',3),
(4,'Web API',16,'2026-03-01','2026-07-01',4),
(5,'React',15,'2026-03-15','2026-07-15',5);


INSERT INTO Assignments
(AssignmentId, CourseId, AssignmentTitle, Description, Weight, MaxGrade, DueDate)
VALUES

-- SQL
(1,1,'SQL Assignment 1','Basic Select Queries',20,100,'2026-01-10'),
(2,1,'SQL Assignment 2','Joins',20,100,'2026-03-10'),
(3,1,'SQL Assignment 3','Views',20,100,'2025-12-15'),
(4,1,'SQL Assignment 4','Stored Procedures',20,100,'2026-04-15'),
(5,1,'SQL Assignment 5','Functions',20,100,'2026-07-01'),

-- C#
(6,2,'C# Assignment 1','Variables',20,100,'2026-02-12'),
(7,2,'C# Assignment 2','Classes',20,100,'2026-03-22'),
(8,2,'C# Assignment 3','LINQ',20,100,'2025-11-18'),
(9,2,'C# Assignment 4','Collections',20,100,'2026-04-20'),
(10,2,'C# Assignment 5','Interfaces',20,100,'2026-06-20'),

-- EF
(11,3,'EF Assignment 1','DbContext',20,100,'2026-02-01'),
(12,3,'EF Assignment 2','Migrations',20,100,'2026-04-10'),
(13,3,'EF Assignment 3','Relationships',20,100,'2025-12-12'),
(14,3,'EF Assignment 4','Repositories',20,100,'2026-05-01'),
(15,3,'EF Assignment 5','Tracking',20,100,'2026-06-15'),

-- API
(16,4,'API Assignment 1','Controllers',20,100,'2026-03-01'),
(17,4,'API Assignment 2','CRUD',20,100,'2025-12-30'),
(18,4,'API Assignment 3','JWT',20,100,'2026-05-10'),
(19,4,'API Assignment 4','Swagger',20,100,'2026-06-01'),
(20,4,'API Assignment 5','Validation',20,100,'2026-07-20'),

-- React
(21,5,'React Assignment 1','Components',20,100,'2026-02-14'),
(22,5,'React Assignment 2','Hooks',20,100,'2026-03-25'),
(23,5,'React Assignment 3','State',20,100,'2025-11-11'),
(24,5,'React Assignment 4','Routing',20,100,'2026-06-20'),
(25,5,'React Assignment 5','Context API',20,100,'2026-08-01');


INSERT INTO Comments
(CommentId, AssignmentId, CreatedByUserId, CreatedDate, CommentContent)
VALUES
(1,2,1,GETDATE(),'Need clarification'),
(2,7,5,GETDATE(),'Done successfully'),
(3,10,2,GETDATE(),'Assignment is difficult'),
(4,12,9,GETDATE(),'Submitted'),
(5,18,6,GETDATE(),'Need extension'),
(6,20,11,GETDATE(),'Working on it'),
(7,24,4,GETDATE(),'Finished'),
(8,15,8,GETDATE(),'Question about requirements'),
(9,6,12,GETDATE(),'Looks good'),
(10,3,7,GETDATE(),'Will submit tomorrow');


INSERT INTO Grades (GradeId, AssignmentId, StudentId, Grade)
SELECT 
    ROW_NUMBER() OVER (ORDER BY A.AssignmentId, U.UserId),
    A.AssignmentId,
    U.UserId,
    ABS(CHECKSUM(NEWID())) % 41 + 60
FROM Assignments A
JOIN Users U ON U.Role = 'Student';





SELECT *
FROM Courses;

SELECT *
FROM Assignments
WHERE CourseId = 1;

SELECT *
FROM Users
WHERE Role = 'Student';

UPDATE Users
SET Role = 'Teacher'
WHERE UserId = 1;

DELETE FROM Comments
WHERE CommentId = 5;

SELECT 
    U.UserId,
    U.FirstName,
    U.LastName,
    G.Grade
FROM Users U
JOIN Grades G ON U.UserId = G.StudentId
JOIN Assignments A ON G.AssignmentId = A.AssignmentId
WHERE U.Role = 'Student'
  AND A.CourseId = 1;

SELECT 
    C.CourseId,
    C.CourseName,
    AVG(CAST(G.Grade AS FLOAT)) AS AverageGrade
FROM Courses C
JOIN Assignments A ON C.CourseId = A.CourseId
JOIN Grades G ON A.AssignmentId = G.AssignmentId
GROUP BY C.CourseId, C.CourseName
ORDER BY C.CourseId;

SELECT 
    C.CourseId,
    C.CourseName,
    C.StartDate,
    C.EndDate,
    S.SyllabusId,
    S.Description AS SyllabusDescription
FROM Courses C
LEFT JOIN Syllabus S ON C.SyllabusId = S.SyllabusId
ORDER BY C.CourseId;

SELECT 
    C.CourseId,
    C.CourseName,
    A.AssignmentId,
    A.AssignmentTitle,
    CM.CommentId,
    CM.CreatedByUserId,
    CM.CreatedDate,
    CM.CommentContent
FROM Courses C
JOIN Assignments A ON C.CourseId = A.CourseId
JOIN Comments CM ON A.AssignmentId = CM.AssignmentId
WHERE C.CourseId = 1
ORDER BY CM.CreatedDate;


GO

CREATE PROCEDURE AddStudent
    @UserId INT,
    @UserName VARCHAR(64),
    @FirstName VARCHAR(64),
    @LastName VARCHAR(64),
    @EmailAddress VARCHAR(128),
    @PhoneNumber VARCHAR(16)
AS
BEGIN
    INSERT INTO Users (UserId, UserName, FirstName, LastName, EmailAddress, PhoneNumber, Role)
    VALUES (@UserId, @UserName, @FirstName, @LastName, @EmailAddress, @PhoneNumber, 'Student');
END;

EXEC AddStudent 
    @UserId = 16,
    @UserName = 'karam_sa',
    @FirstName = 'Karam',
    @LastName = 'Sawaf',
    @EmailAddress = 'karam@university.com',
    @PhoneNumber = '+963900000000';


    GO

CREATE PROCEDURE AddAssignment
    @AssignmentId INT,
    @CourseId INT,
    @AssignmentTitle VARCHAR(128),
    @Weight FLOAT,
    @MaxGrade INT,
    @DueDate DATE
AS
BEGIN
    IF (
        (SELECT ISNULL(SUM(Weight), 0)
         FROM Assignments
         WHERE CourseId = @CourseId) + @Weight > 100
    )
    BEGIN
        PRINT 'Total weight cannot exceed 100';
        RETURN;
    END

    INSERT INTO Assignments 
    VALUES (@AssignmentId, @CourseId, @AssignmentTitle, NULL, @Weight, @MaxGrade, @DueDate);
END;
GO

EXEC AddAssignment
    @AssignmentId = 26,
    @CourseId = 2,
    @AssignmentTitle = 'SQL Final Project',
    @Weight = 30,
    @MaxGrade = 100,
    @DueDate = '2026-07-15';

    GO

CREATE FUNCTION GetStudentCourseGrade
(
    @StudentId INT,
    @CourseId INT
)
RETURNS CHAR(1)
AS
BEGIN
    DECLARE @FinalGrade FLOAT;

    SELECT @FinalGrade =
        SUM(G.Grade * A.Weight) / SUM(A.Weight)
    FROM Grades G
    JOIN Assignments A ON G.AssignmentId = A.AssignmentId
    WHERE G.StudentId = @StudentId
      AND A.CourseId = @CourseId;

    IF @FinalGrade >= 90 RETURN 'A';
    ELSE IF @FinalGrade >= 80 RETURN 'B';
    ELSE IF @FinalGrade >= 70 RETURN 'C';
    ELSE IF @FinalGrade >= 60 RETURN 'D';
    ELSE RETURN 'F';

    RETURN NULL;
END;

GO

SELECT dbo.GetStudentCourseGrade(1, 1) AS FinalGrade;


GO

CREATE FUNCTION GetStudentGPA
(
    @StudentId INT
)
RETURNS FLOAT
AS
BEGIN
    DECLARE @GPA FLOAT;

    SELECT @GPA =
        SUM(
            CASE
                WHEN G.Grade >= 90 THEN 4.0
                WHEN G.Grade >= 80 THEN 3.0
                WHEN G.Grade >= 70 THEN 2.0
                WHEN G.Grade >= 60 THEN 1.0
                ELSE 0.0
            END * A.Weight
        )
        / SUM(A.Weight)

    FROM Grades G
    JOIN Assignments A ON G.AssignmentId = A.AssignmentId
    WHERE G.StudentId = @StudentId;

    RETURN @GPA;
END;

GO

SELECT dbo.GetStudentGPA(1) AS GPA;
