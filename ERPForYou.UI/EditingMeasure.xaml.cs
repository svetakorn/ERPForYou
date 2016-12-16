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
    /// Логика взаимодействия для EditingMeasure.xaml
    /// </summary>
    public partial class EditingMeasure : Page
    {
        WebClient client = new WebClient();
        public EditingMeasure()
        {
            InitializeComponent();
            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Ues select s.Name;
            listBoxMeasure.ItemsSource = result.ToList();
        }

        //Добавление новой единицы измерения
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();
            Info.Add("name", NewMeasure.Text);

            byte[] InsertInfo = client.UploadValues("http://pestova.styleru.net/proga/add_ue", "POST", Info);
            client.Headers.Add("Content-Type", "binary/octet-stream");

            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Ues select s.Name;
            listBoxMeasure.ItemsSource = result.ToList();

            NewMeasure.Text = "";
        }

        //Изменение единицы измерения
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();
            Info.Add("name", listBoxMeasure.SelectedItem.ToString());
            byte[] RemoveInfo = client.UploadValues("http://pestova.styleru.net/proga/remove_ue", "POST", Info);

            NameValueCollection Info1 = new NameValueCollection();
            Info1.Add("name", ChangedMeasure.Text);
            byte[] InsertInfo = client.UploadValues("http://pestova.styleru.net/proga/add_ue", "POST", Info1);

            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Ues select s.Name;
            listBoxMeasure.ItemsSource = result.ToList();

            ChangedMeasure.Text = "";
        }

        //Удаление единицы измерения
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();

            Info.Add("id", 9.ToString());
            byte[] RemoveInfo = client.UploadValues("http://pestova.styleru.net/proga/remove_ue", "POST", Info);

            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Ues select s.Name;
            listBoxMeasure.ItemsSource = result.ToList();
        }
    }
}
