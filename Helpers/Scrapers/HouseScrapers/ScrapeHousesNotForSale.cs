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
        private async Task<bool> CheckIfAIIsCorrect()
        {
            htmlDoc = await LoadHtml("vurdering");
            string node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[3]/div[2]/div/div[2]").InnerText.Trim();
            if(node.Contains("usædvanligvis"))
            {
                return false;
            }
            return true;
        }
        private async Task<string> GetPriceIfAiIsNotCorrect()
        {
            htmlDoc = await LoadHtml("vurdering");
            string node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[1]/div[1]/div/div[2]/div[2]/strong").InnerText.Trim();
            return node;
        }

        private async Task<string> GetPriceIfAiIsCorrect()
        {
            htmlDoc = await LoadHtml("vurdering");
            string node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[1]/div[2]/div[1]/div[2]/div/div/span").InnerText.Trim();
            return node;
        }

        public async Task<BasicHouseInformation> InformationAboutHouseNotOnSale()
        {
            var loadHtmlTask = LoadHtml(" ");
            var checkAi = await CheckIfAIIsCorrect();
            var prisTask = GetPriceIfAiIsCorrect();
            if(!checkAi)
            {
                prisTask = GetPriceIfAiIsNotCorrect();
            }
            await Task.WhenAll(loadHtmlTask, prisTask);
            var pris = prisTask.Result.Trim();
            htmlDoc = loadHtmlTask.Result;
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
