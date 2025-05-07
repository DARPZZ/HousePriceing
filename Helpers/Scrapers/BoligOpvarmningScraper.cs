using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class BoligOpvarmningScraper : AbStractScraper
    {
        HtmlDocument _document;
        public BoligOpvarmningScraper(LocationHelper locationHelper) : base(locationHelper)
        {
        }
        public async Task<string> GetBoligOpvarmning()
        {
          
            _document = await LoadHtml(" ");
            try
            {
                var squre = _document.DocumentNode.SelectSingleNode("//*[@id=\"fjernvarme\"]/div/div[1]/h2/text()");

                if (squre != null)
                {
                    var boligOpvarmningType = _document.DocumentNode.SelectSingleNode("//*[@id=\"fjernvarme\"]/div/div[2]/div/div[1]/p[1]/text()");
                    if(boligOpvarmningType != null)
                    {
                        return boligOpvarmningType.InnerText;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kunne ikke hente informationer" + ex);
            }
            return"";
        }

    }
}
