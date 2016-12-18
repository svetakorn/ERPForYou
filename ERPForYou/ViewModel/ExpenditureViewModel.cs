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
            Repository.UpdateSklad();
            Repository.UpdateMaterial();
            Repository.UpdateUe();
            ZakazList = ZakazRequest();
        }

        private List<ZakazViewModelPattern> ZakazRequest()
        {
            return (from s in Repository.Sklads
                    select new ZakazViewModelPattern
                    {
                        MaterialName = (from c in Repository.Materials where s.Id_material == c.Id select c.Name).Single(),
                        UeName = (from c in Repository.Ues where ((from d in Repository.Materials where s.Id_material == d.Id select d.Id_ue).Single()) == c.Id select c.Name).Single(),
                        Quantity = s.Quantity
                    }).ToList();
        }
    }
}
