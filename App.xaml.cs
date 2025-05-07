namespace HousePriceing;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        MainPage = new AppShell();
		

	}
}
