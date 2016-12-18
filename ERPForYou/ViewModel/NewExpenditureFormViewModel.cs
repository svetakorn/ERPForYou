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
    public class NewExpenditureFormViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _agent;
        public string Agent
        {
            get { return _agent; }
            set
            {
                _agent = value;
                OnPropertyChanged("Agent");
            }
        }

        private string _trademark;
        public string Trademark
        {
            get { return _trademark; }
            set
            {
                _trademark = value;
                OnPropertyChanged("Trademark");
            }
        }

        private int _idZakaz;
        public int IdZakaz
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
            AddZakaz();
        }
        #endregion

        private void AddZakaz()
        {
            if (!string.IsNullOrWhiteSpace(_agent) && (_idZakaz > 0) && !string.IsNullOrEmpty(_trademark) && (_quantity > 0))
            {
                Repository.UpdateZakaz();
                Repository.UpdateAgent();
                Repository.UpdateTrademark();

                NameValueCollection Info = new NameValueCollection();
                Info.Add("id_trademark", (from t in Repository.Trademarks where t.Name == _trademark select t.Id.ToString()).Single());
                Info.Add("id_agent", (from t in Repository.Agents where t.Name == _agent select t.Id.ToString()).Single());
                Info.Add("quantity", _quantity.ToString());
                Info.Add("num_zakaz", _idZakaz.ToString());

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_zakaz", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");
                _idZakaz = 0;
                _quantity = 0;
                _trademark = "";
                _agent = "";
                
                OnPropertyChanged("IdZakaz");
                OnPropertyChanged("Trademark");
                OnPropertyChanged("Agent");
                OnPropertyChanged("Quantity");
            }
            else
            {
                MessageBox.Show("Данные введены не полностью или неверно!");
            }
        }
    }
}
