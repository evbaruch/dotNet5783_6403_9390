using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;

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

        NewOrder dataNewOrder { get; set; }



        public string productDetails
        {
            get { return (string)GetValue(productDetailsProperty); }
            set { SetValue(productDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productDetailsProperty =
            DependencyProperty.Register("productDetails", typeof(string), typeof(PL.UserWindows.CartAndProduct.Product));



        public int amount
        {
            get { return (int)GetValue(amountProperty); }
            set { SetValue(amountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for amount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty amountProperty =
            DependencyProperty.Register("amount", typeof(int), typeof(PL.UserWindows.CartAndProduct.Product));



        public Product(ProductItem? ProductContent, BO.Cart cart,NewOrder newOrder, bool IsUptdat)
        {
           InitializeComponent();

            dataProductItem = ProductContent;
            productDetails = ProductContent.ToString();
            dataNewOrder = newOrder;
            dataCart = cart;

            if (IsUptdat)
            {
                amount = (int)ProductContent.Amount;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int number;
                bool isNumber = int.TryParse(amount.ToString(), out number);
                if (!isNumber || number < 0)
                {
                    MessageBox.Show("ERROR No number entered");
                    return;
                }

                bl.Cart.AddProduct(dataCart, dataProductItem.ID);
                bl.Cart.UpdateProductQuantity(dataCart, dataProductItem.ID, number);

                dataNewOrder.Dispatcher.Invoke(() =>
                {

                    foreach (var item in dataNewOrder.productItemForObservableCollection)
                    {
                        if (item.ID == dataProductItem.ID)
                        {
                            item.Amount = amount;
                        }
                    }

                    dataNewOrder.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(dataNewOrder.productItemForObservableCollection);
                });

                this.Close();
            }
            catch (BO.DataNotFoundException)
            {
                MessageBox.Show("This product is out of stock");
            }
            
        }
    }
}
