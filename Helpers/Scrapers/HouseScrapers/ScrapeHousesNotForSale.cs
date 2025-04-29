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
            await LoadHtml(htmlDoc, "vurdering");
            string node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"AIVurderingnumber\"]").InnerText.Trim();

            return node;
        }

        public async Task<BasicHouseInformation> InformationAboutHouseNotOnSale()
        {
            var pris = await GetPrice();
            await LoadHtml(htmlDoc,"");
            var samletNode = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"bbr\"]/div/div[2]/div/div[1]/table[3]/tbody/tr[2]/td[2]/strong");
            var samletAreal = samletNode != null ? samletNode.InnerText.Trim() : null;
            BasicHouseInformation basicHouse = new BasicHouseInformation(
                    pris,
                    selectNodes(3).InnerText.Trim(),
                    selectNodes(2).InnerText.Trim(),
                    selectNodes(4).InnerText.Trim(),
                    selectNodes(9).InnerText.Trim(),
                    selectNodes(10).InnerText.Trim(),
                    selectNodes(11).InnerText.Trim(),
                    samletAreal
                 );
                return basicHouse;
        }
    }
}
