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

namespace Palkin_0202
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Класс для вывода листбокса
        public class ViewPartners
        {
            public int id { get; set; }
            public int id_partner { get; set; }
            public string name { get; set; }
            public string partnerType { get; set; }
            public int id_partner_type { get; set; }
            public double cost_for_unit { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public int rating { get; set; }
            public int countInRequest { get; set; }
            public double costAll { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

        }
        Entities entities = new Entities();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем данные из бд в лист
                var requests = (from r in entities.Partner_products_request
                                 join part in entities.Partners on r.id_partner equals part.id
                                 join prod in entities.Products on r.id_product equals prod.article
                                 join partType in entities.Partner_type on part.id_partner_type equals partType.id
                                 select new
                                 {
                                     id = r.id,
                                     id_partner = part.id,
                                     name = part.name,
                                     partnerType = partType.name,
                                     id_partner_type = partType.id,
                                     cost_for_unit = prod.min_cost,
                                     address = part.address,
                                     phone = part.phone,
                                     rating = part.rating,
                                     countInRequest = r.count

                                 }).ToList();
                //переносим данные из одного листа в лист класса ViewPartners
                var PartnersList = requests.Select(m => new ViewPartners
                {
                    id = (int)m.id,
                    name = m.name,
                    id_partner = m.id_partner,
                    partnerType = m.partnerType,
                    id_partner_type = m.id_partner_type,
                    cost_for_unit = (double)m.cost_for_unit,
                    address = m.address,
                    phone = m.phone,
                    rating = (int)m.rating,
                    countInRequest = (int)m.countInRequest,
                    costAll = (int)m.countInRequest * (int)m.cost_for_unit
                }).ToList();

                requestList.ItemsSource = PartnersList.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

                AddRequest addMaterial = new AddRequest();
                addMaterial.Show();
                this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (requestList.SelectedItem == null)
                {
                    MessageBox.Show("Выберите заявку");
                    return;
                }
                    
                AddRequest addMaterial = new AddRequest((requestList.SelectedItem as ViewPartners).id);
                addMaterial.Show();
                this.Close();
            }
            catch { MessageBox.Show("Упс.."); }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CatalogProducts catalogProducts = new CatalogProducts();
            catalogProducts.Show();
            this.Close();
        }
    }
}
