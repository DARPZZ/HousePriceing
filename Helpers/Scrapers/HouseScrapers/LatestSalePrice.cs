using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers.HouseScrapers
{
    public class LatestSalePrice : AbStractScraper
    {
        HtmlDocument htmlDoc;
        public LatestSalePrice(LocationHelper locationHelper) : base(locationHelper)
        {
        }
        public async Task<Dictionary<string,string>> GetLatestSalePrice()
        {

            Dictionary<string, string> data = new Dictionary<string,string>();
            htmlDoc = await LoadHtml("vurdering");
            HtmlNode pris = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[2]/div/ul/li[2]/h4");
            HtmlNode dato = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div[4]/div[2]/div[2]/div/ul/li[2]/small");
            if (pris != null && dato != null) 
            {
                data.Add("Pris", pris.InnerText.Trim());
                data.Add("Dato", dato.InnerText.Trim());
                return data;
            }
            return null;
           
            
        }


    }
}
