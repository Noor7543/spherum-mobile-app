namespace Spherum.Mobile.Services.LoggingService.Extensions;

/// <summary>
/// Extension methods for Logger.
/// </summary>
public static class DebugConsoleSinkConfigurationExtensions
{
    public static LoggerConfiguration DebugSink(this LoggerSinkConfiguration sinkConfiguration,
                                                ITextFormatter formatter, LogEventLevel level = LevelAlias.Minimum)
    {
        return sinkConfiguration.Sink(new DebugConsoleSink(formatter), level);
    }
}