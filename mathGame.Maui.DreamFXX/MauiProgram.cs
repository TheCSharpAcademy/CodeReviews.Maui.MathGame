using mathGame.Maui.Data;
using Microsoft.Extensions.Logging;
using Windows.UI.Shell;

namespace mathGame.Maui
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
                    fonts.AddFont("Audiowide-Regular.ttf", "AudioWideRegular");
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // DEPENDENCY INJECTION - Téma na prostudování..
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");

            // tzv. Registrace služby na moje úložiště, AddSingleton - registruje je pouze jednou, poté bude platit v celém projektu.
            builder.Services.AddSingleton(s =>
            ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
