using BlApi;
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
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public Cart()
        {
            InitializeComponent();
        }

        BO.Cart dataCart = new BO.Cart();
        public Cart(BO.Cart cart)
        {
            InitializeComponent();
            ListCart.ItemsSource = cart.listOfOrderItem;
            TotalPrice.Text = cart.TotalPrice.ToString();
            dataCart = cart;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrEmpty(CustomerEmail.Text) || string.IsNullOrEmpty(CustomerAddress.Text))
            {
                MessageBox.Show("ERROR");
                return;
            }

            dataCart.CustomerName = CustomerName.Text;
            dataCart.CustomerEmail = CustomerEmail.Text;
            dataCart.CustomerAddress = CustomerAddress.Text;

            
            bl.Cart.OrderConfirmation(dataCart);


            this.Close();
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            BO.OrderItem orderItem = new BO.OrderItem();

            orderItem = (BO.OrderItem)ListCart.SelectedItem;

            productItem = bl.Product.ProductDetails(orderItem.ProductID, dataCart);

            CartAndProduct.Product productWindow = new CartAndProduct.Product(
                productItem, dataCart, true
                );

            this.Close();
            productWindow.ShowDialog();
        }

        private void ListCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
