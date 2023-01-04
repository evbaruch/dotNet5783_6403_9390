using BO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Provider;

namespace PL.UserWindows.CartAndProduct
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Window
    {
        

        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart dataCart = new BO.Cart();
        BO.ProductItem dataProductItem = new BO.ProductItem();
        NewOrder dataNewOrder = new NewOrder();
        public Product(ProductItem? ProductContent, BO.Cart cart,NewOrder newOrder, bool IsUptdat)
        {
           InitializeComponent();

            dataProductItem = ProductContent;
            productTextBlock.Text = ProductContent.ToString();
            dataNewOrder = newOrder;
            dataCart = cart;

            if (IsUptdat)
            {
                TextBoxValue.Text = ProductContent.Amount.ToString();
            }
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

            //dataNewOrder.Dispatcher.Invoke();
            //MessageBox.Show(dataCart.ToString());
            dataNewOrder.Dispatcher.Invoke(() =>
            {

                

                foreach (var item in dataNewOrder.productItemForObservableCollection)
                {
                    if (item.ID == dataProductItem.ID)
                    {
                        item.Amount = int.Parse(TextBoxValue.Text);
                    }
                }

                dataNewOrder.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(dataNewOrder.productItemForObservableCollection);    
            });

            this.Close();
        }
    }
}
