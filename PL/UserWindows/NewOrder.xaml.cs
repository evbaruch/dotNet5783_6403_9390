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
        private ObservableCollection<String> _Categories;
        public ObservableCollection<String> Categories
        {
            get { return _Categories; }
            set
            {
                _Categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }


        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Cart cart = new Cart();
        public bool hasBeenSorted = true;
        private ObservableCollection<BO.ProductItem> _DataProductItemForObservableCollection;

        public NewOrder()
        {
            Categories = new ObservableCollection<string>(Enum.GetNames(typeof(BO.Enums.productsCategory)).Prepend("All"));

            IEnumerable<BO.ProductItem>  productItemList = bl.Product.ProductItemList();
            productItemForObservableCollection = new ObservableCollection<BO.ProductItem>(productItemList);
            _DataProductItemForObservableCollection = productItemForObservableCollection;
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

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {


            Button button = sender as Button;
            int productId = (int)button.Tag;

            // Find the product in the OrderForObservableCollection
            ProductItem productToUpdate = productItemForObservableCollection.FirstOrDefault(item => item.ID == productId);

            if (productToUpdate.Amount == 0)
            {
                return;
            }

            // Decrement the product's quantity and update the cart
            if (productToUpdate != null)
            {
                productToUpdate.Amount--;
                bl.Cart.UpdateProductQuantity(cart, productId, (int)productToUpdate.Amount);
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

            // Find the product in the collection and update its amount
            var product = productItemForObservableCollection.FirstOrDefault(p => p.ID == productId);

            if (product.InStock == false)
            {
                return;
            }

            bl.Cart.AddProduct(cart, productId);


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

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesSelector.SelectedItem.ToString() == "All")
            {
                productItemForObservableCollection = _DataProductItemForObservableCollection;
            }
            else
            {
                //a => a?.Category.ToString() == CategoriesSelector.SelectedItem.ToString()
                string selectedCategory = CategoriesSelector.SelectedItem.ToString();
                var filteredProducts = _DataProductItemForObservableCollection.Where(x => x.Category.ToString() == selectedCategory);
                productItemForObservableCollection = new ObservableCollection<ProductItem>(filteredProducts);

            }
        }
    }
}
