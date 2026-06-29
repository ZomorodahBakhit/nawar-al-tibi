using University.Data.Entities;

namespace University.Data.Repositories
{
    public interface IStudentRepository
    {
        Student? GetById(int id);
        Student? GetByEmail(string email);
        List<Student> GetAll();
        void Add(Student student);
        void Update(Student student);
        void Delete(int id);
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context)
        {
            _context = context;
        }
        public List<Student> GetAll() => _context.Students.ToList();

        public Student? GetById(int id) => _context.Students.Find(id);

        public Student? GetByEmail(string email) =>
            _context.Students.FirstOrDefault(s => s.Email == email);

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
