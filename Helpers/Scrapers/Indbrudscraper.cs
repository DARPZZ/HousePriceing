using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class Indbrudscraper : AbStractScraper
    {
        HtmlDocument _document;
        public Indbrudscraper(LocationHelper locationHelper) : base(locationHelper)
        {
        }
        public async Task<string>  GetInformationAboutHouseBreakin()
        {
            _document = await LoadHtml(" ");
            var inbrudNode = _document.DocumentNode.SelectSingleNode("//*[@id=\"naboerne\"]/div[6]/div/div[2]/div/div[1]/p[1]/u");
            var inbrudText = inbrudNode !=null ? inbrudNode.InnerText :null;
            return inbrudText;
        }

    }
}
