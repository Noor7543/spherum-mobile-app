namespace Spherum.Mobile.Services.Core;

public interface IShell
{
    /// <summary>
    /// Asynchronously navigates to one view back on the stack, optionally animating.
    /// </summary>
    /// <param name="animated">Indicates whether view should animate while navigating</param>
    /// <returns cref='Task'/>
    Task GoBackAsync(bool animated = true);

    /// <summary>
    /// Asynchronously navigates to the shell provided, optionally animating.
    /// </summary>
    /// <param name="route">Route to navigate to</param>
    /// <param name="animated">Indicates whether view should animate while navigating</param>
    /// <returns cref='Task'/>
    Task GotoAsync(string route, bool animated = true);

    /// <summary>
    /// Asynchronously navigates to one view back on the stack, optionally animating.
    /// </summary>
    /// <param name="route">Route to navigate to</param>
    /// <param name="param">Parameters to pass to the view model via ApplyQueryAttributes override</param>
    /// <param name="animated">Indicates whether view should animate while navigating</param>
    /// <returns cref='Task'/>
    Task GotoAsync(string route, Dictionary<string, object?> param, bool animated = true);

    /// <summary>
    /// Displays a toast
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="length">Toast length</param>
    Task DisplayToastAsync(string message, SnackBarLength length = SnackBarLength.Long);
}

public enum SnackBarLength
{
    Short, //2 seconds
    Long //3 seconds
}