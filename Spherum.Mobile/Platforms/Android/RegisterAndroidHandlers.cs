namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static partial void RegisterHandlers(this IMauiHandlersCollection handlers)
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
            if (view is BorderLessEntry _)
            {
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        });
    }
}