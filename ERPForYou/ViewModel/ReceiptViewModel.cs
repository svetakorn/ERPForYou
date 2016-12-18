using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Documents;
using ERPForYou.Model;
using System.Windows;
using ERPForYou.ViewModel.ViewModelPattern;
using System.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace ERPForYou.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ReceiptViewModel : INotifyPropertyChanged
    {
        WebClient client = new WebClient();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private List<SkladViewModelPattern> _skladList;
        public List<SkladViewModelPattern> SkladList
        {
            get { return SkladRequest(); }
            set
            {
                _skladList = value;
                OnPropertyChanged("SkladList");
            }
        }

        private SkladViewModelPattern _selectedItem;
        public SkladViewModelPattern SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public ReceiptViewModel()
        {
            client.Encoding = Encoding.UTF8;
            Repository.UpdateSklad();
            Repository.UpdateMaterial();
            Repository.UpdateUe();
            SkladList = SkladRequest();
        }

        private List<SkladViewModelPattern> SkladRequest()
        {
            Repository.UpdateSklad();
            return (from s in Repository.Sklads
                    orderby s.DateTime descending
                    select new SkladViewModelPattern
                    {
                        MaterialName = (from c in Repository.Materials where s.Id_material == c.Id select c.Name).Single(),
                        UeName = (from c in Repository.Ues where ((from d in Repository.Materials where s.Id_material == d.Id select d.Id_ue).Single()) == c.Id select c.Name).Single(),
                        Quantity = s.Quantity,
                        DateTime = s.DateTime
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
                Repository.UpdateSklad();
                NameValueCollection Info = new NameValueCollection();
                Info.Add("id", (from s in Repository.Sklads where _selectedItem.DateTime == s.DateTime select s.Id.ToString()).Single());

                byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/remove_sklad", "POST", Info);
                //client.Headers.Add("Content-Type", "binary/octet-stream");

                OnPropertyChanged("SkladList");
            }
            else
            {
                MessageBox.Show("Что-то пошло не так!");
            }
        }
    }
}