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

namespace PL.UserWindows
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        BO.Cart cart = new Cart();
        public NewOrder()
        {
            InitializeComponent();
            ProductItemForList.ItemsSource = bl.Product.ProductItemList();
        }

        private void CartWindow(object sender, RoutedEventArgs e)
        {
            CartAndProduct.Cart cartWindow = new CartAndProduct.Cart(cart);
            cartWindow.ShowDialog();
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            CartAndProduct.Product productWindow = new CartAndProduct.Product(
                (BO.ProductItem)ProductItemForList.SelectedItem, cart, false
                );           
            productWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }
    }
}
