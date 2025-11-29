using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

    public partial class MaterialCalc : Window
    {
        Entities entities = new Entities();
        /// <summary>
        /// Форма с расчетом стоимости материалов
        /// </summary>
        public MaterialCalc()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CatalogProducts catalogProducts = new CatalogProducts();
            catalogProducts.Show();
            this.Close();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            var calc = Calculate((cmbProduct.SelectedItem as Products).article, (cmbMaterial.SelectedItem as Material).id, Convert.ToInt32(txtAmount.Text), Convert.ToInt32(txtAmountInStock.Text));
            txtResult.Text = calc.ToString();
        }
        /// <summary>
        /// Метод 4 модуль
        /// </summary>
        /// <param name="prodID"></param>
        /// <param name="matID"></param>
        /// <param name="prodNeeded"></param>
        /// <param name="prodInStock"></param>
        /// <returns></returns>
        private int Calculate(string prodID, int matID, int prodNeeded, int prodInStock)
        {
            try
            {
                if (prodNeeded <= 0 || prodInStock < 0)
                {
                    return -1;
                }

                int productionCount = Math.Max(0, prodNeeded - prodInStock);
                if (productionCount == 0)
                {
                    return 0;
                }
                var prod = entities.Products.FirstOrDefault(x => x.article == prodID);
                var material = entities.Material.FirstOrDefault(x => x.id == matID);

                double productTypeFactor = entities.Product_type.FirstOrDefault(x => x.id == prod.id_product_type).factor ?? 1.0;

                //Процент брака материала
                double defectPercentage = entities.Material_type.FirstOrDefault(x => x.id == material.id_material_type).percentage_of_defect ?? 0.0;
                double defectFactor = 1.0 + (defectPercentage / 100.0);

                //Расчет материала на единицу продукции
                double materialPerUnit = prodNeeded * prodInStock * productTypeFactor;

                //Количество материала с учетом брака
                double totalMaterial = materialPerUnit * productionCount * defectFactor;

                //Округление
                return (int)Math.Ceiling(totalMaterial);
            }
            catch
            {
                return -1;
            }
         
        }
    }
}
