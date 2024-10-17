namespace Spherum.Mobile.Services.LoggingService.LogEnrichers;

/// <summary>
/// Enriches log events with the current thread's ID.
/// </summary>
public sealed class ThreadIdEnricher : ILogEventEnricher
{
    /// <summary>
    /// Enriches the log event with the current thread's ID.
    /// </summary>
    /// <param name="logEvent">A log event.</param>
    /// <param name="propertyFactory">Creates log event properties from regular .NET objects, applying policies as
    /// required.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Thread", Environment.CurrentManagedThreadId));
    }
}