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

        public string Name { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }

        private string _selectedUe;
        public string SelectedUe
        {
            get { return _selectedUe; }
            set
            {
                _selectedUe = value;
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
                if (item.Trim() == Name) flag = false;
            }
            if (flag == true && !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrEmpty(Price) && !string.IsNullOrEmpty(SelectedUe) && !string.IsNullOrEmpty(Type))
            {
                NameValueCollection Info = new NameValueCollection();
                Info.Add("name", Name);
                Info.Add("id_type", (from t in Repository.Types where t.Name == Type select t.Id.ToString()).Single());
                Info.Add("id_ue", (from t in Repository.Ues where t.Name == SelectedUe select t.Id.ToString()).Single());
                Info.Add("unit_price", Price);

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_material", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");

            }
            else
            {
                MessageBox.Show("Такая единица измерения уже существует в базе данных или данные введены не полностью!");
            }
        }
    }
}
