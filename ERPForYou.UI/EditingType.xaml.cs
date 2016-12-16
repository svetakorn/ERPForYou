using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERPForYou.UI
{
    /// <summary>
    /// Логика взаимодействия для AddingNewType.xaml
    /// </summary>
    public partial class AddingNewType : Page
    {
        WebClient client = new WebClient();
        public AddingNewType()
        {
            InitializeComponent();

            InitializeComponent();
            Data.Repository.UpdateType();
            var result = from s in Data.Repository.Types select s.Name;
            listBoxType.ItemsSource = result.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();
            Info.Add("name", NewType.Text);

            byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_type", "POST", Info);
            //client.Headers.Add("Content-Type", "binary/octet-stream");

            Data.Repository.UpdateType();
            var result = from s in Data.Repository.Types select s.Name;
            listBoxType.ItemsSource = result.ToList();

            NewType.Text = "";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();
            string id_str = (from c in Data.Repository.Types where c.Name == listBoxType.SelectedItem.ToString() select c.Id.ToString()).Single();
            Info.Add("id", id_str);

            byte[] RemoveInfo = client.UploadValues("http://kornilova.styleru.net/proga/remove_type", "POST", Info);
            //client.Headers.Add("Content-Type", "binary/octet-stream");

            Data.Repository.UpdateType();
            var result = from s in Data.Repository.Types select s.Name;
            listBoxType.ItemsSource = result.ToList();
        }
    }
}
