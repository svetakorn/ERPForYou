using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.Data
{
    class Zakaz
    {
        public Trademark Trademark { get; set; }
        public int MyProperty { get; set; }
        public Agent Agent { get; set; }
        public int Quantity { get; set; }
        public DateTime Datetime { get; set; }

    }
}
