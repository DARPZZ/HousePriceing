using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class ScrapeHousesNotForSale : AbStractScraper
    {
        public ScrapeHousesNotForSale(LocationHelper locationHelper) : base(locationHelper)
        {
        }

        public async Task InformationAboutHouseNotOnSale()
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(await GetUrl("vurdering"));

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc != null)
            {
                estimat = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"AIVurderingnumber\"]").InnerText.Trim();
            }
        }
    }
}
