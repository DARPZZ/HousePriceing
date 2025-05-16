namespace HousePriceing.Views;

public partial class OversvømmelsePage : ContentPage
{
	public OversvømmelsePage(OversvømmelseViewModel viewModel)
	{
		InitializeComponent();
        oversvømmelsePageÅbnet += viewModel.OnOversvømmelsePageAppear;
        BindingContext = viewModel;
    }
    public event EventHandler<bool> oversvømmelsePageÅbnet;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        oversvømmelsePageÅbnet?.Invoke(this, true);

    }
}