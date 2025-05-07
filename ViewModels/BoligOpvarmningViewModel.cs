using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;

namespace HousePriceing.ViewModels;

public partial class BoligOpvarmningViewModel : BaseViewModel
{
    [ObservableProperty]
    private string opvarmningText;
    private BoligOpvarmningScraper _scraper;
    public BoligOpvarmningViewModel(BoligOpvarmningScraper _scraper)
    {
        this._scraper = _scraper;
        PlaceValues();
    }
    private async Task PlaceValues()
    {
        var data = await _scraper.GetBoligOpvarmning();
        OpvarmningText = data;
    }
}
