namespace Spherum.Mobile;

public static partial class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseAcrylicView()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SourceSansPro-Bold.ttf", "SourceSansProBold");
                fonts.AddFont("SourceSansPro-Regular.ttf", "SourceSansProRegular");
                fonts.AddFont("SourceSansPro-SemiBold.ttf", "SourceSansProSemiBold");
            });

        builder.Services.ConfigureAndAddLogger();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}