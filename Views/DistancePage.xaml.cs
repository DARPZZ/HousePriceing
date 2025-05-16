namespace HousePriceing.Views;

public partial class DistancePage : ContentPage
{
	public DistancePage(DistanceViewModel viewModel)
	{
		InitializeComponent();
        distancePage≈bnet += viewModel.OnDistancePageAppear;
        BindingContext = viewModel;
    }
    public event EventHandler<bool> distancePage≈bnet;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        distancePage≈bnet?.Invoke(this, true);

    }
}