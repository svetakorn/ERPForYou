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
    /// Логика взаимодействия для ReceiptPage.xaml
    /// </summary>
    public partial class ReceiptPage : Page
    {
        public ReceiptPage()
        {
            InitializeComponent();
            Data.Repository.UpdateSklad();
            Data.Repository.UpdateMaterial();
            Data.Repository.UpdateUe();
            var result = from s in Data.Repository.Sklads
                                       select new Data.ViewModel.SkladViewModel
                                       {
                                           material_name = (from c in Data.Repository.Materials where s.Id_material == c.Id select c.Name).Single(),
                                           ue_name = (from c in Data.Repository.Ues where s.Id_ue == c.Id select c.Name).Single(),
                                           Quantity = s.Quantity
                                       };
            receiptTable.ItemsSource = result.ToList();
        }
        //static MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private void newReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            //mainWindow.mainFrame.NavigationService.Navigate(new Uri("NewReceiptForm.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri("NewReceiptForm.xaml", UriKind.Relative));
        }

    }
}