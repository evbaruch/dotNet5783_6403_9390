using System.Windows;
using System.Windows.Input;

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
        NewOrder dataNewOrder = new NewOrder();
        public Cart(BO.Cart cart, NewOrder newOrder)
        {
            InitializeComponent();
            ListCart.ItemsSource = cart.listOfOrderItem;
            TotalPrice.Text = cart.TotalPrice.ToString();
            dataCart = cart;
            dataNewOrder = newOrder;
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


            int orderID = bl.Cart.OrderConfirmation(dataCart);




            dataNewOrder.Close();
            this.Close();

            MessageBox.Show($"Thank you for shopping with us" +
                            $" Your order number is:" + orderID);

            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            BO.OrderItem orderItem = new BO.OrderItem();

            orderItem = (BO.OrderItem)ListCart.SelectedItem;

            productItem = bl.Product.ProductDetails(orderItem.ProductID, dataCart);

            CartAndProduct.Product productWindow = new CartAndProduct.Product(
                productItem, dataCart, dataNewOrder, true
                );

            this.Close();
            productWindow.ShowDialog();
        }
    }
}
