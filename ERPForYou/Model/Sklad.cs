using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Model
{
    public class Sklad
    {
        public int Type { get; set; }
        public int Id_material { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
    }
}
