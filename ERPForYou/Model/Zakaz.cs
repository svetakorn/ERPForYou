using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Model
{
    public class Zakaz
    {
        public int Id_trademark { get; set; }
        public int Id_agent { get; set; }
        public int Quantity { get; set; }
        public DateTime Datetime { get; set; }
        public int Id { get; set; }
        public float Price { get; set; }
    }
}
