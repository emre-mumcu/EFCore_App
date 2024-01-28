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


                /// Normally the DbContext takes care of the transaction, but in this case (SET IDENTITY_INSERT) 
                /// manually taking care of the transactions are required. 
                /// Database context will generate a BEGIN TRAN after the SET IDENTITY_INSERT is issued.
                /// This will make transaction's inserts to fail since IDENTITY_INSERT seems to affect tables at session/transaction level.
                /// So, everything must be wrapped in a single transaction to work properly.
                using var transaction = context.Database.BeginTransaction();

                if (!context.Iller.Any())
                {
                    var iller = JsonConvert.DeserializeObject<List<Il>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "iller.json")));
                    context.Iller.AddRange(iller!);

                    await context.EnableIdentityInsert<Il>();
                    await context.SaveChangesAsync();
                    await context.DisableIdentityInsert<Il>();
                }

                if (!context.Ilceler.Any())
                {
                    var ilceler = JsonConvert.DeserializeObject<List<Ilce>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "ilceler.json")));
                    context.Ilceler.AddRange(ilceler!);

                    await context.EnableIdentityInsert<Ilce>();
                    await context.SaveChangesAsync();
                    await context.DisableIdentityInsert<Ilce>();
                }

                if (!context.SemtBucakBeldeler.Any())
                {
                    var sbbler = JsonConvert.DeserializeObject<List<SemtBucakBelde>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "semtbucakbeldeler.json")));
                    context.SemtBucakBeldeler.AddRange(sbbler!);

                    await context.EnableIdentityInsert<SemtBucakBelde>();
                    await context.SaveChangesAsync();
                    await context.DisableIdentityInsert<SemtBucakBelde>();
                }

                if (!context.Mahalleler.Any())
                {
                    var mahalleler = JsonConvert.DeserializeObject<List<Mahalle>>(File.ReadAllText(
                        Path.Combine(Environment.CurrentDirectory, "wwwroot", "static", "mahalleler.json")));
                    context.Mahalleler.AddRange(mahalleler!);

                    await context.EnableIdentityInsert<Mahalle>();
                    await context.SaveChangesAsync();
                    await context.DisableIdentityInsert<Mahalle>();
                }

                transaction.Commit();
            }
        }

        public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);
        public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: false);
        private static Task SetIdentityInsert<T>(DbContext context, bool enable)
        {
            var entityType = context.Model.FindEntityType(typeof(T))!;
            var value = enable ? "ON" : "OFF";
#pragma warning disable EF1002 // Risk of vulnerability to SQL injection.
            return context.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
#pragma warning restore EF1002 // Risk of vulnerability to SQL injection.
        }
    }
}