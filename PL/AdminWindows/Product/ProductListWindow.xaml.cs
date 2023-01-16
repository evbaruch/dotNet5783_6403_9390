using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace PL.AdminWindows
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.ProductForList> _ProductForObservableCollection;
        public ObservableCollection<BO.ProductForList> ProductForObservableCollection
        {
            get { return _ProductForObservableCollection; }
            set
            {
                _ProductForObservableCollection = value;
                OnPropertyChanged(nameof(ProductForObservableCollection));
            }
        }

        private ObservableCollection<String> _Categories;
        public ObservableCollection<String> Categories
        {
            get { return _Categories;}
            set
            {
                _Categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public bool hasBeenSorted = true;

        BlApi.IBl? bl = BlApi.Factory.Get();

        private ObservableCollection<ProductForList> AllProduct;

        public ProductListWindow()
        {
            var productForLists = bl.Product.Products();
            ProductForObservableCollection = new ObservableCollection<BO.ProductForList>(productForLists);
            Categories = new ObservableCollection<string>(Enum.GetNames(typeof(BO.Enums.productsCategory)).Prepend("All"));
            AllProduct = ProductForObservableCollection;
            //CategoriesSelector.Items.Add("All");
            //for (int i = 0; i<5; i++)
            //{
            //    CategoriesSelector.Items.Add($"{(BO.Enums.productsCategory)i}");
            //}

            InitializeComponent(); 
            //CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory)); 
            // insted of locking the Item Source i prefer do it like that
            

        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BlApi.IBl? bl = BlApi.Factory.Get();

            var categories = sender as ComboBox;
            if (categories.SelectedItem.ToString() == "All")
            {
                ProductForObservableCollection =  AllProduct;
            }
            else
            {
                //a => a?.Category.ToString() == CategoriesSelector.SelectedItem.ToString()
                string selectedCategory = categories.SelectedItem.ToString();
                var filteredProducts = AllProduct.Where(x => x.Category.ToString() == selectedCategory);
                ProductForObservableCollection = new ObservableCollection<ProductForList>(filteredProducts);

            }

            //var categoriesSelector = sender as ComboBox;
            //if (categoriesSelector.SelectedItem.ToString() == "All")
            //{
            //     ProductListView.ItemsSource = bl.Product.Products();
            //}
            //else
            //{
            //    ProductListView.ItemsSource = bl.Product.Products(a => a?.Category.ToString() == categoriesSelector.SelectedItem.ToString()); // the way we get only the specific category
            //}
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            new modifyProductWindow().Show();
            this.Close(); // closing the window after moving to another window
        }

        private void updateProduct_Click(object sender, MouseButtonEventArgs e)
        {
            if ((BO.ProductForList)((sender as ListView).SelectedItem) != null)
            {
                var product = (BO.ProductForList)((sender as ListView).SelectedItem);
                new modifyProductWindow(product).Show();
                this.Close();
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainAdminWindow().Show();
            this.Close();
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        } 

        private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
            if (gridViewColumnHeader != null)
            {
                string name = (gridViewColumnHeader.Tag as string);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
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
