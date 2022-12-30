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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrEmpty(CustomerEmail.Text) || string.IsNullOrEmpty(CustomerAddress.Text))
            {
                MessageBox.Show("ERROR");
                return;
            }

            dataCart.CustomerName = CustomerName.Text;
            dataCart.CustomerEmail = CustomerEmail.Text;
            dataCart.CustomerAddress = CustomerAddress.Text;

            //dataCart.CustomerName = 
            bl.Cart.OrderConfirmation(dataCart);
        }
    }
}
