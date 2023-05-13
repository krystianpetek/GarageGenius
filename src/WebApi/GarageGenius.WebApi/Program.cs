using GarageGenius.Shared.Abstractions.Modules;
using GarageGenius.Shared.Infrastructure;
using GarageGenius.WebApi.Middlewares.ErrorHandling;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Collections.Immutable;
using System.Reflection;

namespace GarageGenius.WebApi;

public static class Program
{
    public async static Task<int> Main(string[] args)
    {
        // ensure logging before configuration is loaded
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        try
        {
            Log.Information("Starting web host");

            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostBuilderContext.Configuration)
                .ReadFrom.Services(serviceProvider)
                .Enrich.FromLogContext(),
                preserveStaticLogger: true);

            builder.Services.AddGlobalErrorHandling();
            builder.Services.AddAuthorization();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen((swagger) =>
            {
                swagger.EnableAnnotations();
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc(name: "v1", info: new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GeniusGarage"
                });
            });
            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();

            IList<Assembly> assemblies = LoadAssemblies(builder.Configuration, "GarageGenius.Modules.");
            IEnumerable<IModule> modules = assemblies.LoadModules();

            builder.Services.AddSharedInfrastructure( builder.Configuration, assemblies);
            foreach (IModule module in modules)
            {
                module.Register(builder.Services);
                Log.Information($"Loaded module: {module.Name}");
            }

            WebApplication? app = builder.Build();
            app.UseSerilogRequestLogging(requestLoggingOptions =>
            {
                requestLoggingOptions.MessageTemplate = "HTTP {RequestMethod} {RequestPath} ({UserId}) responded {StatusCode} in {Elapsed:0.0000} ms";
            });

            foreach (IModule module in modules)
            {
                module.Use(app);
                Log.Information($"Mapped registered services for module: {module.Name}");
            }

            app.UseGlobalErrorHandling();
            app.UseSharedInfrastructure();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI((swagger) =>
            {
                swagger.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "GeniusGarage");
            });

            app.MapControllers();
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
        return 0;
    }

    public static IList<IModule> LoadModules(this IEnumerable<Assembly> assemblies)
        => assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
            .OrderBy(x => x.Name)
            .Select(Activator.CreateInstance)
            .Cast<IModule>()
            .ToList();

    public static IList<Assembly> LoadAssemblies(IConfiguration configuration, string modulePart)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

        return assemblies;
    }

}