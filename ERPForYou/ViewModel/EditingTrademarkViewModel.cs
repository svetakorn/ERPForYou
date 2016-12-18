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
    public class EditingTrademarkViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<string> _typeList;

        public ObservableCollection<string> TypeList
        {
            get
            {
                Repository.UpdateTrademark();
                return _typeList = new ObservableCollection<string>((from t in Repository.Trademarks select t.Name).ToList());
            }
            set
            {
                Repository.UpdateTrademark();
                _typeList = new ObservableCollection<string>((from t in Repository.Trademarks select t.Name).ToList());
                OnPropertyChanged("TypeList");
            }
        }


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _newText;

        public string NewText
        {
            get { return _newText; }
            set
            {
                _newText = value;
                OnPropertyChanged("NewText");
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
            return !string.IsNullOrEmpty(_newText); ;
        }

        private void Execute(object obj)
        {
            AddNewType();
        }
        #endregion

        #region Command

        private DelegateCommand _editCommand;
        public DelegateCommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new DelegateCommand(editExecute, editCanExecute)); }
        }

        private bool editCanExecute(object obj)
        {
            return true;
        }

        private void editExecute(object obj)
        {
            EditTrademark();
        }
        #endregion

        private void AddNewType()
        {
            Repository.UpdateTrademark();
            bool flag = true;
            var existingNames = (from m in Repository.Trademarks select m.Name).ToList();
            foreach (var item in existingNames)
            {
                if (item.Trim() == NewText) flag = false;
            }
            if (flag && !string.IsNullOrWhiteSpace(NewText) && !string.IsNullOrEmpty(NewText))
            {
                NameValueCollection Info = new NameValueCollection();
                Info.Add("name", NewText);

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_trademark", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");

                _newText = "";
                OnPropertyChanged("NewText");
                OnPropertyChanged("TypeList");
            }
            else
            {
                MessageBox.Show("Такая торговая марка уже существует в базе данных!");
                _newText = "";
                OnPropertyChanged("NewText");
            }
        }

        private string _selectedItem;

        public string SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("SelectedItem"); }
        }

        private string _changedName;

        public string ChangedName
        {
            get { return _changedName; }
            set { _changedName = value; OnPropertyChanged("ChangedItem"); }
        }


        private void EditTrademark()
        {
            if (_selectedItem != null && !string.IsNullOrEmpty(_changedName) && !string.IsNullOrWhiteSpace(_changedName))
            {

                Repository.UpdateTrademark();
                NameValueCollection Info = new NameValueCollection();
                Info.Add("id", (from t in Repository.Trademarks where t.Name == _selectedItem select t.Id.ToString()).Single());
                Info.Add("name", _changedName);

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/edit_trademark", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");
                OnPropertyChanged("TypeList");
                _changedName = "";
                OnPropertyChanged("ChangedName");
            }
            else MessageBox.Show("Ошибка!\nЧто-то пошло не так.");
        }
    }
}

