using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using ERPForYou.Model;
using System.Windows;
using ERPForYou.ViewModel.ViewModelPattern;
using System.Linq;

namespace ERPForYou.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ReceiptViewModel
    {
        public List<SkladViewModelPattern> SkladList { get; set; }

        public ReceiptViewModel()
        {
            Repository.UpdateSklad();
            Repository.UpdateMaterial();
            Repository.UpdateUe();
            SkladList = SkladRequest();
        }

        private List<SkladViewModelPattern> SkladRequest()
        {
            return (from s in Repository.Sklads
                    select new SkladViewModelPattern
                    {
                        MaterialName = (from c in Repository.Materials where s.Id_material == c.Id select c.Name).Single(),
                        UeName = (from c in Repository.Ues where ((from d in Repository.Materials where s.Id_material == d.Id select d.Id_ue).Single()) == c.Id select c.Name).Single(),
                        Quantity = s.Quantity
                    }).ToList();
        }
    }
}