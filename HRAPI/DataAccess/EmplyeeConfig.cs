using HRAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMVC.Context
{
    public class EmplyeeConfig : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Emploee");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdNumber)
                .HasMaxLength(11);

            builder.HasIndex(e => e.IdNumber)
                .IsUnique();

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .HasMaxLength(50);

            builder.Property(e => e.JobTitle)
                .HasMaxLength(100);

            builder.Property(e => e.Status)
                .HasMaxLength(50);

            builder.Property(e => e.Mobile)
                .HasMaxLength(50);
        }
    }
}
