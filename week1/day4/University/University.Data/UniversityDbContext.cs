using Microsoft.EntityFrameworkCore;
using University.Data.Entities;

namespace University.Data
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityDbContext).Assembly);
        }
    }
}
