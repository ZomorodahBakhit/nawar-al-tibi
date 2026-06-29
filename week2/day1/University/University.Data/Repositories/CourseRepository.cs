using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface ICourseRepository
    {
        Course? GetById(int id);
        List<Course> GetAll();
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly UniversityDbContext _context;

        public CourseRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public List<Course> GetAll() => _context.Courses.ToList();

        public Course? GetById(int id) => _context.Courses.Find(id);

        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
}
