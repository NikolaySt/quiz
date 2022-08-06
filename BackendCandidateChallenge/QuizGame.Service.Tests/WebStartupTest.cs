using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using QuizGame.Common.Infrastructure;

namespace QuizGame.Service.Tests;

public class WebStartupTest
{
    public WebStartupTest(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => app.UseWebService(env)
            .Initialize();
}