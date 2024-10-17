using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Spherum.Mobile.Services.LoggingService.Extensions;

namespace Spherum.Mobile.Services.LoggingService.Sinks;

/// <summary>
/// Writes events to <see cref="System.Diagnostics.Debug"/>.
/// </summary>
public class DebugConsoleSink : ILogEventSink
{
    readonly ITextFormatter? _textFormatter;

    /// <summary>
    /// Initializes a new instance of the <see cref="DebugConsoleSink"/> class.
    /// </summary>
    /// <param name="textFormatter">Formatter for log events</param>
    /// <exception cref="ArgumentNullException">The text formatter must be provided</exception>
    public DebugConsoleSink(ITextFormatter? textFormatter)
    {
        _textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
    }

    /// <summary>
    /// Emit the provided log event to the sink.
    /// </summary>
    /// <param name="logEvent">The log event to write.</param>
    public void Emit(LogEvent logEvent)
    {
        ArgumentNullException.ThrowIfNull(logEvent);

        Trace.WriteLine(LoggerExtensions.GetLogMessage(logEvent, _textFormatter));
    }
}