using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuizService.Data;
using QuizService.Infrastructure;
using QuizService.Services.Answers;
using QuizService.Services.Questions;
using QuizService.Services.Quizes;

namespace QuizService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddDbContext<QuizDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddTransient<IQuestionService, QuestionService>();
        services.AddTransient<IQuizService, Services.Quizes.QuizService>();
        services.AddTransient<IAnswerService, AnswerService>();
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Initialize();
    }
}