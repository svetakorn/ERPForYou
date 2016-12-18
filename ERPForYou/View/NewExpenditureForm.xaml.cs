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
    /// Логика взаимодействия для NewExpenditureForm.xaml
    /// </summary>
    public partial class NewExpenditureForm : Page
    {
        public NewExpenditureForm()
        {
            InitializeComponent();
        }

        private void EditAgent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/EditingAgent.xaml", UriKind.Relative));
        }

        private void EditTm_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("View/EditingTrademark.xaml", UriKind.Relative));
        }
    }
}
