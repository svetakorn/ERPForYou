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

namespace ERPForYou.View
{
    /// <summary>
    /// Логика взаимодействия для NewPeceiptForm.xaml
    /// </summary>
    public partial class NewPeceiptForm : Page
    {
        public NewPeceiptForm()
        {
            InitializeComponent();
            textBoxUe.IsReadOnly = true;
        }

        private void editType_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/EditingType.xaml", UriKind.Relative));
        }

        private void editMaterial_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditingMaterial(comboBoxType.SelectedItem.ToString()));
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/ReceiptPage.xaml", UriKind.Relative));
        }
    }
}
