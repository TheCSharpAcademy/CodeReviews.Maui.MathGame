using MathGameMauiJMS.Data;

namespace MathGameMauiJMS;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Belanosima-Regular.ttf", "BelanosimaRegular");
			});

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));
        return builder.Build();
	}
}
