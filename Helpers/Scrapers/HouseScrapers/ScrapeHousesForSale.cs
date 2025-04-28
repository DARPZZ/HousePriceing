using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HousePriceing.Models.HouseingModels;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class ScrapeHousesForSale : AbStractScraper
    {
        HtmlDocument htmlDoc = new HtmlDocument();
        public ScrapeHousesForSale(LocationHelper locationHelper) : base(locationHelper)
        {
            
        }
        private HtmlNode AddNotes(string text, HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode($"//strong[text()='{text}:']/following-sibling::text()[1]");
        }
       
        public async Task<bool> CheckIfHouseIsOnSale()
        {
            await LoadHtml(htmlDoc, "");
            try
            {
                var squre = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"ctrldiv\"]/div[4]/div[1]/div[4]/div[1]/div[1]/div/div[2]/div[3]/sup[2]");
                if (squre == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kunne ikke hente informationer" + ex);
            }
            return false;
        }

        public async Task<BasicHouseInformation> InformationAboutHouseWhenOnSale()
        {
            BasicHouseInformation basicHouse = new BasicHouseInformation
                (
                    AddNotes("Udbudspris", htmlDoc).InnerText.Trim().Split("kr.")[0],
                    AddNotes("Type", htmlDoc).InnerText.Trim(),
                    AddNotes("Værelser", htmlDoc).InnerText.Trim(),
                    AddNotes("Boligareal", htmlDoc).InnerText.Trim() +2,
                    AddNotes("Grund", htmlDoc).InnerText.Trim() + 2,
                    AddNotes("Byggeår", htmlDoc).InnerText.Trim(),
                    ConvertEnergimærke(htmlDoc),
                    AddNotes("Grundskyld", htmlDoc).InnerText.Trim().Split(" ")[0] + "%",
                    AddNotes("Liggetid", htmlDoc).InnerText.Trim()
                );
            return basicHouse;
        }
    }
}
