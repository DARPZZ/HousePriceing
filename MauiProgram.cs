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
		builder.Services.AddSingleton<ConnectivityTest>();

		builder.Services.AddSingleton<BoligOpvarmningPage>();

		builder.Services.AddSingleton<BoligOpvarmningViewModel>();
		builder.Services.AddSingleton<BoligOpvarmningScraper>();
		builder.Services.AddSingleton<TrafikStøjViewModel>();
		builder.Services.AddSingleton<TrafikStøjPage>();
		builder.Services.AddSingleton<TrafikStøjScraper>();

		builder.Services.AddSingleton<InbrudPage>();
		builder.Services.AddSingleton<IndbrudViewModel>();
		builder.Services.AddSingleton<Indbrudscraper>();

        builder.Services.AddSingleton<OversvømmelsePage>();
        builder.Services.AddSingleton<OversvømmelseViewModel>();
        builder.Services.AddSingleton<OversvømmelseScraper>();

		builder.Services.AddSingleton<DistancePage>();
		builder.Services.AddSingleton<DistanceViewModel>();
		builder.Services.AddSingleton<DistanceScraper>();

        return builder.Build();
	}
}
