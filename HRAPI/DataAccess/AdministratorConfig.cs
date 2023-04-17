using HRAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace HRMVC.Context
{
    public class AdministratorConfig : IEntityTypeConfiguration<AdministratorEntity>
    {
        public void Configure(EntityTypeBuilder<AdministratorEntity> builder)
        {
            builder.ToTable("Administrator");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.IdNumber)
                .HasMaxLength(11);

            builder.Property(e => e.Name)
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .HasMaxLength(80);

            builder.Property(e => e.Password)
                .HasMaxLength(50);

            builder.HasIndex(e => e.IdNumber)
                .IsUnique();

            builder.HasIndex(e=>e.Email)
                .IsUnique();
        }

    }
}
