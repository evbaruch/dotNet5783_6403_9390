using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PL.UserWindows.CartAndProduct
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public bool IsEnable
        {
            get { return (bool)GetValue(IsEnableProperty); }
            set { SetValue(IsEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnableProperty =
            DependencyProperty.Register("IsEnable", typeof(bool), typeof(PL.UserWindows.CartAndProduct.Cart));



        public BO.Cart cartDetails
        {
            get { return (BO.Cart)GetValue(cartDetailsProperty); }
            set { SetValue(cartDetailsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cartDetails.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartDetailsProperty =
            DependencyProperty.Register("cartDetails", typeof(BO.Cart), typeof(PL.UserWindows.CartAndProduct.Cart));



        private ObservableCollection<BO.OrderItem> _listOfOrderItemForObservableCollection;
        public ObservableCollection<BO.OrderItem> listOfOrderItemForObservableCollection
        {
            get { return _listOfOrderItemForObservableCollection; }
            set
            {
                _listOfOrderItemForObservableCollection = value;
                OnPropertyChanged(nameof(listOfOrderItemForObservableCollection));
            }
        }

        private ObservableCollection<string> _TotalPriceForObservableCollection;
        public ObservableCollection<string> TotalPriceForObservableCollection
        {
            get { return _TotalPriceForObservableCollection; }
            set
            {
                _TotalPriceForObservableCollection = value;
                OnPropertyChanged(nameof(TotalPriceForObservableCollection));
            }
        }



        BlApi.IBl? bl = BlApi.Factory.Get();

        BO.Cart dataCart = new BO.Cart();

        NewOrder dataNewOrder { get; set; }

        public bool hasBeenSorted = true;

        IEnumerable<BO.ProductForList> ListOfproductForList;

        public Cart(BO.Cart cart, NewOrder newOrder)
        {
            IsEnable = true;
            listOfOrderItemForObservableCollection = new ObservableCollection<BO.OrderItem>(cart.listOfOrderItem);
            TotalPriceForObservableCollection = new ObservableCollection<string> { (from math in cart.listOfOrderItem 
                                                                                    select math.TotalPrice).Sum().ToString()};
            cartDetails = cart;

            dataCart = cart;

            dataNewOrder = newOrder;

            if (newOrder.dataIsRegistered)
            {
                IsEnable = false;
                cartDetails.CustomerName = newOrder.user.UserName.ToString();
                cartDetails.CustomerEmail = newOrder.user.Email.ToString();
                cartDetails.CustomerAddress = newOrder.user.Address.ToString();

                dataCart.CustomerName = cartDetails.CustomerName;
                dataCart.CustomerEmail = cartDetails.CustomerEmail;
                dataCart.CustomerAddress = cartDetails.CustomerAddress;
            }
            InitializeComponent();
            //ListCart.ItemsSource = cart.listOfOrderItem;
            //TotalPrice.Text = cart.TotalPrice.ToString();
            
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirma_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cartDetails.CustomerName) || string.IsNullOrEmpty(cartDetails.CustomerEmail) || string.IsNullOrEmpty(cartDetails.CustomerAddress))
                {
                    MessageBox.Show("ERROR");
                    return;
                }

                if (!CustomerEmail.Text.Contains("@"))
                {
                    MessageBox.Show("Incorrect email");
                    return;
                }

                if (!dataNewOrder.dataIsRegistered)//אם המשתמש לא רשום
                {
                    dataCart.CustomerName = cartDetails.CustomerName;
                    dataCart.CustomerEmail = cartDetails.CustomerEmail;
                    dataCart.CustomerAddress = cartDetails.CustomerAddress;
                }



                int orderID = bl.Cart.OrderConfirmation(dataCart);


                if (dataNewOrder.dataIsRegistered)//אם המשתמש רשום
                {
                    dataNewOrder.user.listOfOrder.Add( bl.Order.OrderForList(orderID));
                    dataNewOrder.user.currentCart = new BO.Cart();
                    bl.User.UpdateUser(dataNewOrder.user);
                }

                MessageBox.Show($"Thank you for shopping with us" +
                                $" Your order number is:" + orderID); 
                new MainWindow().Show();

                if (dataNewOrder.dataparent != null)
                {
                    dataNewOrder.dataparent.Close();
                }
                dataNewOrder.Close();
                this.Close();
               
            }
            catch (BO.DataNotFoundException)
            {               
                MessageBox.Show($"There are missing products" +
                                $"\n" + bl.Cart.MissingProducts(dataCart));
            }           
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem productItem = new BO.ProductItem();
            BO.OrderItem orderItem = new BO.OrderItem();

            orderItem = (BO.OrderItem)(sender as ListView).SelectedItem;
            if (orderItem != null)
            {
                productItem = bl.Product.ProductDetails(orderItem.ProductID, dataCart);

                CartAndProduct.Product productWindow = new CartAndProduct.Product(
                    productItem, dataCart, dataNewOrder, true
                    );

                this.Close();
                productWindow.ShowDialog();
            }
        }

        private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
            if (gridViewColumnHeader != null)
            {
                string name = (gridViewColumnHeader.Tag as string);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView((sender as ListView).ItemsSource);
                view.SortDescriptions.Clear();
                if (hasBeenSorted)
                {
                    view.SortDescriptions.Add(new SortDescription(name, ListSortDirection.Descending));
                    hasBeenSorted = false;
                }
                else
                {
                    view.SortDescriptions.Add(new SortDescription(name, ListSortDirection.Ascending));
                    hasBeenSorted = true;
                }
            }
        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = (int)button.Tag;

            // Find the product in the `productItemForObservableCollection` list
            var product = dataNewOrder.productItemForObservableCollection.FirstOrDefault(item => item.ID == productId);
            if (product != null)
            {
                // Decrement the product's amount
                product.Amount--;

                // If the product's amount is now zero, remove it from the `listOfOrderItemForObservableCollection` list
                if (product.Amount == 0)
                {
                    this.listOfOrderItemForObservableCollection.Remove(
                        this.listOfOrderItemForObservableCollection.FirstOrDefault(x => x.ProductID == product.ID)
                        );

                    //update the product's quantity in the `dataCart` object
                    bl.Cart.UpdateProductQuantity(dataCart, productId, (int)product.Amount);
                }
                else
                {
                    // Otherwise, update the product's quantity in the `dataCart` object
                    bl.Cart.UpdateProductQuantity(dataCart, productId, (int)product.Amount);
                }

                // Update the total price
                this.TotalPriceForObservableCollection[0] = dataCart.TotalPrice.ToString();

                // Update the observables
                this.dataNewOrder.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(dataNewOrder.productItemForObservableCollection);
                this.listOfOrderItemForObservableCollection = new ObservableCollection<BO.OrderItem>(this.listOfOrderItemForObservableCollection);
                this.TotalPriceForObservableCollection = new ObservableCollection<string>(this.TotalPriceForObservableCollection);
            }
        }

        private void Increase_Click(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;
            int productId = (int)button.Tag;

            bl.Cart.AddProduct(dataCart, productId);

            // Find the product in the collection and update its amount
            var product = dataNewOrder.productItemForObservableCollection.FirstOrDefault(p => p.ID == productId);
            if (product != null)
            {
                product.Amount++;
            }

            //עידכון המחיר הכולל
            this.TotalPriceForObservableCollection[0] = dataCart.TotalPrice.ToString();

            // Update the OrderForObservableCollection in the parent window
            this.Dispatcher.Invoke(() =>
            {
                dataNewOrder.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(dataNewOrder.productItemForObservableCollection);
                this.listOfOrderItemForObservableCollection = new ObservableCollection<BO.OrderItem>(this.listOfOrderItemForObservableCollection);
                this.TotalPriceForObservableCollection = new ObservableCollection<string>(this.TotalPriceForObservableCollection);
            });         
        }
    }
}
