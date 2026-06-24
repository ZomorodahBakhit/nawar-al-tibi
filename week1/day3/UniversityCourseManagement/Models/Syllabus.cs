namespace UniversityCourseManagement
{
    public class Syllabus
    {
        public int Id { get; set; }
        public string Outline { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
