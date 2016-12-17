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
    /// Логика взаимодействия для EditingMaterial.xaml
    /// </summary>
    public partial class EditingMaterial : Page
    {
        public EditingMaterial(string type)
        {
            InitializeComponent();
            labelType.Content = type;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddingNewMaterial(labelType.Content.ToString()));
        }
    }
}
