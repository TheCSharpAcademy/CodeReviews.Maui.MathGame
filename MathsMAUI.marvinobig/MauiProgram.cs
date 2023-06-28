using MathsMAUI.Data;
using Microsoft.Extensions.Logging;

namespace MathsMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Math.db");
		var builder = MauiApp.CreateBuilder();

		builder
            .UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder
			.Services
			.AddSingleton(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));

		return builder.Build();
	}
}
