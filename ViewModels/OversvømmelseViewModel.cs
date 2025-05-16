using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;

namespace HousePriceing.ViewModels
{
    public partial class OversvømmelseViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string oversvømmelsePicture;
        [ObservableProperty]
        private string oversvømmelseText;
        OversvømmelseScraper _scraper;
        public OversvømmelseViewModel(OversvømmelseScraper scraper)
        {
            this._scraper = scraper;
        }
        public void OnOversvømmelsePageAppear(object? sender, bool e)
        {
            ShowUserInformation();
           
        }
        private async void ShowUserInformation()
        {
            try
            {
                var data = await _scraper.GetInformationAboutOversvømmelse();
                if (data == null)
                {
                    OversvømmelseText = "Vi kunne ikke hente noget data";
                    return;
                }

                OversvømmelseText = data.Plantrin2;
                if (data.Plantrin2.Contains("Ikke".ToLower()))
                {
                    OversvømmelsePicture = "checkmarkicon.png";
                }
                else
                {
                    OversvømmelsePicture = "dangericon.png";
                }
            }
            catch(Exception ex)
            {
                OversvømmelsePicture = ex.ToString();
            }
        }
    }
}
