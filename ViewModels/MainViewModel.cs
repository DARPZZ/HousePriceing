
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using HousePriceing.Helpers;
using HousePriceing.Helpers.Scrapers;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HousePriceing.Models.HouseingModels;
using HtmlAgilityPack;
using Mapsui.Projections;
using Mapsui.Tiling;
namespace HousePriceing.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    #region  ObservableProperty

    [ObservableProperty]
    private bool locationChangeText;
    [ObservableProperty]
    private bool showmap;
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
    private bool netWorkState;

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

    [ObservableProperty]
    private string estimat;
    [ObservableProperty]
    private string opførselsesår;
    [ObservableProperty]
    private string boligtype;
    [ObservableProperty]
    private string ombygningsår;

    [ObservableProperty]
    private string samletAreal;
    [ObservableProperty]
    private string vægtetAreal;
    [ObservableProperty]
    private string antalEtager;
    [ObservableProperty]
    private string antaltoiletter;
    [ObservableProperty]
    private string antalbadeværelser;
    [ObservableProperty]
    private string antalværelser;

    [ObservableProperty]
    private bool gridVisibleIfHouseIsOnSale;
    [ObservableProperty]
    private bool gridVisibleIfHouseIsNotOnSale;

    [ObservableProperty]
    private string senestesalgspris;
    [ObservableProperty]
    private string senestesalgsprisDato;

    [ObservableProperty]
    private bool showHouseError;
    [ObservableProperty]
    private bool showHouseEnable;
    #endregion
    public IRelayCommand PinMapCommand { get; }

    private LocationHelper locationHelper;
    private ScrapeHousesForSale scrapeHousesForSale;
    private LatestSalePrice latestSalePrice;
    private ScrapeHousesNotForSale notForSale;
    private ConnectivityTest connectionHelper;

    public MainViewModel(ScrapeHousesForSale scrapeHousesForSale, LocationHelper locationHelper, ScrapeHousesNotForSale scrapeHousesNotForSale, ConnectivityTest connectionHelper, LatestSalePrice latestSalePrice)
    {
        this.latestSalePrice = latestSalePrice;
        this.scrapeHousesForSale = scrapeHousesForSale;
        this.locationHelper = locationHelper;
        GridVisibleIfHouseIsOnSale = false;
        GridVisibleIfHouseIsNotOnSale = false;
        notForSale = scrapeHousesNotForSale;
        this.connectionHelper = connectionHelper;
        ShowNetworkState();
        connectionHelper.NetworkStatusChanged += OnNetworkStatusChanged;
    }

    
    private void OnNetworkStatusChanged(object? sender, bool e)
    {
        if (e)
        {
            NetWorkState = false;
            ShowHouseEnable = true;
        }
        else
        {
            NetWorkState = true;
            ShowHouseEnable = false;
        }
    
    }
    private async Task  LatestPriceData()
    {
        var latestSalePriceing = await latestSalePrice.GetLatestSalePrice();
        if (latestSalePriceing != null) {
            var pris = latestSalePriceing.GetValueOrDefault("Pris");
            var dato = latestSalePriceing.GetValueOrDefault("Dato");
            Senestesalgspris = pris;
            SenestesalgsprisDato = "Seneste salg: " + dato;
        }
        
    }
    private async Task PlaceValuesIfHouseIsOnSale(BasicHouseInformation data)
    {
        await LatestPriceData();
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
    private void ShowNetworkState()
    {

       var firstState = connectionHelper.GetfirstNetworkState();
        if (firstState) 
        {
            ShowHouseEnable = true;
            NetWorkState = false;
        }
        else
        {
            NetWorkState = true;
        }

    }
    private void ClearCache()
    {
        scrapeHousesForSale.ClearCache();
    }
    private async Task PlaceValuesIfHouseIsNotOnSale(BasicHouseInformation data)
    {
        await LatestPriceData();
        Estimat = data.Estimat;
        Opførselsesår = data.Opførselsesår;
        Boligtype = data.Boligtype;
        Ombygningsår = data.Ombygningsår;
        AntalEtager = data.AntalEtager;
        Antaltoiletter = data.Antaltoiletter;
        Antalbadeværelser = data.Antalbadeværelser;
        Antalværelser = data.Antalværelser;
        SamletAreal = data.SamletAreal;
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
            case "set":
                locationHelper.vejNavn = Vejnavn;
                locationHelper.HusNummer = Husnummer;
                locationHelper.by = By;
                locationHelper.postNummer = Postnummer;
                break;
        }
    }
    private void Sendmessage()
    {
        var coordinates = (Longitude: locationHelper.longi, Latitude: locationHelper.lati);
        WeakReferenceMessenger.Default.Send(new GenericValueChangedMessage<(double Longitude, double Latitude)>(coordinates));
    }
    [RelayCommand]
    private async Task OnStartStopTripClicked()
    {
        Showmap = true;
        await locationHelper.GetCurrentLocation();
        await locationHelper.GetURLStringFromCordiantes(locationHelper.lati, locationHelper.longi);
        Sendmessage();
        await GetSetVairabels("get");
       
    }
    [RelayCommand]
    private async Task OnSeBoligClicked()
    {

        ClearCache();
        GridVisibleIfHouseIsOnSale = false;
        GridVisibleIfHouseIsNotOnSale = false;
        ShowHouseError = false;
        ClearFeilds();
        try
        {
            await Task.Run(() => GetSetVairabels("set"));
            bool isHouseOnSale = await scrapeHousesForSale.CheckIfHouseIsOnSale();
            if (isHouseOnSale)
            {
                var data = await scrapeHousesForSale.InformationAboutHouseWhenOnSale();
                if (data != null)
                {
                    await PlaceValuesIfHouseIsOnSale(data);
                    GridVisibleIfHouseIsOnSale = true;
                    GridVisibleIfHouseIsNotOnSale = false;
                }
            }
            else
            {
                var data = await notForSale.InformationAboutHouseNotOnSale();
                if (data != null)
                {
                    await PlaceValuesIfHouseIsNotOnSale(data);
                }
                GridVisibleIfHouseIsOnSale = false;
                GridVisibleIfHouseIsNotOnSale = true;
            }
            
            Showmap = true;
        }
        catch (Exception ex)
        {
            GridVisibleIfHouseIsOnSale = false;
            GridVisibleIfHouseIsNotOnSale = false;
            ShowHouseError = true;

        }
    }

    private void ClearFeilds()
    {
        Udbudspris = "";
        Type = "";
        Værelser = "";
        Boligareal = "";
        Grund = "";
        Byggeår = "";
        Energimærke = "";
        Grundskyld = "";
        Liggetid = "";
        Estimat = "";
        Opførselsesår = "";
        Boligtype = "";
        Ombygningsår = "";
        AntalEtager = "";
        Antaltoiletter = "";
        Antalbadeværelser = "";
        Antalværelser = "";
        SamletAreal = "";
        VægtetAreal = "";
    }
  
}
