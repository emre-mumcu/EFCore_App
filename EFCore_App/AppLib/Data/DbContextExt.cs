using EFCore_App.AppLib.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Reflection.Emit;

namespace EFCore_App.AppLib.Data
{
    public static class DbContextExt
    {
        /// <summary>
        /// This SeedData method will run along with migrations (add migration and update database).
        /// Primary key field (ID) data can be SET in this type of seeding.
        /// But this will cause the migration files and times bigger and longer if the seed data size is  large.
        /// </summary>        
        [Obsolete(message:"Use the other SeedData in Program.cs", error: true)]
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            //
            // if (System.Diagnostics.Debugger.IsAttached == false) System.Diagnostics.Debugger.Launch();
            // 

            var iller = JsonConvert.DeserializeObject<List<Il>>(File.ReadAllText(
                Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "iller.json")));
            modelBuilder.Entity<Il>().HasData(iller!);

            var ilceler = JsonConvert.DeserializeObject<List<Ilce>>(File.ReadAllText(
                Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "ilceler.json")));
            modelBuilder.Entity<Ilce>().HasData(ilceler!);

            var sbbler = JsonConvert.DeserializeObject<List<SemtBucakBelde>>(File.ReadAllText(
                Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "semtbucakbeldeler.json")));
            modelBuilder.Entity<SemtBucakBelde>().HasData(sbbler!);

            var mahalleler = JsonConvert.DeserializeObject<List<Mahalle>>(File.ReadAllText(
                Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "mahalleler.json")));
            modelBuilder.Entity<Mahalle>().HasData(mahalleler!);
        }

        /// <summary>
        /// 
        /// </summary>
        public static async Task SeedData(IServiceProvider services)
        {
            IWebHostEnvironment environment = services.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                IServiceScope scope = services.CreateScope();

                SampleDbContext context = scope.ServiceProvider.GetRequiredService<SampleDbContext>();

                context.Database.EnsureCreated();

                if (context.Database.GetPendingMigrations().Any()) await context.Database.MigrateAsync();

                if (!context.Iller.Any())
                {
                    var iller = JsonConvert.DeserializeObject<List<Il>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "iller.json")));
                    context.Iller.AddRange(iller!);
                    await context.SaveChangesAsync();
                }

                if (!context.Ilceler.Any())
                {
                    var ilceler = JsonConvert.DeserializeObject<List<Ilce>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "ilceler.json")));
                    context.Ilceler.AddRange(ilceler!);
                    await context.SaveChangesAsync();
                }

                if (!context.SemtBucakBeldeler.Any())
                {
                    var sbbler = JsonConvert.DeserializeObject<List<SemtBucakBelde>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "semtbucakbeldeler.json")));
                    context.SemtBucakBeldeler.AddRange(sbbler!);
                    await context.SaveChangesAsync();
                }

                if (!context.Mahalleler.Any())
                {
                    var mahalleler = JsonConvert.DeserializeObject<List<Mahalle>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "mahalleler.json")));
                    context.Mahalleler.AddRange(mahalleler!);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}