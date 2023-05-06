using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Checkout.FX.LoggingExample.Core;
using Checkout.FX.LoggingExample.Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Checkout.FX.LoggingExample.Host.Lambda
{
    /// <summary>
    /// Lambda Function
    /// </summary>
    public class Function
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly string? _environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        /// <summary>
        /// Default Lambda constructor
        /// </summary>
        public Function()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCoreServices();
            _serviceProvider = _environment is "local" or "localdocker" 
                ? serviceCollection.BuildServiceProvider(new ServiceProviderOptions
                {
                    ValidateScopes = true, ValidateOnBuild = true
                    
                }) : serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Lambda function entry point
        /// </summary>
        public async Task HandleAsync(ILambdaContext lambdaContext)
        {
            var cancellationToken = BuildCancellationToken(lambdaContext);

            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IHandler>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger>();
            logger.ForContext<Function>();
            try
            {
                await handler.HandleAsync(cancellationToken);
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Global exception handler");
                throw;
            }
        }

        private static CancellationToken BuildCancellationToken(ILambdaContext lambdaContext)
        {
            using var cancellationTokenSource = new CancellationTokenSource();

            if (lambdaContext.RemainingTime != TimeSpan.Zero)
            {
                // Exit gracefully just before we reach Lambda timeout limit
                var delay = lambdaContext.RemainingTime.Subtract(TimeSpan.FromSeconds(2));
                cancellationTokenSource.CancelAfter(delay);
            }

            var token = cancellationTokenSource.Token;
            token.Register(() =>
            {
                Console.WriteLine("Cancellation was requested. Exiting gracefully.");
            });
            return token;
        }

        /// <summary>
        /// Lambda does not yet implement IDisposable so we need to use the finalizer to perform disposal
        /// </summary>
        ~Function()
        {
            _serviceProvider.Dispose();
        }
    }
}