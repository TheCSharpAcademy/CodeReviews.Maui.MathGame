using MathGameMauiJMS.Data;
using System.Reflection;

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

        string assemblyLocation = Assembly.GetExecutingAssembly().Location;
        string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
        string solutionFolderPath = Path.GetFullPath(Path.Combine(assemblyDirectory, "..\\..\\..\\.."));
        string dbPath = Path.Combine(solutionFolderPath, @"\gameDB.db");
        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<GameRepository>(s, dbPath));
        return builder.Build();
	}
}
