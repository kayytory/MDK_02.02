using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class AddPartner : Window
    {
        Entities entities = new Entities();
        /// <summary>
        /// Окно добавления партнера
        /// </summary>
        public AddPartner()
        {
            InitializeComponent();
            cmbType.ItemsSource = entities.Partner_type.ToList();
        }

        private void txtRating_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            checkDigit(sender, e);
        }
        private void checkDigit(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void txtINN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            checkDigit(sender, e);
        }

        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            checkDigit(sender, e);
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRequest addRequest = new AddRequest();
            addRequest.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(txtEmail.Text, pattern))
                {
                    MessageBox.Show("Почта не подходит");
                    return;
                }
                if (Convert.ToInt32(txtRating.Text) > 10 || Convert.ToInt32(txtRating.Text) < 0)
                {
                    MessageBox.Show("Рейтинг не подходит");
                    return;
                }

                var newPartner = new Partners
                {
                    id_partner_type = (cmbType.SelectedItem as Partner_type).id,
                    name = txtName.Text,
                    director = txtDir.Text,
                    email = txtEmail.Text,
                    phone = txtPhone.Text,
                    address = txtAddress.Text,
                    inn = txtINN.Text,
                    rating = Convert.ToInt32(txtRating.Text)
                };
                entities.Partners.Add(newPartner);
                entities.SaveChanges();
                MessageBox.Show("Сохранено");
                AddRequest addRequest = new AddRequest();
                addRequest.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
