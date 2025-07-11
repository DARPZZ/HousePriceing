using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;

namespace HousePriceing.ViewModels;

public partial class BoligOpvarmningViewModel : BaseViewModel
{
    [ObservableProperty]
    private string opvarmningText;
    [ObservableProperty]
    private string pictureSource;
    private BoligOpvarmningScraper _scraper;
    public BoligOpvarmningViewModel(BoligOpvarmningScraper _scraper)
    {
        this._scraper = _scraper;
        PlaceValues();
    }
    private async Task PlaceValues()
    {
        var data = await _scraper.GetBoligOpvarmning();
        OpvarmningText = "Den primære opvarmningskilde er: ";

        if(data.Contains("Fjernvarme".ToLower()))
        {
            PictureSource = "fjernvarme.png";
            OpvarmningText += "Fjernvarme";
        }else if (data.Contains("Naturgas".ToLower())){
            PictureSource = "naturgas.png";
            OpvarmningText += "Naturgas";
        }else if (data.Contains("Varmepumper".ToLower()))
        {
            PictureSource = "varmevumpe.png";
            OpvarmningText += "Varmepumper eller anden opvarmning";
        }
        else
        {
            PictureSource = "";
            OpvarmningText = "Vi er ikke istand til at finde opvarmnings kilde";
        }
        
    }
}
