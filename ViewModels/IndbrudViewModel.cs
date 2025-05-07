using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;

namespace HousePriceing.ViewModels
{
    public partial class IndbrudViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string indbrudSoruceImage;
        private Indbrudscraper _scraper;
        public IndbrudViewModel(Indbrudscraper _scraper)
        {
            this._scraper = _scraper;
        }
        public void OnIndbrudPageAppear(object? sender, bool e)
        {

            ShowUserInformation();
        }
        private async void ShowUserInformation()
        {
            var data = await _scraper.GetInformationAboutHouseBreakin();
            if (data != null)
            {
                switch (data)
                {
                    case "meget lav":
                        IndbrudSoruceImage = "inbrudmegetlav.png";
                        break;
                    case "lav":
                        IndbrudSoruceImage = "indbrudlav.png";
                        break;
                    case "mellem":
                        IndbrudSoruceImage = "indbrudmellem.png";
                        break;
                    case "høj":
                        IndbrudSoruceImage = "indbrudhoej.png";
                        break;
                    case "meget høj":
                        IndbrudSoruceImage = "indbrudmegethoej.png";
                        break;
                    default:
                        IndbrudSoruceImage = "";
                        break;
                   
                }
            }
        }
    }
}
