using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuChallenge.Console {
    class Program {
        private static IHost CreateHost() =>
          Host.CreateDefaultBuilder()
              .ConfigureServices((context, services) =>
              {
                  services.AddSingleton<ILogger>();
              })
              .Build();

        static int Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<IRunner, Runner>();
                services.Configure<Settings>(context.Configuration.GetSection("Settings"));
                services.AddLogging(builder => builder.AddConsole());
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .Build();

            var runner = host.Services.GetRequiredService<IRunner>();
            return runner.Run();
        }
    }
}