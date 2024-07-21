using System.Reflection;
using JobFit.Application.Common.EventBus.Brokers;
using JobFit.Infrastructure.Common.Brokers;

using JobFit.Persistence.DataContext;
using JobFit.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace JobFit.Api.Configurations;

public static partial class HostConfigurations
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();
    
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
    
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    { 
        var dbConnectionString = builder.Environment.IsDevelopment()
            ? Environment.GetEnvironmentVariable("DbConnectionString")
            : builder.Configuration.GetConnectionString("DbConnectionString");

        builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));
        
        return builder;
    }

    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf 
            => {conf.RegisterServicesFromAssemblies(Assemblies.ToArray());});
        
        return builder;
    }

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEventBusBroker, EventBusBroker>();

        return builder;
    }
    
    private static async ValueTask<WebApplication> MigrateDatabaseSchemaAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        await serviceScopeFactory.MigrateAsync<AppDbContext>();
        
        return app;
    }
    
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
    
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}