namespace UniversityCourseManagement
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public ICollection<Course> TaughtCourses { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
