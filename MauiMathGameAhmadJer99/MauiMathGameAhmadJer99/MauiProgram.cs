using MauiMathGameAhmadJer99.Data;
using Microsoft.Extensions.Logging;

namespace MauiMathGameAhmadJer99
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
                    fonts.AddFont("ModernAntiqua-Regular.ttf", "ModernAntiquaRegular");

                });

            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "game.db");

            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<GameRepository>(s, databasePath));

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
