using EFCore_App.AppLib.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore_App.AppLib.Data
{
    // CREATE DATABASE SampleDB ENCODING = 'UTF8' 
    // dotnet ef migrations add Init -o AppLib/Data/Migrations
    // dotnet ef database update
    public class SampleDbContext : DbContext
    {
        public SampleDbContext() { }
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = App.Instance.DataConfiguration.GetSection("Database:ConnectionString").Value!;
                optionsBuilder.UseSqlServer(connectionString: connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            // modelBuilder.SeedData();
        }


        public virtual DbSet<Il> Iller => Set<Il>();
        public virtual DbSet<Ilce> Ilceler => Set<Ilce>();
        public virtual DbSet<SemtBucakBelde> SemtBucakBeldeler => Set<SemtBucakBelde>();
        public virtual DbSet<Mahalle> Mahalleler => Set<Mahalle>();
    }
}
