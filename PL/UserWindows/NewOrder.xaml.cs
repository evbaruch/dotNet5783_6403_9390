using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class NewOrder : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.ProductItem> _productItemForObservableCollection;
        public ObservableCollection<BO.ProductItem> productItemForObservableCollection
        {
            get { return _productItemForObservableCollection; }
            set
            {
                _productItemForObservableCollection = value;
                OnPropertyChanged(nameof(productItemForObservableCollection));
            }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new Cart();
        public bool hasBeenSorted = true;

        //IEnumerable<BO.ProductItem> productItemList = new List<BO.ProductItem>();
        public NewOrder()
        {
            IEnumerable<BO.ProductItem>  productItemList = bl.Product.ProductItemList();
            productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(productItemList);
            InitializeComponent();
            
        }

        private void CartWindow(object sender, RoutedEventArgs e)
        {
            CartAndProduct.Cart cartWindow = new CartAndProduct.Cart(cart, this);
            cartWindow.ShowDialog();

          
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            if ((BO.ProductItem)ProductItemForList.SelectedItem != null)
            {
                CartAndProduct.Product productWindow = new CartAndProduct.Product(
                    (BO.ProductItem)ProductItemForList.SelectedItem, cart, this, false
                    );
                productWindow.ShowDialog();
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }




        private void OrderItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = (int)button.Tag;

            // Update the OrderForObservableCollection in the parent window
            foreach (var item in productItemForObservableCollection)
            {
                if (item.ID == productId)
                {
                    item.Amount--;
                    bl.Cart.UpdateProductQuantity(cart, productId, (int)item.Amount);
                    break;
                }
            }

            this.Dispatcher.Invoke(() =>
            {
                this.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(productItemForObservableCollection);
            });          
        }



        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int productId = (int)button.Tag;

            bl.Cart.AddProduct(cart, productId);

            // Find the product in the collection and update its amount
            var product = productItemForObservableCollection.FirstOrDefault(p => p.ID == productId);
            if (product != null)
            {
                product.Amount++;
            }

            // Update the OrderForObservableCollection in the parent window
            this.Dispatcher.Invoke(() =>
            {
                this.productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(productItemForObservableCollection);
            });
        }

        private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
            if (gridViewColumnHeader != null)
            {
                string name = (gridViewColumnHeader.Tag as string);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductItemForList.ItemsSource);
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

    }
}
