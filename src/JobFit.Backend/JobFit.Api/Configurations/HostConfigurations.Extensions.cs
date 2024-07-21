using System.Reflection;
using JobFit.Domain.Common.Constants;
using JobFit.Infrastructure.Common.Settings;
using JobFit.Persistence.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobFit.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();

    /// <summary>
    /// Configures mappers
    /// </summary>
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }

    /// <summary>
    /// Registers persistence infrastructure
    /// </summary>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Determine connection string
        var dbConnectionString = builder.Environment.IsProduction()
            ? Environment.GetEnvironmentVariable(DataAccessConstants.DbConnectionString)
            : builder.Configuration.GetConnectionString(DataAccessConstants.DbConnectionString);

        // Register data context
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString); });

        return builder;
    }
    /// <summary>
    /// Registers mediatr infrastructure
    /// </summary>
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => { conf.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

        return builder;
    }
    
    /// <summary>
    /// Configures CORS for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        // Register settings
        builder.Services.Configure<CorsSettings>(builder.Configuration.GetSection(nameof(CorsSettings)));
        var corsSettings = builder.Configuration.GetSection(nameof(CorsSettings)).Get<CorsSettings>()
                           ?? throw new HostAbortedException("Cors settings are not configured");
        
        builder.Services.AddCors(options => options.AddPolicy(HostConstants.AllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins(corsSettings.AllowedOrigins);
                    
                if(corsSettings.AllowAnyHeaders)
                    policy.AllowAnyHeader();
                
                if(corsSettings.AllowAnyMethods)
                    policy.AllowAnyMethod();
                
                if(corsSettings.AllowCredentials)
                    policy.AllowCredentials();
            }
        ));

        return builder;
    }

    /// <summary>
    /// Registers developer tools
    /// </summary>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();

        return builder;
    }

    /// <summary>
    /// Registers API exposers
    /// </summary>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers().AddNewtonsoftJson();

        return builder;
    }

    /// <summary>
    /// Migrates database schemas
    /// </summary>
    private static async ValueTask<WebApplication> MigrateDataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);

        await serviceScopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }
    
    // /// <summary>
    // /// Seeds application initial data
    // /// </summary>
    // private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    // {
    //     var serviceScope = app.Services.CreateScope();
    //     await serviceScope.ServiceProvider.InitializeSeedAsync();
    //
    //     return app;
    // }

    /// <summary>
    /// Configures CORS settings
    /// </summary>
    private static WebApplication UseCors(this WebApplication app)
    {
        app.UseCors(HostConstants.AllowSpecificOrigins);

        return app;
    }

    /// <summary>
    /// Registers developer tools middlewares
    /// </summary>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    /// <summary>
    /// Registers exposer middlewares
    /// </summary>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}