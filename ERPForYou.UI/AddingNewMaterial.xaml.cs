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
    /// Логика взаимодействия для AddingNewMaterial.xaml
    /// </summary>
    public partial class AddingNewMaterial : Page
    {
        WebClient client = new WebClient();
        public AddingNewMaterial()
        {
            InitializeComponent();
        }
        public AddingNewMaterial(string type)
        {
            InitializeComponent();
            textBoxType.Text = type;

            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Ues select s.Name;
            comboBoxMeasure.ItemsSource = result.ToList();
        }

        private void editMeasure_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("EditingMeasure.xaml", UriKind.Relative));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NameValueCollection Info = new NameValueCollection();
            Info.Add("name", textBoxName.Text);
            Info.Add("id_ue", (from u in Data.Repository.Ues where u.Name == comboBoxMeasure.SelectedItem.ToString() select u.Id.ToString()).Single());
            Info.Add("id_type", (from u in Data.Repository.Types where u.Name == textBoxType.Text select u.Id.ToString()).Single());
            Info.Add("unit_price", textBoxPrice.Text);

            byte[] InsertInfo = client.UploadValues("http://kornilova.styleru.net/proga/add_material", "POST", Info);
            //client.Headers.Add("Content-Type", "binary/octet-stream");

            NavigationService.Navigate(new NewReceiptForm());

            textBoxPrice.Text = "";
            textBoxName.Text = "";

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditingMaterial());
        }
    }
}
