using UIKit;

namespace Spherum.Mobile;

public static partial class MauiProgram
{
    static partial void RegisterHandlers(this IMauiHandlersCollection handlers)
    {
        // registers handlers here

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(IView.Background), (handler, view) =>
        {
            if (view is not BorderLessEntry entry)
            {
                return;
            }

            entry.HeightRequest = 40;
            handler.PlatformView.BorderStyle = UITextBorderStyle.None;
        });
    }
}