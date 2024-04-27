﻿using Microsoft.Extensions.Logging;

using MudBlazor.Services;
using PokeApiNet;

namespace PokedexAppFF
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddMudServices();
            builder.Services.AddSingleton(new PokeApiClient());
            builder.Services.AddSingleton(new StateService());

            return builder.Build();
        }
    }
}
