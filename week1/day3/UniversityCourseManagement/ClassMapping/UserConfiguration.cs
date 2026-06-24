using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UniversityCourseManagement
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.EmailAddress)
                   .IsUnique();

            builder.HasMany(u => u.TaughtCourses)
                   .WithOne(c => c.Teacher)
                   .HasForeignKey(c => c.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Grades)
                   .WithOne(g => g.Student)
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Comments)
                   .WithOne(c => c.User)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
