using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Common.Models;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Services.Answers;
using QuizGame.Service.Services.Questions;
using QuizGame.Service.Services.Quizzes;

namespace QuizGame.Service.Tests;

public abstract class SuperTest
{
    private static readonly Assembly QuizServiceAssembly = Assembly.Load("QuizGame.Service");

    private static string GetRandomDbName() => $"QuizDatabase_{Guid.NewGuid().ToString()[..8]}";

    protected static TestServer GetTestWebServer()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddEnvironmentVariables();

        var config = builder.Build();

        var dbName = GetRandomDbName();

        return new TestServer(new WebHostBuilder()
            .ConfigureServices(it => it
                .AddSingleton(config)
                .AddScoped<DbContext, QuizDbContext>()
                .AddDbContext<QuizDbContext>(i => i.UseInMemoryDatabase(dbName))
                .AddAutoMapper((_, cfg) => cfg.AddProfile(new MappingProfile(QuizServiceAssembly)), Array.Empty<Assembly>())
                .AddTransient<IDataSeeder, QuizDataSeeder>()
                .AddTransient<IQuestionService, QuestionService>()
                .AddTransient<IQuizService, Services.Quizzes.QuizService>()
                .AddTransient<IAnswerService, AnswerService>()
                .AddTransient<INotificationService, NotificationService>()
                .AddMassTransitTestHarness()
                .AddControllers()
                .AddApplicationPart(QuizServiceAssembly)
                .AddControllersAsServices()
            )
            .UseStartup<WebStartupTest>());
    }

    protected static IMapper GetMapper()
    {
        var assembly = Assembly.Load("QuizGame.Service");
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile(assembly)));
        return new Mapper(configuration);
    }

    protected static QuizDbContext GetInMemoryDbContext()
    {
        var dbName = GetRandomDbName();
        var options = new DbContextOptionsBuilder().UseInMemoryDatabase(dbName).Options;
        return new QuizDbContext(options);
    }

    protected static async Task<ITestHarness> GetMassTransitHarness<T>(
        IConsumer<T> consumer = null) where T : class, new()
    {
        var provider = new ServiceCollection()
            .AddMassTransitTestHarness(cfg =>
            {
                if (consumer != null) cfg.AddConsumer(consumer.GetType());
            }
            )

            .BuildServiceProvider(true);

        var harness = provider.GetRequiredService<ITestHarness>();

        await harness.Start();

        return harness;
    }
}