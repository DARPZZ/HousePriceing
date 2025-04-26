
using HousePriceing.Helpers;
using HousePriceing.Models.HouseingModels;

namespace HousePriceing.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private string vejnavn;
    [ObservableProperty]
    private string husnummer;
    [ObservableProperty]
    private string by;
    [ObservableProperty]
    private string postnummer;
    [ObservableProperty]
    private string pris;


    [ObservableProperty]
    private string udbudspris;
    [ObservableProperty]
    private string type;
    [ObservableProperty]
    private string værelser;
    [ObservableProperty]
    private string boligareal;
    [ObservableProperty]
    private string grund;
    [ObservableProperty]
    private string byggeår;
    [ObservableProperty]
    private string energimærke;
    [ObservableProperty]
    private string grundskyld;
    [ObservableProperty]
    private string liggetid;


    private LocationHelper locationHelper;
    private webscraper webscraper;
    public MainViewModel(webscraper webscraper, LocationHelper locationHelper)
    {
        this.webscraper = webscraper;
        this.locationHelper = locationHelper;

    }
    private async Task PlaceValues(BasicHouseInformation data)
    {
        Udbudspris = data.Udbudspris  + " kr.";
        Type = data.Type;
        Værelser = data.Værelser;
        Boligareal = data.Boligareal;
        Grund = data.Grund;
        Byggeår = data.Byggeår;
        Energimærke = data.Energimærke;
        Grundskyld = data.Grundskyld;
        Liggetid = data.Liggetid;
    }
    private async Task GetSetVairabels(string way)
    {
        switch(way)
        {
            case "get":
                Vejnavn = locationHelper.vejNavn;
                Husnummer = locationHelper.HusNummer;
                By = locationHelper.by;
                Postnummer = locationHelper.postNummer;
                break;
            case "Set":
                locationHelper.vejNavn = Vejnavn;
                locationHelper.HusNummer = Husnummer;
                locationHelper.by = By;
                locationHelper.postNummer = Postnummer;
                break;
        }
    }

    [RelayCommand]
    private async Task OnStartStopTripClicked()
    {
        await locationHelper.GetCurrentLocation();
        await locationHelper.GetURLStringFromCordiantes(locationHelper.lati, locationHelper.longi);
        await GetSetVairabels("get");
    }
    [RelayCommand]
    private async Task OnSeBoligClicked()
    {
        await GetSetVairabels("set");
        if(await webscraper.ScrapeData())
        {
            var data = webscraper.basicHouseInformation;
            await PlaceValues(data);
        }
    }
}
