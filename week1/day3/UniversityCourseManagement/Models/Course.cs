namespace UniversityCourseManagement
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public Syllabus Syllabus { get; set; }
    }
}
