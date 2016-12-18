using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPForYou.ViewModel.ViewModelPattern
{
    public class ZakazViewModelPattern
    {
        public DateTime DateTime { get; set; }
        public string Agent { get; set; }
        public string Trademark { get; set; }
        public int Quantity { get; set; }
        public int Number { get; set; }
        public float Price { get; set; }
    }
}
