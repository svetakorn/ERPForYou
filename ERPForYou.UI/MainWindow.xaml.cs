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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sideBar.IsVisible)
            {
                sideBar.Visibility = Visibility.Hidden;
                HamburgerButton.Margin = new Thickness(15, 0, 0, 0);
            }
            else
            {
                sideBar.Visibility = Visibility.Visible;
                HamburgerButton.Margin = new Thickness(193, 0, 0, 0);
            }           
        }
    }
}
