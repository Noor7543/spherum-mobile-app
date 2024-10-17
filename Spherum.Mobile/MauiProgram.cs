﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Xe.AcrylicView;

namespace Spherum.Mobile;

public static class MauiProgram
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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}