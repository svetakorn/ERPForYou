using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;
using System.ComponentModel;
using System.Windows;

namespace ERPForYou.ViewModel
{
    public class NewReceiptFormViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> _typeList;

        private List<string> _materialList;
        public List<string> TypeList { get { return _typeList; } set { } }
        
        //Доступен ли combobox с наименованиями
        public bool IsSelectedType { get; set; }


        public NewReceiptFormViewModel()
        {
            Repository.UpdateType();
            _typeList = (from t in Repository.Types select t.Name).ToList();
            IsSelectedType = false;
        }


        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                InitializeMaterials();
            }
        }

        private string _selectedMaterial;
        public string SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                _selectedMaterial = value;
                ShowUe();
            }
        }

        private string _ue;
        public string Ue
        {
            get { return _ue; }
            set
            {
                _ue = value;
                OnPropertyChanged("Ue");
            }
        }

        private void ShowUe()
        {
            _ue = (from u in Repository.Ues where (from m in Repository.Materials where m.Name == SelectedMaterial select m.Id_ue).Single() == u.Id select u.Name).Single();
            OnPropertyChanged("Ue");
        }

        public List<string> MaterialList { get { return _materialList; } set {  } }

        private void InitializeMaterials()
        {
            if (_selectedType != null)
            {
                Repository.UpdateMaterial();
                _materialList = (from m in Repository.Materials where (from t in Repository.Types where t.Name == _selectedType select t.Id).Single() == m.Id_type select m.Name).ToList();
                IsSelectedType = true;
                OnPropertyChanged("MaterialList");
                OnPropertyChanged("IsSelectedType");
            }          
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
