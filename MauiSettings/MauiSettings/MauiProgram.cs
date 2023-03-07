using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MauiSettings;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .AddCustomConfiguration()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<MainPage>();
        return builder.Build();
    }

    private static MauiAppBuilder AddCustomConfiguration(this MauiAppBuilder builder)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        using var stream = executingAssembly.GetManifestResourceStream(GetEnvironmentSettings());
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        
        builder.Configuration.AddConfiguration(configuration);
        return builder;
    }

    private static string GetEnvironmentSettings()
    {
#if DEVELOPMENT
        return "MauiSettings.Settings.appsettings.Development.json";
#elif PREPRODUCTION
        return "MauiSettings.Settings.appsettings.Preproduction.json";
#else
        return "MauiSettings.Settings.appsettings.json";
#endif
    }
}