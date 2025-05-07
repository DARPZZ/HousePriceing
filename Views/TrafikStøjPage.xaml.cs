

namespace HousePriceing.Views;

public partial class TrafikStøjPage : ContentPage
{
    public TrafikStøjPage(TrafikStøjViewModel viewModel)
    {
        InitializeComponent();
        TrafikStøjÅbnet += viewModel.OnTrakfikStøjPageAppear;
        BindingContext = viewModel;
    }


    public event EventHandler<bool> TrafikStøjÅbnet;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        TrafikStøjÅbnet?.Invoke(this,true);
        
    }
}