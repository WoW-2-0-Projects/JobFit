namespace JobFit.Api.Configurations;

public static partial class HostConfiguration
{
    /// <summary>
    /// Configures application builder
    /// </summary>
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddSerializers()
            .AddCaching()
            .AddMappers()
            .AddPersistence()
            .AddMediator()
            // .AddCors()
            .AddDevTools()
            .AddExposers();
            
        return new(builder);
    }

    /// <summary>
    /// Configures application
    /// </summary>
    public static async  ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        // await app.MigrateDataBaseSchemasAsync();
        // await app.SeedDataAsync();

        app
            // .UseCors()
            .UseDevTools();
            // .UseLocalFileStorage();
        
        return app;
    }
}