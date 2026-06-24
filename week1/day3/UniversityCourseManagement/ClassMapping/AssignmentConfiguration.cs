using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UniversityCourseManagement
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.Comments)
                   .WithOne(c => c.Assignment)
                   .HasForeignKey(c => c.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Grades)
                   .WithOne(g => g.Assignment)
                   .HasForeignKey(g => g.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
