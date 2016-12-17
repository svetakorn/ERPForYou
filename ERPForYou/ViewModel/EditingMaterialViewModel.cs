using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPForYou.ViewModel
{
    public class EditingMaterialViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _selType;
        public string SelectedType
        { get
            {
                return _selType;
            }
            set
            {
                _selType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public EditingMaterialViewModel()
        {
            Messenger.Default.Register<string>(this, p => MessageBox.Show(p));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
