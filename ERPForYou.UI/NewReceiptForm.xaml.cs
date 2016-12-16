using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для NewReceiptForm.xaml
    /// </summary>
    public partial class NewReceiptForm : Page
    {
        public NewReceiptForm()
        {
            InitializeComponent();

            Data.Repository.UpdateMaterial();
            var resultMaterial = from s in Data.Repository.Materials select s.Name;
            comboBoxMaterial.ItemsSource = resultMaterial.ToList();

            Data.Repository.UpdateType();
            var resultType = from s in Data.Repository.Types select s.Name;
            comboBoxType.ItemsSource = resultType.ToList();
        }

        private void editType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("EditingType.xaml", UriKind.Relative));
        }

        private void editMaterial_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditingMaterial(comboBoxType.SelectedItem.ToString()));
        }
    }
}
