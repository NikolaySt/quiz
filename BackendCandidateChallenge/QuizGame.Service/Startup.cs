using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Common.Infrastructure;
using QuizGame.Common.Services;
using QuizGame.Service.Data;
using QuizGame.Service.Services.Answers;
using QuizGame.Service.Services.Questions;
using QuizGame.Service.Services.Quizes;
using ServiceCollectionExtensions = QuizGame.Common.Infrastructure.ServiceCollectionExtensions;

namespace QuizGame.Service;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.

    public void ConfigureServices(IServiceCollection services)
        => ServiceCollectionExtensions.AddWebService<QuizDbContext>(services, this.Configuration)
            .AddTransient<IDataSeeder, QuizDataSeeder>()
            .AddTransient<IQuestionService, QuestionService>()
            .AddTransient<IQuizService, Services.Quizes.QuizService>()
            .AddTransient<IAnswerService, AnswerService>()
            //.AddMessaging();
            .AddMvc();

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => app
            .UseWebService(env)
            .Initialize();

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    //{
    //    if (env.IsDevelopment())
    //    {
    //        app.UseDeveloperExceptionPage();
    //    }
    //    app.UseRouting();
    //    app.UseEndpoints(endpoints =>
    //    {
    //        endpoints.MapControllers();
    //    });
    //    app.Initialize();
    //}
}