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
        [ObservableProperty]
        private string ekstremRegnPicture;
        [ObservableProperty]
        private string ekstremRegnText;
        [ObservableProperty]
        private string grundVandOversvømmelsetext;
        [ObservableProperty]
        private string grundVandOversvømmelsePicture;
        OversvømmelseScraper _scraper;
        public OversvømmelseViewModel(OversvømmelseScraper scraper)
        {
            this._scraper = scraper;
        }
        public void OnOversvømmelsePageAppear(object? sender, bool e)
        {
            _ = HandlePageAppearAsync(sender, e);
        }

        private async Task HandlePageAppearAsync(object? sender, bool e)
        {
            var odInfo = ShowUserInformationAboutOD();
            var erInfo = ShowUserInformationAboutER();
            var gvInfo = ShowUserInformationAboutGV();
            await Task.WhenAll(odInfo, erInfo, gvInfo);
        }
        private async Task ShowUserInformationAboutOD()
        {
            try
            {
                var data = await _scraper.GetInformationAboutOversvømmelsesdirektivet();
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
        private string ChangePicture(string data)
        {
            if (data.Contains("Ikke".ToLower()))
            {
                return "checkmarkicon.png";
            }
            else
            {
                return "dangericon.png";
            }
        }
        private async Task ShowUserInformationAboutER()
        {
            try
            {
                var data = await _scraper.GetInformationAboutEkstremRegn();
                if (data == null)
                { 
                    return;
                }
                EkstremRegnText = data;
                EkstremRegnPicture = ChangePicture(data);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        private async Task ShowUserInformationAboutGV()
        {
            try
            {
                var data = await _scraper.GetInformationAboutGrundvand();
                if (data == null)
                {
                    return;
                }
                var grundvandText = data.Replace("ifm", "i forbindelse med");
                GrundVandOversvømmelsetext = grundvandText;
                GrundVandOversvømmelsePicture = ChangePicture(data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }


    }
}
