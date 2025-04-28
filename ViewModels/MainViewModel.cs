
using HousePriceing.Helpers;
using HousePriceing.Helpers.Scrapers;
using HousePriceing.Models.HouseingModels;

namespace HousePriceing.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    #region  ObservableProperty
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
    #endregion

    private LocationHelper locationHelper;
    private ScrapeHousesForSale scrapeHousesForSale;
    private ScrapeHousesNotForSale notForSale;
    public MainViewModel(ScrapeHousesForSale scrapeHousesForSale, LocationHelper locationHelper, ScrapeHousesNotForSale scrapeHousesNotForSale)
    {
        this.scrapeHousesForSale = scrapeHousesForSale;
        this.locationHelper = locationHelper;
        GridVisibleIfHouseIsOnSale = false;
        GridVisibleIfHouseIsNotOnSale = false;
        notForSale = scrapeHousesNotForSale;
    }
    private async Task PlaceValuesIfHouseIsOnSale(BasicHouseInformation data)
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
    private async Task PlaceValuesIfHouseIsNotOnSale(BasicHouseInformation data)
    {
        Estimat = data.Estimat;
        Opførselsesår = data.Opførselsesår;
        Boligtype = data.Boligtype;
        Ombygningsår = data.Ombygningsår;
        AntalEtager = data.AntalEtager;
        Antaltoiletter = data.Antaltoiletter;
        Antalbadeværelser = data.Antalbadeværelser;
        Antalværelser = data.Antalværelser;
        SamletAreal = data.SamletAreal;
        VægtetAreal = data.VægtetAreal;

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
        GridVisibleIfHouseIsOnSale = false;
        GridVisibleIfHouseIsNotOnSale = false;
        await Task.Run(() => ClearFeilds());
        await Task.Run(() => GetSetVairabels("set"));
        bool isHouseOnSale = await scrapeHousesForSale.CheckIfHouseIsOnSale();
        if (isHouseOnSale) 
        {
           var data = await scrapeHousesForSale.InformationAboutHouseWhenOnSale();
           if (data != null)
           {
               GridVisibleIfHouseIsOnSale = true;
               GridVisibleIfHouseIsNotOnSale = false;
               await PlaceValuesIfHouseIsOnSale(data);
           }
        } else
        {
            GridVisibleIfHouseIsOnSale = false;
            GridVisibleIfHouseIsNotOnSale = true;
            var data = await notForSale.InformationAboutHouseNotOnSale();
            if(data != null)
            {
                await PlaceValuesIfHouseIsNotOnSale(data);
            }
            
        }

    }
    private async void ClearFeilds()
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
