namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static void ConfigureAndAddLogger(this IServiceCollection services)
    {
        var config = LoggerExtensions.DefaultConfig;
              // We can add AppCenter sink here
        config.WriteTo.
        // Add file sink, will output to the file specified in the path
        File(LoggerExtensions.DefaultExpressionTemplate,
             Path.Combine(FileSystem.AppDataDirectory, "Logs/logs.txt"), LogEventLevel.Information,
             rollingInterval: RollingInterval.Day);
        var logger = LoggerExtensions.CreateLogger(config);
        services.AddLogging(options =>
        {
            options.ClearProviders();
            options.AddLogger(logger);
        });
        services.AddSingleton(logger); // add the logger as a singleton for DI
    }
}