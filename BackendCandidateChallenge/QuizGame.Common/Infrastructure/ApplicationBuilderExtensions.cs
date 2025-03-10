﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizGame.Common.Services;

namespace QuizGame.Common.Infrastructure;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebService(
        this IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => endpoints
                .MapControllers());

        return app;
    }

    public static IApplicationBuilder Initialize(
        this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var db = serviceProvider.GetRequiredService<DbContext>();

        db.Database.EnsureDeleted();

        if (db.Database.ProviderName != null
            && !db.Database.ProviderName.Contains("InMemory"))
            db.Database.Migrate();

        var seeders = serviceProvider.GetServices<IDataSeeder>();

        foreach (var seeder in seeders)
        {
            seeder.SeedData();
        }

        return app;
    }
}