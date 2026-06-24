namespace UniversityCourseManagement
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
