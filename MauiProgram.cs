using HousePriceing.Helpers;
using HousePriceing.Helpers.Scrapers;

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
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddScoped<ScrapeHousesForSale>();
		builder.Services.AddScoped<ScrapeHousesNotForSale>();
		builder.Services.AddSingleton<Indbrudscraper>();

		builder.Services.AddSingleton<BlankViewModel>();

		builder.Services.AddSingleton<BlankPage>();

		return builder.Build();
	}
}
