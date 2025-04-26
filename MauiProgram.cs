using HousePriceing.Helpers;

namespace HousePriceing;

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

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddSingleton<LocationHelper>();
		builder.Services.AddSingleton<webscraper>();
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<BlankViewModel>();

		builder.Services.AddSingleton<BlankPage>();

		return builder.Build();
	}
}
