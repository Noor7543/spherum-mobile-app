using CommunityToolkit.Mvvm.ComponentModel;

namespace Spherum.Mobile.ViewModels.Base;

/* All view models to inherit from this class and mark themselves a `public sealed partial` to get
 NotifyPropertyChanged and perf benefits */
public partial class BaseViewModel : ObservableObject, IQueryAttributable, IDisposable
{
    // readonly IShell _shell;

    protected internal BaseViewModel(/*IShell shell*/)
    {
        // _shell = shell;
    }

    [ObservableProperty]
    bool _isBusy;

    [ObservableProperty]
    bool _useObtrusiveOverlay;

    [ObservableProperty]
    string? _loadingMessage;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object?> query)
    {
        // override and received the params as needed
    }

    public virtual Task InitAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task CleanupAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        // inherit and clean up unused resources here
    }
}