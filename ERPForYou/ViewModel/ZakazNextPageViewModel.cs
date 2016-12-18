using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPForYou.ViewModel
{
    public class ZakazNextPageViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();

        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> _typeList;

        private List<string> _materialList;
        public List<string> TypeList { get { return _typeList; } set { } }

        //Доступен ли combobox с наименованиями
        public bool IsSelectedType { get; set; }


        public ZakazNextPageViewModel()
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

        private string _idZakaz;
        public string IdZakaz
        {
            get { return _idZakaz; }
            set
            {
                _idZakaz = value;
                OnPropertyChanged("IdZakaz");
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
            Repository.UpdateUe();
            _ue = (from u in Repository.Ues where (from m in Repository.Materials where m.Name == SelectedMaterial select m.Id_ue).Single() == u.Id select u.Name).Single();
            OnPropertyChanged("Ue");
        }

        public List<string> MaterialList { get { return _materialList; } set { } }

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
            if (!string.IsNullOrWhiteSpace(_selectedType) && (_quantity > 0) && !string.IsNullOrEmpty(_selectedMaterial))
            {
                NameValueCollection Info = new NameValueCollection();
                Info.Add("id_zakaz", _idZakaz.ToString());
                Info.Add("id_material", (from t in Repository.Materials where t.Name == _selectedMaterial select t.Id.ToString()).Single());
                Info.Add("quantity", _quantity.ToString());

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_zakaz_material", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");
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
                MessageBox.Show("Данные введены не полностью или неверно!");
            }
        }
    }
}
