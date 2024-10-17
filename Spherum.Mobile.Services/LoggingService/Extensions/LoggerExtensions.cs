namespace Spherum.Mobile.Services.LoggingService.Extensions;

/// <summary>
/// Extension methods for the <see cref="ILoggerFactory"/> class.
/// </summary>
public static class LoggerExtensions
{
    /// <summary>Add Logger to the logging pipeline.</summary>
    /// <param name="builder">The <see cref="T:Microsoft.Extensions.Logging.ILoggingBuilder" /> to add logging provider to.</param>
    /// <param name="logger">The Logger logger; if not supplied, the static <see cref="T:Serilog.Log" /> will be used.</param>
    /// <param name="dispose">When true, dispose <paramref name="logger" /> when the framework disposes the provider. If the
    ///     logger is not specified but <paramref name="dispose" /> is true, the <see cref="M:Serilog.Log.CloseAndFlush" /> method will be
    ///     called on the static <see cref="T:Serilog.Log" /> class instead.</param>
    /// <returns>Reference to the supplied <paramref name="builder" />.</returns>
    public static void AddLogger(this ILoggingBuilder builder, ILogger? logger = null, bool dispose = false)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (dispose)
        {
            builder.Services.AddSingleton<ILoggerProvider, SerilogLoggerProvider>(
                _ => new SerilogLoggerProvider(logger, true));
        }
        else
        {
            builder.AddProvider(new SerilogLoggerProvider(logger));
        }

        builder.AddFilter<SerilogLoggerProvider>(null, LogLevel.Trace);
    }

    const string DefaultOutputTemplate =
        "[T:{Thread}] [{@t:HH:mm:ss}] [{Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)}] [{@l:u3}] {@m}\n{@x}";

    public static readonly ExpressionTemplate DefaultExpressionTemplate = new(DefaultOutputTemplate);

    /// <summary>
    /// Gets the default logger configuration.
    /// </summary>
    public static LoggerConfiguration DefaultConfig =>
        new LoggerConfiguration().Enrich.With(new ThreadIdEnricher())
#if DEBUG
                                 .MinimumLevel.Debug()
#else
                                 .MinimumLevel.Information()
#endif
                                 .WriteTo.DebugSink(DefaultExpressionTemplate);

    /// <summary>
    /// Configures the logger.
    /// </summary>
    /// <param name="config">The logger configuration.</param>
    /// <returns>The ILogger instance.</returns>
    public static ILogger CreateLogger(LoggerConfiguration? config = null)
    {
        config ??= DefaultConfig;

        return config.CreateLogger();
    }

    public static string GetLogMessage(LogEvent logEvent, ITextFormatter? formatter)
    {
        using var writer = new StringWriter();
        formatter?.Format(logEvent, writer);

        return writer.ToString();
    }
}