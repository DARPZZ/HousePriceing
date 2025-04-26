using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePriceing.Models.HouseingModels
{
    public class BasicHouseInformation
    {
        public BasicHouseInformation()
        {
        }

        public BasicHouseInformation(string udbudspris, string type, string værelser, string boligareal, string grund, string byggeår, string energimærke, string grundskyld, string liggetid)
        {
            Udbudspris = udbudspris;
            Type = type;
            Værelser = værelser;
            Boligareal = boligareal;
            Grund = grund;
            Byggeår = byggeår;
            Energimærke = energimærke;
            Grundskyld = grundskyld;
            Liggetid = liggetid;
        }

        public string Udbudspris { get; set; }
        public string Type { get; set; }
        public string Værelser { get; set; }
        public string Boligareal { get; set; }
        public string Grund { get; set; }
        public string Byggeår { get; set; }
        public string Energimærke { get; set; }
        public string Grundskyld { get; set; }
        public string Liggetid { get; set; }
       

    }
    
    
}
