namespace HousePriceing.Views;

public partial class LokalområdetPage : ContentPage
{
	public LokalområdetPage(LokalområdetViewModel viewModel)
	{
		InitializeComponent();
        lokalområdetPageÅbnet += viewModel.OnLokalområdetPageÅbnet;
        BindingContext = viewModel;
    }
    public event EventHandler<bool> lokalområdetPageÅbnet;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        lokalområdetPageÅbnet?.Invoke(this, true);

    }
}