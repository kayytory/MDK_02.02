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

    public partial class AddRequest : Window
    {
        Entities entities = new Entities();
        Partner_products_request selectedItem;

        /// <summary>
        /// Окно добавления заявки
        /// </summary>
        public AddRequest()
        {
            InitializeComponent();
            cmbPartner.ItemsSource = entities.Partners.ToList();
            cmbProduct.ItemsSource = entities.Products.ToList();
        }
        //Конструктор для редактирования
        public AddRequest(int selectedMaterial)
        {
            InitializeComponent();
            selectedItem = new Partner_products_request();
            selectedItem = entities.Partner_products_request.FirstOrDefault(x=>x.id == selectedMaterial);
            cmbPartner.ItemsSource = entities.Partners.ToList();
            cmbProduct.ItemsSource = entities.Products.ToList();
            cmbPartner.SelectedItem = selectedItem.Partners;
            cmbProduct.SelectedItem = selectedItem.Products;
            txtAmount.Text = selectedItem.count.ToString();
            btnAdd.Content = "Изменить";
            this.Title = "Изменение заказа";
            btnAddPartner.Visibility = Visibility.Collapsed;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //Добавление нового
                if (selectedItem == null)
                {
                    Partner_products_request newRequest = new Partner_products_request
                    {
                        id_partner = (cmbPartner.SelectedItem as Partners).id,
                        id_product = (cmbProduct.SelectedItem as Products).article,
                        count = Convert.ToInt32(txtAmount.Text)
                    };
                    entities.Partner_products_request.Add(newRequest);
                }
                //Редактирование существующего
                else
                {
                    var editingRequest = entities.Partner_products_request.FirstOrDefault(x => x.id == selectedItem.id);
                    editingRequest.id_partner = (cmbPartner.SelectedItem as Partners).id;
                    editingRequest.id_product = (cmbProduct.SelectedItem as Products).article;
                    editingRequest.count = Convert.ToInt32(txtAmount.Text);
                }
                entities.SaveChanges();
                MessageBox.Show("Изменения сохранены");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Проверка на число
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void btnAddPartner_Click(object sender, RoutedEventArgs e)
        {
            AddPartner addPartner = new AddPartner();
            addPartner.Show();
            this.Close();
        }
    }
}
