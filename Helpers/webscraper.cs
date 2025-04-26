
using System;
using HousePriceing.Models.HouseingModels;
using HtmlAgilityPack;

namespace HousePriceing.Helpers
{
    public class webscraper
    {
        public BasicHouseInformation basicHouseInformation { get; set; }
       
        private string startAdresse = "https://www.dingeo.dk/adresse/";
        LocationHelper locationHelper;
        public webscraper( LocationHelper locationHelper)
        {
            this.locationHelper = locationHelper;
        }
        public async Task<string> GetUrl()
        {
            var adress = $"{startAdresse}{locationHelper.postNummer}-{locationHelper.by}/{locationHelper.vejNavn}-{locationHelper.HusNummer}";
         
            return  adress;
        }
        private HtmlNode AddNotes(string text, HtmlDocument htmlDoc )
        {
            return htmlDoc.DocumentNode.SelectSingleNode($"//strong[text()='{text}:']/following-sibling::text()[1]");
        }
        
        public async Task<bool> ScrapeData()
        {
            try
            {
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(await GetUrl());

                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
             
                if (html != null)
                {
                    var squre = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"ctrldiv\"]/div[4]/div[1]/div[4]/div[1]/div[1]/div/div[2]/div[3]/sup[2]");
                    if(squre == null)
                    {
                        return false;
                    }
                    var squred = squre.InnerText.Trim();
                    ConvertEnergimærke(htmlDoc);
                    BasicHouseInformation basicHouse = new BasicHouseInformation
                        (
                            AddNotes("Udbudspris", htmlDoc).InnerText.Trim().Split("kr.")[0],
                            AddNotes("Type", htmlDoc).InnerText.Trim(),
                            AddNotes("Værelser", htmlDoc).InnerText.Trim(),
                            AddNotes("Boligareal", htmlDoc).InnerText.Trim() + squred,
                            AddNotes("Grund", htmlDoc).InnerText.Trim() + squred,
                            AddNotes("Byggeår", htmlDoc).InnerText.Trim(),
                            ConvertEnergimærke(htmlDoc),
                            AddNotes("Grundskyld", htmlDoc).InnerText.Trim().Split(" ")[0] + "%",
                            AddNotes("Liggetid", htmlDoc).InnerText.Trim()
                        );
                    basicHouseInformation = basicHouse;
                   
                }
            return true;
            }
            catch(Exception ex)
            {
                
                Console.WriteLine("Kunne ikke hente informationer" + ex );
                return false;
            }
        }
        private string ConvertEnergimærke(HtmlDocument htmldoc)
        {
            var energimærkebilelde = htmldoc.DocumentNode.SelectSingleNode("//*[@id=\"ctrldiv\"]/div[4]/div[1]/div[4]/div[1]/div[1]/div/div[2]/div[3]/img");
            var energimærkebilledeTrimmed = energimærkebilelde.OuterHtml.Trim();
            var substringbillede = getBetween(energimærkebilledeTrimmed, "/img/energy/", "-info.png");
            return substringbillede;
        }

        private string getBetween(string strSource, string strStart, string strEnd)
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

    }
}
