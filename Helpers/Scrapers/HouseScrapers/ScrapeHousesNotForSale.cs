using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HousePriceing.Models.HouseingModels;
using HtmlAgilityPack;
using static System.Net.Mime.MediaTypeNames;

namespace HousePriceing.Helpers.Scrapers
{
    public class ScrapeHousesNotForSale : AbStractScraper
    {

        
        HtmlDocument htmlDoc = new HtmlDocument();
        public ScrapeHousesNotForSale(LocationHelper locationHelper) : base(locationHelper)
        {
           
        }
        private HtmlNode selectNodes(int trNumber)
        {
            var node = htmlDoc.DocumentNode.SelectSingleNode($"//*[@id=\"bbr\"]/div/div[2]/div/div[1]/table[1]/tbody/tr[{trNumber}]/td[2]/strong");
           
            return node;
        }
        private async Task<string> GetPrice()
        {
            var html = await httpClient.GetStringAsync(await GetUrl("vurdering"));
            htmlDoc.LoadHtml(html);
            string node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"AIVurderingnumber\"]").InnerText.Trim();

            return node;
        }

        public async Task<BasicHouseInformation> InformationAboutHouseNotOnSale()
        {
            var pris = await GetPrice();
            var html = await httpClient.GetStringAsync(await GetUrl());
            htmlDoc.LoadHtml(html);
            var samletAreal = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"bbr\"]/div/div[2]/div/div[1]/table[3]/tbody/tr[1]/td[2]/strong").InnerText.Trim();
            var vægtetAreal = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"bbr\"]/div/div[2]/div/div[1]/table[3]/tbody/tr[5]/td[2]/strong").InnerText.Trim();
            BasicHouseInformation basicHouse = new BasicHouseInformation(
                    pris,
                    selectNodes(3).InnerText.Trim(),
                    selectNodes(2).InnerText.Trim(),
                    selectNodes(4).InnerText.Trim(),
                    selectNodes(8).InnerText.Trim(),
                    selectNodes(9).InnerText.Trim(),
                    selectNodes(10).InnerText.Trim(),
                    selectNodes(11).InnerText.Trim(),
                    samletAreal,
                    vægtetAreal
                 );
                return basicHouse;
        }
    }
}
