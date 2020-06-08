using Shared.Http.Client;
using JokeGenerator.Application;
using JokeApiClient;
using JokeGenerator.ConsolePresentation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JokeGenerator
{
    public sealed class Program
    {
        public static void Main()
        {
            // In an application their should be just one place were we should configure the application. It's a host
            // Here I build the host. Basically here I'm configuring container, configuration 

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            using (var serviceProvider = new ServiceCollection()
                .AddSingleton<IPresentationBehavior, ConsoleMaster>()
                .AddSingleton<ConsoleWriter>()
                .AddTransient<ApplicationService>()
                .AddTransient<IJokeProvider, JokeWebApiClient>()
                .AddOptions()
                .Configure<JokeGeneratorSettings>(configuration.GetSection(nameof(JokeGeneratorSettings)))
                .BuildServiceProvider(true))
            {
                serviceProvider.GetService<ApplicationService>().Run();
            }
        }
    }
}