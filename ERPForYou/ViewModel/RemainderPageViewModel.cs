using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;
using System.ComponentModel;

namespace ERPForYou.ViewModel
{
    public class RemainderPageViewModel : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<OstatokViewModelPattern> _ostatokList;
        public List<OstatokViewModelPattern> OstatokList
        {
            get { return OstatokRequest(); }
            set
            {
                _ostatokList = value;
                OnPropertyChanged("OstatokList");
            }
        }

        public RemainderPageViewModel()
        {
            Repository.UpdateZakazMaterial();
            Repository.UpdateSklad();
            Repository.UpdateOstatok();
            Repository.UpdateMaterial();
            OstatokList = OstatokRequest();
        }

        private List<OstatokViewModelPattern> OstatokRequest()
        {
            Repository.UpdateOstatok();
            Repository.UpdateMaterial();
            Repository.UpdateSklad();
            return (from s in Repository.Ostatok
                    select new OstatokViewModelPattern
                    {
                        MaterialName = (from m in Repository.Materials where m.Id == s.Id_material select m.Name).Single(),
                        Quantity = s.Quantity
                    }).ToList();
        }
    }
}
