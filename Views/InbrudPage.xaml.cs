namespace HousePriceing.Views;

public partial class InbrudPage : ContentPage
{
	public InbrudPage(IndbrudViewModel viewModel)
	{
		InitializeComponent();
        inbrudPage≈bnet += viewModel.OnIndbrudPageAppear;
        BindingContext = viewModel;
	}
    public event EventHandler<bool> inbrudPage≈bnet;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        inbrudPage≈bnet?.Invoke(this, true);

    }
}