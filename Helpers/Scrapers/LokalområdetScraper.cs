using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class LokalområdetScraper : AbStractScraper
    {
        HtmlDocument _document;
        public LokalområdetScraper(LocationHelper locationHelper) : base(locationHelper)
        {

        }

        List<string> naboListe = new List<string>();
        public async Task<List<string>> GetInformationAboutLokalOmrådet()
        {
            naboListe.Clear();
            _document = await LoadHtml(" ");

            var catogori = _document.DocumentNode.SelectSingleNode("//*[@id=\"naboerne\"]/div[1]/div[1]/h2/small");
            
            var catogoriText = catogori != null ? catogori.InnerText : null;

            naboListe.Add(catogoriText);
            GetTableInfo();
            return naboListe;
        }
        private void GetTableInfo()
        {
            var parentNode = _document.DocumentNode.SelectSingleNode("//*[@id=\"naboerne\"]/div[1]/div[2]/div/div[1]/div/div[1]/ul");
            foreach (var item in parentNode.ChildNodes)
            {

                if(item != null && item.Name =="li")
                {
                    string htmlDeCodedItem = WebUtility.HtmlDecode(item.InnerText);
                    naboListe.Add(htmlDeCodedItem);
                }
            }
            
        }

    }
}
