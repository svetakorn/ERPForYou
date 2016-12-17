using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace ERPForYou.ViewModel
{
    
    public class AddingNewMaterialViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> UeList { get; set; }
        public AddingNewMaterialViewModel()
        {
            UeList = new ObservableCollection<string>((from t in Repository.Ues select t.Name).ToList());
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }


        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }


        private float _price;
        public float Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }


        private string _selectedUe;
        public string SelectedUe
        {
            get { return _selectedUe; }
            set
            {
                _selectedUe = value;
                OnPropertyChanged("SelectedUe");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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
            AddNewMaterial();
        }
        #endregion

        private void AddNewMaterial()
        {
            bool flag = true;
            var existingNames = (from m in Repository.Materials select m.Name).ToList();
            foreach (var item in existingNames)
            {
                if (item.Trim() == _name) flag = false;
            }
            if (flag == true && !string.IsNullOrWhiteSpace(_name) && (_price > 0) && !string.IsNullOrEmpty(_selectedUe) && !string.IsNullOrEmpty(_type))
            {
                NameValueCollection Info = new NameValueCollection();
                Info.Add("name", _name);
                Info.Add("id_type", (from t in Repository.Types where t.Name == _type select t.Id.ToString()).Single());
                Info.Add("id_ue", (from t in Repository.Ues where t.Name == _selectedUe select t.Id.ToString()).Single());
                Info.Add("unit_price", _price.ToString());

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_material", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");
                _name = "";
                _price = 0;
                _selectedUe = "";
                OnPropertyChanged("Name");
                OnPropertyChanged("Price");
                OnPropertyChanged("SelectedUe");
            }
            else
            {
                MessageBox.Show("Такая единица измерения уже существует в базе данных или данные введены не полностью!");
            }
        }
    }
}
