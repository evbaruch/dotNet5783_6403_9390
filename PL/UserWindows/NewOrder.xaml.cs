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
        //public static readonly DependencyProperty ImageUrlProperty = DependencyProperty.Register("ImageUrl", typeof(string), typeof(MyWindow), new PropertyMetadata(null));

        //public string ImageUrl
        //{
        //    get { return (string)GetValue(ImageUrlProperty); }
        //    set { SetValue(ImageUrlProperty, value); }
        //}

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

        private ObservableCollection<String> _ImageUrl;
        public ObservableCollection<String> ImageUrl
        {
            get { return _ImageUrl; }
            set
            {
                _ImageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        private void foo()
        {
            string[] item = new string[15];
            item[0] = @"..\background\products\angel.jpeg";
            item[1] = @"..\background\products\centaur.jpeg";



            ImageUrl = new ObservableCollection<string> (item);
        }


        private BlApi.IBl? bl = BlApi.Factory.Get();
        private BO.Cart? cart = new Cart();
        private bool hasBeenSorted = true;
        private ObservableCollection<BO.ProductItem> _DataProductItemForObservableCollection;
        internal UserMainWindow dataparent;
        internal BO.User user = new BO.User();
        internal bool dataIsRegistered = false;

        public NewOrder(bool isRegistered = false, UserMainWindow parent = null)
        {
            foo();
            dataIsRegistered = isRegistered;
            Categories = new ObservableCollection<string>(Enum.GetNames(typeof(BO.Enums.productsCategory)).Prepend("All"));
            IEnumerable<BO.ProductItem> productItemList;

            if (isRegistered && parent != null)//משתמש רשום
            {
                dataparent = parent;
                user = parent.User;
                cart = parent.User.currentCart;
                productItemList = bl.User.UserProductItems(cart);
            }
            else//משתמש לא רשום
            {
                productItemList = bl.Product.ProductItemList();
            }

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
            if (dataIsRegistered)//משתמש רשום
            {
                user.currentCart = cart;
                bl.User.UpdateUser(user);
                this.Close();
                dataparent.Show();

            }
            else//משתמש לא רשום
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
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

            if (dataIsRegistered)//נעדכן למשתמש
            {
                user.currentCart = cart;
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

            if (cart == null)
            {
                cart = new Cart();
            }

            bl.Cart.AddProduct(cart, productId);

            if (product != null)
            {
                product.Amount++;
            }

            if (dataIsRegistered)//נעדכן למשתמש
            {
                user.currentCart = cart;   
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
            var categories = sender as ComboBox;
            if (categories.SelectedItem.ToString() == "All")
            {
                productItemForObservableCollection = _DataProductItemForObservableCollection;
            }
            else
            {
                //a => a?.Category.ToString() == CategoriesSelector.SelectedItem.ToString()
                string selectedCategory = categories.SelectedItem.ToString();
                var filteredProducts = _DataProductItemForObservableCollection.Where(x => x.Category.ToString() == selectedCategory);
                productItemForObservableCollection = new ObservableCollection<ProductItem>(filteredProducts);

            }
        }
    }
}
