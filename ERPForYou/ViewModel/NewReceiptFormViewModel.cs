using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;
using System.ComponentModel;
using System.Windows;
using System.Collections.Specialized;
using System.Net;
using GalaSoft.MvvmLight.Messaging;

namespace ERPForYou.ViewModel
{
    public class NewReceiptFormViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();
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

        //Привязка к полям ввода
        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
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

        //Привязка к полям ввода
        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
                MyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
                MyCommand.RaiseCanExecuteChanged();
            }
        }

        #region Command

        private DelegateCommand _myCommand;
        public DelegateCommand MyCommand
        {
            get { return _myCommand ?? (_myCommand = new DelegateCommand(Execute, CanExecute)); }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void Execute(object obj)
        {
            Messenger.Default.Send<string>(_selectedType);
        }
        #endregion

        //private void AddToMaterialTable()
        //{
        //        NameValueCollection Info = new NameValueCollection();
        //        Info.Add("name", NewText);

        //        byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_material", "POST", Info);
        //        //client.Headers.Add("Content-Type", "binary/octet-stream");
        //}

    }
}
