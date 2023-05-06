using System;
using System.Diagnostics;
using System.IO;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Checkout.Configuration.AwsSecretsManager;
using Checkout.Diagnostics.Lambdas.DataDog;
using Serilog;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Checkout.FX.LoggingExample.Core.IoC
{
    /// <summary>
    /// Core service collection called by the different host types
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static readonly string _environment =
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? throw new ArgumentNullException("DOTNET_ENVIRONMENT");

        /// <summary>
        /// Adds core services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection serviceCollection)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            serviceCollection.TryAddScoped<IHandler, Handler>();

            return serviceCollection
                .AddConfiguration(out var configuration)
                .AddObservability(configuration);
        }

        /// <summary>
        /// Adds settings and options to memory.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddConfiguration(this IServiceCollection serviceCollection, out IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            builder.AddJsonFile($"appsettings.{_environment}.json");
            builder.AddEnvironmentVariables();
            configuration = builder.Build();

            var config = configuration;

            void ConfigEditor(AmazonSecretsManagerConfig smConfig)
            {
                GetSecretsManagerConfig(config, smConfig);
            }

            try
            {
                builder.AddConfigurationFromSecretsManager("fx-logging-example", configEditor: ConfigEditor);

                configuration = builder.Build();
                serviceCollection.AddSingleton(configuration);
            }
            catch (Exception e)
            {
                // TODO Fix Message
                Console.WriteLine($"Failed to load AWS Secrets to memory. {e.Message}");
                throw;
            }

            return serviceCollection;
        }

        /// <summary>
        /// Register observability services
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddObservability(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddLambdaDataDogMetrics( configuration ?? throw new ArgumentNullException("Cannot find configuration service"));

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            serviceCollection.TryAddScoped(_ => Log.Logger);

            return serviceCollection;
        }

        private static void GetSecretsManagerConfig(IConfiguration configuration, AmazonSecretsManagerConfig smConfig)
        {
            string serviceUrl = configuration["SecretsManager:ServiceUrl"]
                ?? throw new ArgumentNullException(nameof(serviceUrl), "Service URL was not configured");
            string authenticationRegion = configuration["SecretsManager:AuthenticationRegion"]
                ?? throw new ArgumentNullException(nameof(authenticationRegion), "Authentication Region was not configured");

            if (string.IsNullOrEmpty(serviceUrl))
                return;

            smConfig.ServiceURL = serviceUrl;
            smConfig.AuthenticationRegion = authenticationRegion;
        }
    }
}
