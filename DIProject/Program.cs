
//Di, Serilog, Settings

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace DIProject

{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            //Build Config
            BuildConfig(builder);


            //Logger Configuration
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())  // Read Config from appsettings.json
                .Enrich.FromLogContext()                    // Extra information to logging
                .WriteTo.Console()                          // Write to Console
                .CreateLogger();                            // Create Logger

            Log.Logger.Information("Starting App");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((ctx, svs) =>
                {
                    svs.AddTransient<IDoomerServices, DoomerServices>();
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<DoomerServices>(host.Services);
            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            // Set up Configuration
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

        }
    }
}
