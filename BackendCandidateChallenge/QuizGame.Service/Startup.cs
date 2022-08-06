using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Common.Infrastructure;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Services.Answers;
using QuizGame.Service.Services.Questions;
using QuizGame.Service.Services.Quizzes;

namespace QuizGame.Service;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
        => services.AddWebService<QuizDbContext>(Configuration)
            .AddTransient<IDataSeeder, QuizDataSeeder>()
            .AddTransient<IQuestionService, QuestionService>()
            .AddTransient<IQuizService, Services.Quizzes.QuizService>()
            .AddTransient<IAnswerService, AnswerService>()
            .AddMessaging(Configuration)
            .AddMvc();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => app
            .UseWebService(env)
            .Initialize();
}