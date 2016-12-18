using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPForYou.ViewModel.ViewModelPattern;
using System.ComponentModel;
using System.Net;
using System.Collections.Specialized;
using System.Windows;

namespace ERPForYou.ViewModel
{
    public class ExpenditureViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<ZakazViewModelPattern> _zakazList;
        public List<ZakazViewModelPattern> ZakazList
        {
            get { return ZakazRequest(); }
            set
            {
                _zakazList = value;
                OnPropertyChanged("ZakazList");
            }
        }

        private ZakazViewModelPattern _selectedItem;
        public ZakazViewModelPattern SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ExpenditureViewModel()
        {
            Repository.UpdateZakaz();
            Repository.UpdateTrademark();
            Repository.UpdateAgent();
            ZakazList = ZakazRequest();
        }

        private List<ZakazViewModelPattern> ZakazRequest()
        {
            Repository.UpdateZakaz();
            return (from s in Repository.Zakazs
                    orderby s.Datetime descending
                    select new ZakazViewModelPattern
                    {
                        DateTime = s.Datetime,
                        Agent = (from c in Repository.Agents where s.Id_agent == c.Id select c.Name).Single(),
                        Trademark = (from c in Repository.Trademarks where s.Id_trademark == c.Id select c.Name).Single(),
                        Price = s.Price,
                        Quantity = s.Quantity,
                        Number = s.Num_zakaz
                    }).ToList();
        }

        #region Command

        private DelegateCommand _deleteCommand;
        public DelegateCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new DelegateCommand(Execute, CanExecute)); }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }

        private void Execute(object obj)
        {
            Delete();
        }
        #endregion

        private void Delete()
        {
            if (_selectedItem != null)
            {
                Repository.UpdateZakaz();
                NameValueCollection Info = new NameValueCollection();
                Info.Add("id", (from s in Repository.Zakazs where SelectedItem.Number == s.Num_zakaz select s.Id.ToString()).Single());
               
                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/remove_zakaz", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");

                OnPropertyChanged("ZakazList");
            }
            else
            {
                MessageBox.Show("Ошибка!\nЧто-то пошло не так.");
            }
        }
    }
}
