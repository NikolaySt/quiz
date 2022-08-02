using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizService.Data;

namespace QuizService.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;

            var db = serviceProvider.GetRequiredService<QuizDbContext>();

            db.Database.Migrate();

            //if (db.Categories.Any())
            //{
            //    return app;
            //}

            //foreach (var category in GetData())
            //{
            //    db.Categories.Add(category);
            //}

            //db.SaveChanges();

            return app;
        }

        //private static IDbConnection InitializeDb()
        //{
        //    var connection = new SqliteConnection("Data Source=:memory:");
        //    connection.Open();

        //    // Migrate up
        //    var assembly = typeof(Startup).GetTypeInfo().Assembly;
        //    var migrationResourceNames = assembly.GetManifestResourceNames()
        //        .Where(x => x.EndsWith(".sql"))
        //        .OrderBy(x => x);
        //    if (!migrationResourceNames.Any()) throw new System.Exception("No migration files found!");
        //    foreach (var resourceName in migrationResourceNames)
        //    {
        //        var sql = GetResourceText(assembly, resourceName);
        //        var command = connection.CreateCommand();
        //        command.CommandText = sql;
        //        command.ExecuteNonQuery();
        //    }

        //    return connection;
        //}

        //private static string GetResourceText(Assembly assembly, string resourceName)
        //{
        //    using var stream = assembly.GetManifestResourceStream(resourceName);
        //    using var reader = new StreamReader(stream);
        //    return reader.ReadToEnd();
        //}
    }
}