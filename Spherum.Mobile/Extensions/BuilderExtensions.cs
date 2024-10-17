using Microsoft.Extensions.Logging;

namespace Spherum.Mobile.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Resolves an instance of <see cref="ILogger{T}"/> for the given type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="sp">The service provider to resolve from.</param>
    /// <typeparam name="T">The type of logger to resolve.</typeparam>
    /// <returns>
    /// A logger of type <typeparamref name="T"/> if it's available; otherwise throws an <see cref="InvalidOperationException"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when no service of <see cref="ILogger{T}"/> can be found.</exception>
    public static ILogger<T> ResolveLogger<T>(this IServiceProvider sp)
    {
        return sp.GetService<ILogger<T>>() ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Resolve Service.
    /// </summary>
    /// <param name="sp">The service provider.</param>
    /// <typeparam name="T">The type of service.</typeparam>
    /// <returns>The requested service if available, otherwise throws <see cref="InvalidOperationException"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service of type T cannot be resolved.</exception>
    public static T ResolveService<T>(this IServiceProvider sp)
    {
        return sp.GetService<T>() ?? throw new InvalidOperationException($"Service of type {typeof(T).Name} not found");
    }
}