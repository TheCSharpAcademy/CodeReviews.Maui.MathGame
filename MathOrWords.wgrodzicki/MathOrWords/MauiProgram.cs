using MathOrWords.Data;
using Microsoft.Extensions.Logging;

namespace MathOrWords
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
                    fonts.AddFont("Fredoka-Regular.ttf", "FredokaRegular");
                    fonts.AddFont("IndieFlower-Regular.ttf", "IndieFlowerRegular");
                });

            // Dependency injection
            // Create database at C:\Users\Rosynant\AppData\Local\mathorwords.db
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mathorwords.db");
            
            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
