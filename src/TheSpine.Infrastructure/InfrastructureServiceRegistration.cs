using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TheSpine.Application.Persistance.Contract;
using TheSpine.Infrastructure.Authentication;
using TheSpine.Infrastructure.Authentication.Configuration;
using TheSpine.Infrastructure.DataAccess;
using TheSpine.Infrastructure.DataAccess.Contract;
using TheSpine.Infrastructure.DataAccess.Graph;
using TheSpine.Infrastructure.DataAccess.Repositories;

namespace TheSpine.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthenticationConfiguration>(configuration.GetSection("AzureAd"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped<INodeAsyncRepository, NodeAsyncRepository>();
            services.AddScoped<ISegmentItemAsyncRepository, SegmentItemAsyncRepository>();
            services.AddScoped<ISegmentAsyncRepository, SegmentAsyncRepository>();
            services.AddScoped<IItemDetailedInfoAsyncRepository, ItemDetailedInfoAsyncRepository>();
            services.AddScoped<IActivityAsyncRepository, ActivityAsyncRepository>();
            services.AddScoped<IGraphService, GraphService>();
            services.AddCustomAuthentication(configuration);
        }

        public static void AddLogging(this IHostBuilder webHostBuilder, IConfiguration configuration) 
        {
            var instrumentationKey = configuration["AzureAppInsights:InstrumentationKey"].ToString();
            webHostBuilder.UseSerilog((hostContext, services, configuration) => 
            {
                // TO-DO: Include appropriate sinks (e.g. SQL, Blob Storage, Insights, Analytics etc.)
                // Temporary sinks.
                configuration
                    .WriteTo.File("logs/logs.txt")
                    .WriteTo.ApplicationInsights(new Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration { InstrumentationKey = instrumentationKey}, TelemetryConverter.Traces)
                    .WriteTo.Console();
            });
        }
    }
}
