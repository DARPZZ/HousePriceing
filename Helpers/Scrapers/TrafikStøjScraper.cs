using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HousePriceing.Helpers.Scrapers.HouseScrapers;
using HousePriceing.Models;
using HtmlAgilityPack;

namespace HousePriceing.Helpers.Scrapers
{
    public class TrafikStøjScraper : AbStractScraper
    {

        HtmlDocument _document;
        public TrafikStøjScraper(LocationHelper locationHelper) : base(locationHelper)
        {
           
        }
        private string CheckForNullValues(HtmlNode value)
        {
            if (value != null)
            { 
                return value.InnerText.Trim();
            }
            else
            {
                return "Vi kunne ikke finde informationer på lyd niveauet";
            }
        }
        //private HtmlNode AddNotes()
        //{
        //    return;
        //}
        public async Task<string> hentStøjData()
        {
         
            
            _document = await LoadHtml(" ");
            var db = _document.DocumentNode.SelectSingleNode("//*[@id=\"trafikstoej\"]/div/div[2]/div/div[1]/b[2]");
            var decibel = CheckForNullValues(db);
            
            return decibel;
        }
    }
}
