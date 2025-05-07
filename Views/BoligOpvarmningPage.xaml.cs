namespace HousePriceing.Views;

public partial class BoligOpvarmningPage : ContentPage
{
	public BoligOpvarmningPage(BoligOpvarmningViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Your code here — runs every time the page appears
        Console.WriteLine("Page appeared!");
    }

}
