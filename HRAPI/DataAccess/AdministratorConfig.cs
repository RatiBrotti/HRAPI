using HRAPI.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace HRMVC.Context
{
    public class AdministratorConfig : IEntityTypeConfiguration<Administrator>
    {
        public void Configure(EntityTypeBuilder<Administrator> builder)
        {
            builder.ToTable("Administrator");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Email)
                .HasMaxLength(80);

            builder.Property(e => e.Password)
                .HasMaxLength(51);

            builder.HasIndex(e=>e.Email)
                .IsUnique();

            //builder.HasOne(e=>e.Employee)
            //    .WithOne(e=>e.Administrator)
            //    .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
