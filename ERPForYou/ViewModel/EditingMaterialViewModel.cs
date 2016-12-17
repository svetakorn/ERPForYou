using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;

namespace ERPForYou.ViewModel
{
    public class EditingMaterialViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("Type"); }
        }

        private List<MaterialViewModelPattern> _materialList;

        public List<MaterialViewModelPattern> MaterialList
        {
            get { return _materialList; }
            set { _materialList = value; OnPropertyChanged("MaterialList"); }
        }

        #region Command

        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new DelegateCommand(Execute, CanExecute)); }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void Execute(object obj)
        {
            Repository.UpdateMaterial();
            Repository.UpdateType();
            Repository.UpdateUe();
            if (Type != null)
            {
                _materialList = (from m in Repository.Materials
                                 where (from t in Repository.Types where t.Name == Type select t.Id).Single() == m.Id_type
                                 select new MaterialViewModelPattern
                                 {
                                     MaterialName = m.Name,
                                     UeName = (from t in Repository.Ues where t.Id == m.Id_ue select t.Name).Single(),
                                     Price = m.Unit_price
                                 }).ToList();
                OnPropertyChanged("MaterialList");
            }
        }
        #endregion
    }
}
