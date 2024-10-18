namespace Spherum.Mobile.UnitTests.Extensions;

/// <summary>
/// Provides assertions related to a logger.
/// </summary>
public static class AssertionExtensions
{
    /// <summary>
    /// Asserts that a logger of a specific type has been called.
    /// </summary>
    /// <typeparam name="T">The type attached to the logger.</typeparam>
    /// <param name="logger">The logger on which the call is being checked.</param>
    /// <param name="logLevel">The level of logs to check for calls.</param>
    /// <param name="numberOfCalls">The number of calls expected at the specified log level.</param>
    public static void AssertLoggerIsCalled<T>(this ILogger<T> logger, LogLevel logLevel, int numberOfCalls)
    {
        logger.Received(numberOfCalls)
              .Log(logLevel, Arg.Any<EventId>(), Arg.Any<object>(), Arg.Any<Exception>(),
                   Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Asserts that a logger of a specific type has been called with an exact message.
    /// </summary>
    /// <typeparam name="T">The type attached to the logger.</typeparam>
    /// <param name="logger">The logger on which the call is being checked.</param>
    /// <param name="logLevel">The level of logs to check for calls.</param>
    /// <param name="numberOfCalls">The number of calls expected at the specified log level.</param>
    /// <param name="message">The exact message that is expected to have been logged.</param>
    public static void AssertLoggerIsCalledWithExactMessage<T>(this ILogger<T> logger, LogLevel logLevel,
                                                               int numberOfCalls, string message)
    {
        logger.Received(numberOfCalls)
              .Log(logLevel, Arg.Any<EventId>(), Arg.Is<object>(o => o.ToString() == message), Arg.Any<Exception>(),
                   Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Asserts that a logger of a specific type has been called with a message containing a specified string.
    /// </summary>
    /// <typeparam name="T">The type attached to the logger.</typeparam>
    /// <param name="logger">The logger on which the call is being checked.</param>
    /// <param name="logLevel">The level of logs to check for calls.</param>
    /// <param name="numberOfCalls">The number of calls expected at the specified log level.</param>
    /// <param name="partialMessage">A string that is expected to be part of the logged message.</param>
    public static void AssertLoggerIsCalledWithPartialMessage<T>(this ILogger<T> logger, LogLevel logLevel,
                                                                 int numberOfCalls, string partialMessage)
    {
        logger.Received(numberOfCalls)
              .Log(logLevel, Arg.Any<EventId>(), Arg.Is<object>(o => o.ToString()!.Contains(partialMessage)),
                   Arg.Any<Exception>(), Arg.Any<Func<object, Exception?, string>>());
    }

    /// <summary>
    /// Asserts that a logger of a specific type has logged an error.
    /// </summary>
    /// <typeparam name="T">The type attached to the logger.</typeparam>
    /// <param name="logger">The logger on which the error logging is being checked.</param>
    /// <param name="numberOfCalls">The number of error logs expected.</param>
    public static void AssertErrorIsLogged<T>(this ILogger<T> logger, int numberOfCalls)
    {
        logger.AssertLoggerIsCalled(LogLevel.Error, numberOfCalls);
    }
}