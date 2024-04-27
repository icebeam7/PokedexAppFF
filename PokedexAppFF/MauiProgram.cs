using Microsoft.Extensions.Logging;

using MudBlazor.Services;
using PokeApiNet;

using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration;
using PokedexAppFF.Services;

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

            string appSettingsConnectionString = "";

            builder.Configuration.AddAzureAppConfiguration(options =>
            {
                options.Connect(appSettingsConnectionString)
                        .Select("PokedexApp:*")
                        .ConfigureRefresh(refreshOptions =>
                            refreshOptions.Register(
                                "PokedexApp:Sentinel", true)
                            .SetCacheExpiration(TimeSpan.FromSeconds(30)))
                        .UseFeatureFlags();
            });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddFeatureManagement();
            builder.Services.AddAzureAppConfiguration();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services
                .AddMudServices()
                .AddSingleton<IConfigRefresherService, 
                        ConfigRefresherService>()
                .AddSingleton<PokeApiClient>()
                .AddSingleton<StateService>();

            return builder.Build();
        }
    }
}
