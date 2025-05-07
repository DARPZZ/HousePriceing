using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace HousePriceing.ViewModels
{
    public partial class TrafikStøjViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string lydNiveau;
        private TrafikStøjScraper støjScraper;
        public TrafikStøjViewModel(TrafikStøjScraper støjScraper)
        {
            this.støjScraper = støjScraper;
            
        }
        public void OnTrakfikStøjPageAppear(object? sender, bool e)
        {
            LydNiveau = "";
            ShowUserTrafikStøj();
           
        }
        public async void ShowUserTrafikStøj()
        {
            var data =  await støjScraper.hentStøjData();
            LydNiveau = data;
        }


    }
}
