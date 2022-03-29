using DemoEntityFrameworkRepository.Context;
using DemoEntityFrameworkRepository.Settings;
using DemoMongoRepository;
using DemoMongoRepository.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DemoAPI.Configuration;

/// <summary>
/// Implementation of the services registered in the <see cref="Program"/> class
/// </summary>
public static class ServiceCollectionExtension
{
    internal static IServiceCollection AddDemoAPISwaggerGen(this IServiceCollection services)
    {
        // Configure Swagger documentation
        services.AddSwaggerGen(options =>
        {
            // add swagger documentation
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Repository Pattern",
                Description = "A .NET 6 Web API demo project to showcase Generic Repository Patterns and Swagger / Open API documentation",
                Version = "v1.0",
                Contact = new OpenApiContact()
                {
                    Name = "Viwe Nkepu",
                    Email = "viwe.nkepu@hotmail.com",
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });

            // get the file path for XML documentation
            var fileName = typeof(Program).GetTypeInfo().Assembly.GetName().Name + ".xml";
            var xmlCommentsFilePath = Path.Combine(AppContext.BaseDirectory, fileName);
            // add XML documentation to swagger UI
            options.IncludeXmlComments(xmlCommentsFilePath, true);
        });

        return services;
    }

    internal static IServiceCollection AddDemoAPISettings(this IServiceCollection services, IConfiguration configuration)
    {
        // add 'MongoDbSettings' section to dependency container
        services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));

        // add 'SQLSettings' section to dependency container
        services.Configure<SQLSettings>(configuration.GetSection(nameof(SQLSettings)));

        // get the current value from the appsettings.json section
        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptionsMonitor<MongoDbSettings>>().CurrentValue);
        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptionsMonitor<SQLSettings>>().CurrentValue);

        return services;
    }

    internal static IServiceCollection AddDemoAPIServices(this IServiceCollection services)
    {
        // register the MongoDB repository
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

        return services;
    }

    internal static IServiceCollection AddWebAPIAutomapper(this IServiceCollection services)
    {
        // specify assemblies to scan
        var assemblies = new Assembly[] { typeof(MongoRepository<>).GetTypeInfo().Assembly };

        services.AddAutoMapper(assemblies);

        return services;
    }

    internal static IServiceCollection AddWebAPIDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration["SQLSettings:ConnectionString"], builder => builder.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name));
        });

        return services;
    }
}
