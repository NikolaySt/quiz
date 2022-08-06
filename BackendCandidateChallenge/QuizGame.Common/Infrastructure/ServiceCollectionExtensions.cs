using System.Reflection;
using System.Text.RegularExpressions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizGame.Common.Models;
using QuizGame.Common.Services;

namespace QuizGame.Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebService<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
    {
        services
            .AddDatabase<TDbContext>(configuration)
            .AddAutoMapperProfile(Assembly.GetCallingAssembly())
            .AddTransient<INotificationService, NotificationService>()
            .AddControllers();

        return services;
    }

    public static IServiceCollection AddDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
        => services
            .AddScoped<DbContext, TDbContext>()
            .AddDbContext<TDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    public static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .AddAutoMapper(
                (_, config) => config
                    .AddProfile(new MappingProfile(assembly)),
                Array.Empty<Assembly>());

    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        params Type[] consumers)
    {
        var connection = configuration.GetConnectionString("RabbitMqConnection");
        var match = Regex.Match(connection, @"(.*):(.*)@(.*)");

        var host = match.Groups[3].Value;
        var password = match.Groups[2].Value;
        var username = match.Groups[1].Value;

        services.AddMassTransit(mt =>
        {
            foreach (var consumer in consumers)
            {
                mt.AddConsumer(consumer);
            }
            mt.AddBus(bus => Bus.Factory.CreateUsingRabbitMq(rmq =>
            {
                rmq.Host(host, host =>
                {
                    host.Username(password);
                    host.Password(username);
                });

                foreach (var consumer in consumers)
                {
                    rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                    {
                        endpoint.ConfigureConsumer(bus, consumer);
                    });
                }
            }));
        });

        return services;
    }
}