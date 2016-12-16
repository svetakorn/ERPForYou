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
    /// Логика взаимодействия для EditingMaterial.xaml
    /// </summary>
    public partial class EditingMaterial : Page
    {
        public EditingMaterial()
        {
            InitializeComponent();
        }
        public EditingMaterial(string type)
        {
            InitializeComponent();
            Data.Repository.UpdateMaterial();
            Data.Repository.UpdateUe();
            Data.Repository.UpdateType();

            labelType.Content = type;

            var result = from s in Data.Repository.Materials
                         where (from t in Data.Repository.Types where t.Name == type select t.Id).Single() == s.Id_type
                         select new Data.ViewModel.MaterialViewModel
                         {
                             Type = type,
                             Material_name = s.Name,
                             Ue = (from c in Data.Repository.Ues where s.Id_ue == c.Id select c.Name).Single(),
                             Unit_price = s.Unit_price
                         };
            dataGridMaterial.ItemsSource = result.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddingNewMaterial(labelType.Content.ToString()));
        }
    }
}
