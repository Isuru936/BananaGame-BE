using Serilog;
using Serilog.Core;
namespace BananaGame.Api.Extensions
{

    public static class ConfigureSeriLogExtension
    {
        public static IServiceCollection RegisterSerilogLogging(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddLogging(loggingBuilder =>
            {
                var levelSwitch = new LoggingLevelSwitch();

                var log = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.FromLogContext()
                    .MinimumLevel.Information()
                    .WriteTo.Console()
                    .MinimumLevel.ControlledBy(levelSwitch)
                    .CreateLogger();
                loggingBuilder.AddSerilog(log);
            });

            return service;
        }
    }
}
