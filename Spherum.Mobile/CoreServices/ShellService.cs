namespace Spherum.Mobile.CoreServices;
public sealed class ShellService : IShell
{
    readonly ILogger<ShellService> _logger;
    readonly SemaphoreSlim _semaphore = new(1);

    public ShellService(ILogger<ShellService> logger)
    {
        _logger = logger;
    }

    public async Task GoBackAsync(bool animated = true)
    {
        await EnsureShellIsCreatedAsync();

        await Shell.Current.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                await _semaphore.WaitAsync();
                await Shell.Current.GoToAsync("..", animated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating back");

                return;
            }
            finally
            {
                _semaphore.Release();
            }
        });

        _logger.LogInformation("Navigating back");
    }

    public async Task GotoAsync(string route, bool animated = true)
    {
        await EnsureShellIsCreatedAsync();

        await Shell.Current.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                await _semaphore.WaitAsync();

                await Shell.Current.GoToAsync(route, animated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to {route}", route);

                return;
            }
            finally
            {
                _semaphore.Release();
            }
        });

        _logger.LogInformation("Navigating to {shell}", route);
    }

    public async Task GotoAsync(string route, Dictionary<string, object?> param, bool animated = true)
    {
        await EnsureShellIsCreatedAsync();

        await Shell.Current.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                await _semaphore.WaitAsync();

                await Shell.Current.GoToAsync(route, animated, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to {route}", route);

                return;
            }
            finally
            {
                _semaphore.Release();
            }
        });

        //_logger.LogInformation("Navigating {route} with params {@p}", route, param); // quite verbose
        _logger.LogInformation("Navigating {route} with params", route);
    }

    public async Task DisplayToastAsync(string message, SnackBarLength length = SnackBarLength.Long)
    {
        var cancellationTokenSource = new CancellationTokenSource();

        var snackBarOptions = new SnackbarOptions
        {
            BackgroundColor = Application.Current?.Resources["BlacksGreysCharcoal"] as Color ?? Colors.DarkGray,
            TextColor = Colors.White,
            CornerRadius = new CornerRadius(4),
            Font = Microsoft.Maui.Font.OfSize("SourceSansProRegular", 16)
        };

        var duration = TimeSpan.FromSeconds(length == SnackBarLength.Short ? 2 : 3);
        var snackBar = Snackbar.Make(message, null, string.Empty, duration, snackBarOptions);

        await Shell.Current.Dispatcher.DispatchAsync(async () =>
        {
            try
            {
                await _semaphore.WaitAsync(cancellationTokenSource.Token);

                await snackBar.Show(cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while displaying toast");
            }
            finally
            {
                _semaphore.Release();
            }
        });
    }

    static Task EnsureShellIsCreatedAsync()
    {
        // due to startup race condition on some instances the shell object can be null
        return Shell.Current is null ? Task.Delay(300) : Task.CompletedTask;
    }
}