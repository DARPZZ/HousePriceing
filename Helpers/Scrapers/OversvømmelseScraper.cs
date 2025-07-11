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
    public class OversvømmelseScraper : AbStractScraper
    {
        HtmlDocument _document;
        public OversvømmelseScraper(LocationHelper locationHelper) : base(locationHelper)
        {
           
        }
      
        public async Task<OversvømmelseModel> GetInformationAboutOversvømmelsesdirektivet()
        {
            _document = await LoadHtml(" ");
            var planTrin1 = _document.DocumentNode.SelectSingleNode("//*[@id=\"oversvoemmelsesdirektiv\"]/div/div[2]/div/div[1]");
            var planTrin1Text = planTrin1 != null ? planTrin1.InnerText : "";
            string platrin2 ="";

            foreach (var item in planTrin1.ChildNodes)
            {
                if(item.Name == "p")
                {
                    
                    platrin2 = item.InnerText.Trim();
                    
                    break;
                }
            }
            OversvømmelseModel oversvømmelsesModel = new OversvømmelseModel(
                    planTrin1Text,
                    platrin2
            );
            return oversvømmelsesModel;
        }
       
        public async Task<string> GetInformationAboutGrundvand()
        {
            _document = await LoadHtml(" ");
            var grundVand = _document.DocumentNode.SelectSingleNode("//*[@id=\"grundvand\"]/div/div[2]/div/div[1]/text()");
            string text = grundVand != null ? grundVand.InnerText.Trim() : null;
            return text;
        }
    }
}
