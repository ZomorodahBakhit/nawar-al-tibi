using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UniversityCourseManagement
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.Assignments)
                   .WithOne(a => a.Course)
                   .HasForeignKey(a => a.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Syllabus)
                   .WithOne(s => s.Course)
                   .HasForeignKey<Syllabus>(s => s.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
