using HRAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMVC.Context
{
    public class EmplyeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
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

            builder.HasIndex(e=>e.Mobile)
                .IsUnique();

            //builder.HasOne(e=>e.Administrator)
            //    .WithOne(e=>e.Employee)
            //    .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(e=>e.AdministratorId)
                .IsUnique();

            builder.Property(e => e.AdministratorId)
                .IsRequired(false);
                
        }
    }
}
