using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HousePriceing.Models;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class DistanceScraper : AbStractScraper
    {
        HtmlDocument _document;
        public DistanceScraper(LocationHelper locationHelper) : base(locationHelper)
        {
        }
        public async Task<List<DistanceModel>> ScrapeDistance()
        {

            List<DistanceModel> distancelist = new List<DistanceModel>();
            _document = await LoadHtml(" ");
            var parentNode = _document.DocumentNode.SelectSingleNode("//*[@id=\"afstande\"]/div/div[2]/div/div");
            await Task.Run(() =>
            {
                foreach (var item in parentNode.ChildNodes)
                {
                    if (!item.HasChildNodes)
                    {
                        continue;
                    }
                    foreach (var item1 in item.ChildNodes)
                    {
                        DistanceModel distanceModel = new DistanceModel(
                             item1.SelectSingleNode(".//h4")?.InnerText.Trim(),
                             item1.SelectSingleNode(".//a")?.InnerText.Trim(),
                             item1.SelectSingleNode(".//p[strong]")?.InnerText.Trim()

                        );
                        if (distanceModel.Afstand != null)
                        {
                            distancelist.Add(distanceModel);
                        }
                    }
                }
                
            });
            return distancelist;
        }

    }
}
