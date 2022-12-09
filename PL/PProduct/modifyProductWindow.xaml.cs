using BlApi;
using BlImplementation;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.PProduct
{
    /// <summary>
    /// Interaction logic for addProductWindow.xaml
    /// </summary>
    public partial class modifyProductWindow : Window
    {
        public modifyProductWindow()
        {
            IBl bl = new Bl();
            InitializeComponent();

            updateProduct.IsEnabled = false;
            updateProduct.Visibility = Visibility.Hidden;

            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory));
        }

        public modifyProductWindow(int ID)
        {
            IBl bl = new Bl();
            InitializeComponent();

            var product = bl.Product.ProductDetails(ID);
            IDTextBox.Text = product.ID.ToString();
            Name.Text =  product.Name;
            CategoriesSelector.Text = product.Category.ToString();
            Price.Text = product.Price.ToString();
            inStock.Text = product.InStock.ToString();

            AddProduct.IsEnabled = false;
            AddProduct.Visibility = Visibility.Hidden;

            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory));
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            IBl bl = new Bl();
            BO.Enums.productsCategory.TryParse(CategoriesSelector.Text, out BO.Enums.productsCategory Categor);
            bl.Product.AddProduct(new() {ID= int.Parse(IDTextBox.Text), Name = Name.Text, Price = int.Parse(Price.Text), Category = Categor , InStock = int.Parse(inStock.Text)});
            new ProductListWindow().Show();
            this.Close();
        }

        private void updateProduct_Click(object sender, RoutedEventArgs e)
        {
            IBl bl = new Bl();
            BO.Enums.productsCategory.TryParse(CategoriesSelector.Text, out BO.Enums.productsCategory Categor);
            bl.Product.UpdateProduct(new() { ID= int.Parse(IDTextBox.Text), Name = Name.Text, Price = int.Parse(Price.Text), Category = Categor, InStock = int.Parse(inStock.Text) });
            new ProductListWindow().Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
            this.Close();
        }
    }
}
