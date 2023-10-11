using MathGame.wkktoria.Data;
using Microsoft.Extensions.Logging;

namespace MathGame.wkktoria;

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
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");

        builder.Services.AddSingleton(s =>
            ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}