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
        HtmlDocument _document;
        public ScrapeHousesForSale(LocationHelper locationHelper) : base(locationHelper)
        {
            
        }
        private HtmlNode AddNotes(string text, HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode($"//strong[text()='{text}:']/following-sibling::text()[1]");
        }
       
        public async Task<bool> CheckIfHouseIsOnSale()
        {
            _document = await LoadHtml(" ");
            try
            {
                var squre = _document.DocumentNode.SelectSingleNode("//*[@id=\"ctrldiv\"]/div[4]/div[1]/div[4]/div[1]/h1/strong");

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
                    AddNotes("Udbudspris", _document).InnerText.Trim().Split("kr.")[0],
                    AddNotes("Type", _document).InnerText.Trim(),
                    AddNotes("Værelser", _document).InnerText.Trim(),
                    AddNotes("Boligareal", _document).InnerText.Trim() +2,
                    AddNotes("Grund", _document).InnerText.Trim() + 2,
                    AddNotes("Byggeår", _document).InnerText.Trim(),
                    ConvertEnergimærke(_document),
                    AddNotes("Grundskyld", _document).InnerText.Trim().Split(" ")[0] + "%",
                    AddNotes("Liggetid", _document).InnerText.Trim()
                );
            return basicHouse;
        }
    }
}
