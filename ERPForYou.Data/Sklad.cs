using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Data
{
    public class Sklad
    {
        public int Type { get; set; }
        public int Id_material { get; set; }

        public Material Material { get; set; }
        public int Id_ue { get; set; }

        public UE Ue { get; set; }

        public int Quantity { get; set; }
        public int Id { get; set; }
    }
}
