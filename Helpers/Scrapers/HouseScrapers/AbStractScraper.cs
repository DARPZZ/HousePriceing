using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers.HouseScrapers
{
    public abstract class AbStractScraper
    {
        private string startAdresse = "https://www.dingeo.dk/adresse/";
        protected static HttpClient httpClient = new HttpClient();
        public string estimat { get; set; }
        LocationHelper locationHelper;
        protected AbStractScraper(LocationHelper locationHelper)
        {
            this.locationHelper = locationHelper;
        }

    
        protected string ConvertEnergimærke(HtmlDocument htmldoc)
        {
            var energimærkebilelde = htmldoc.DocumentNode.SelectSingleNode("//*[@id=\"ctrldiv\"]/div[4]/div[1]/div[4]/div[1]/div[1]/div/div[2]/div[3]/img");
            var energimærkebilledeTrimmed = energimærkebilelde.OuterHtml.Trim();
            var substringbillede = getBetween(energimærkebilledeTrimmed, "/img/energy/", "-info.png");
            return substringbillede;
        }

        protected string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }
        protected async Task<string> GetUrl()
        {
            var adress = $"{startAdresse}{locationHelper.postNummer}-{locationHelper.by}/{locationHelper.vejNavn}-{locationHelper.HusNummer}";

            return adress;
        }
        protected async Task<string> GetUrl(string vuderinger)
        {

            var adress = $"{startAdresse}{locationHelper.postNummer}-{locationHelper.by}/{locationHelper.vejNavn}-{locationHelper.HusNummer}/{vuderinger}";

            return adress;
        }
    }

}
