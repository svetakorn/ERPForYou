using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;

namespace ERPForYou.ViewModel
{
    public class ExpenditureViewModel
    {
        public List<ZakazViewModelPattern> ZakazList { get; set; }

        public ExpenditureViewModel()
        {
            Repository.UpdateZakaz();
            Repository.UpdateTrademark();
            Repository.UpdateAgent();
            ZakazList = ZakazRequest();
        }

        private List<ZakazViewModelPattern> ZakazRequest()
        {
            return (from s in Repository.Zakazs
                    select new ZakazViewModelPattern
                    {
                        DateTime = s.Datetime,
                        Agent = (from c in Repository.Agents where s.Id_agent == c.Id select c.Name).Single(),
                        Trademark = (from c in Repository.Trademarks where s.Id_trademark == c.Id select c.Name).Single(),
                        Price = s.Price,
                        Quantity = s.Quantity,
                        Number = s.Num_zakaz
                    }).ToList();
        }
    }
}
