using MathGame.Maui.Data;
using Microsoft.Extensions.Logging;

namespace MathGame.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("CHILLER.ttf", "OpenSansRegular");
				fonts.AddFont("CHILLER.ttf", "OpenSansSemibold");
			});

		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "game.db");
		builder.Services.AddSingleton(s =>
		ActivatorUtilities.CreateInstance<GameRespository>(s, dbPath));

		return builder.Build();
	}
}
