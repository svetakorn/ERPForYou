using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Specialized;
using System.Net;

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

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
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

        #region Command

        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new DelegateCommand(Execute, CanExecute)); }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void Execute(object obj)
        {
            AddToSklad();
        }
        #endregion

        private void AddToSklad()
        {
            Repository.UpdateMaterial();
            if (!string.IsNullOrWhiteSpace(_selectedType) && (_quantity > 0) && !string.IsNullOrEmpty(_selectedMaterial))
            {
                NameValueCollection Info = new NameValueCollection();
                Info.Add("id_material", (from t in Repository.Materials where t.Name == _selectedMaterial select t.Id.ToString()).Single());
                Info.Add("quantity", _quantity.ToString());

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_to_sklad", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");

                byte[] InsertInfo_ostatok = client.UploadValues("http://kornilova.styleru.net/proga/add_to_ostatok", "POST", Info);

                _selectedType = "";
                _quantity = 0;
                _selectedMaterial = "";
                _ue = "";
                OnPropertyChanged("SelectedType");
                OnPropertyChanged("SelectedMaterial");
                OnPropertyChanged("Quantity");
                OnPropertyChanged("Ue");
            }
            else
            {
                _selectedType = "";
                _quantity = 0;
                _selectedMaterial = "";
                _ue = "";
                OnPropertyChanged("SelectedType");
                OnPropertyChanged("SelectedMaterial");
                OnPropertyChanged("Quantity");
                OnPropertyChanged("Ue");
                MessageBox.Show("Данные введены не полностью или неверно!");
            }
        }
    }
}
