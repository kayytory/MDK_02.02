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
using System.Windows.Shapes;

namespace Palkin_0202
{
    /// <summary>
    /// Логика взаимодействия для CatalogProducts.xaml
    /// </summary>
    public partial class CatalogProducts : Window
    {
        Entities entities = new Entities();
        public CatalogProducts()
        {
            InitializeComponent();
            Window_Loaded();
        }
        public void Window_Loaded()
        {
            dgProducts.ItemsSource = entities.Products.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MaterialCalc materialCalc = new MaterialCalc();
            materialCalc.Show();
            this.Close();
        }
    }
}
