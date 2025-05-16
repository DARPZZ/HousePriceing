using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePriceing.Models
{
    public class DistanceModel
    {
        public DistanceModel()
        {
            
        }
        public string Type { get; set; }
        public string Navn { get; set; }
        public string Afstand { get; set; }

        public DistanceModel(string type, string navn, string afstand)
        {
            Type = type;
            Navn = navn;
            Afstand = afstand;
        }
    }
    
}
