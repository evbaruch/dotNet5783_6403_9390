using BO;
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

namespace PL.UserWindows.CartAndProduct
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public Product()
        {            
            InitializeComponent();
        }

        BO.Cart dataCart = new BO.Cart();
        BO.ProductItem dataProductItem = new BO.ProductItem();  

        public Product(ProductItem? ProductContent, BO.Cart cart, bool IsUptdat)
        {
            InitializeComponent();
            productTextBlock.Text = ProductContent.ToString();


            if (IsUptdat)
            {
                TextBoxValue.Text = ProductContent.Amount.ToString();
            }
            dataCart = cart;
            dataProductItem = ProductContent;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxValue.Text))
            {
                MessageBox.Show("ERROR No number entered");
                return;
            }

            bl.Cart.AddProduct(dataCart, dataProductItem.ID);
            bl.Cart.UpdateProductQuantity(dataCart, dataProductItem.ID, int.Parse(TextBoxValue.Text));

            //MessageBox.Show(dataCart.ToString());

            this.Close();
        }
    }
}
