using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Data
{
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Id_ue { get; set; }
        public int Id_type { get; set; }
        public float Unit_price { get; set; }

    }
}
