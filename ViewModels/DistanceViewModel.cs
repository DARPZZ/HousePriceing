using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers;
using HousePriceing.Models;

namespace HousePriceing.ViewModels
{
    public partial class DistanceViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<DistanceModel> distances = new ObservableCollection<DistanceModel>();

        private DistanceScraper _distanceScraper;
        public DistanceViewModel(DistanceScraper distanceScraper)
        {
            _distanceScraper = distanceScraper;
        }
        public void OnDistancePageAppear(object? sender, bool e)
        {
            Distances = [];
            GetDistanceForDiffrentAreas();
        }
        private async void GetDistanceForDiffrentAreas()
        {
            var data = await _distanceScraper.ScrapeDistance();
            foreach (var item in data)
            {
                Distances.Add(item);
            }

        }
    }
}
