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
        }
        //static MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        private void newReceiptButton_Click(object sender, RoutedEventArgs e)
        {
            //mainWindow.mainFrame.NavigationService.Navigate(new Uri("NewReceiptForm.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri("NewReceiptForm.xaml", UriKind.Relative));
        }

}
}
