using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;

namespace HousePriceing.ViewModels
{
    public partial class LokalområdetViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string catogori;
        private LokalområdetScraper scraper;
        [ObservableProperty]
        private ObservableCollection<string> naboer = new ObservableCollection<string>();
        public LokalområdetViewModel(LokalområdetScraper scraper)
        {
            this.scraper = scraper;
        }
        public async void OnLokalområdetPageÅbnet(object? sender, bool e)
        {
            Naboer.Clear();
            var data = await scraper.GetInformationAboutLokalOmrådet();
            Catogori = data[0];
            data.RemoveAt(0);
            foreach (var item in data)
            {
                Naboer.Add(item);
            }
        }

    }
}
