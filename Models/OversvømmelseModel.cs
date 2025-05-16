using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HousePriceing.Models
{
    public class OversvømmelseModel
    {
        public string Plantrin1 { get; set; }
        public string Plantrin2 { get; set; }
        public OversvømmelseModel()
        {
            
        }

        public OversvømmelseModel(string plantrin1, string plantrin2)
        {
            Plantrin1 = plantrin1;
            Plantrin2 = plantrin2;
        }
    }
}
