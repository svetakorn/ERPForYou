using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Model
{
    public class Zakaz_material
    {
        public int Id { get; set; }
        public int Id_material { get; set; }
        public int Id_zakaz { get; set; }
        public int Quantity { get; set; }
    }
}
