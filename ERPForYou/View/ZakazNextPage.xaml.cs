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
    /// Логика взаимодействия для ZakazNextPage.xaml
    /// </summary>
    public partial class ZakazNextPage : Page
    {
        public int flag;
        public ZakazNextPage(string num_zakaz, int quantity)
        {
            if (quantity > 0)
            {
            InitializeComponent();
            textBoxNum.IsReadOnly = true;
            textBoxNum.Text = num_zakaz;
                flag = quantity;
            }   
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (flag == 1)
            {
                NavigationService.Navigate(new ExpenditurePage());
            }
            else
            NavigationService.Navigate(new ZakazNextPage(textBoxNum.Text, flag - 1));
        }
    }
}
