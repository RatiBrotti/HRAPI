using HRAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace HRAPI.DataAccess
{
    public partial class HRContext : DbContext
    {
        public HRContext(DbContextOptions<HRContext> options) : base(options)
        {
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.HasSequence("ContactIDSequence")
                .StartsAt(0)
                .IncrementsBy(10);

            modelBuilder.HasSequence("D").StartsAt(0);

            modelBuilder.HasSequence("val");

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
