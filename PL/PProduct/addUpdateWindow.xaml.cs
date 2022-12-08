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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.PProduct
{
    /// <summary>
    /// Interaction logic for addUpdateWindow.xaml
    /// </summary>
    public partial class addUpdateWindow : Window
    {
        public addUpdateWindow()
        {
            InitializeComponent();
        }

        public addUpdateWindow(int ID)
        {
            IBl bl = new Bl();
            var product = bl.Product.ProductDetails(ID);
            InitializeComponent();
            BO.Enums.productsCategory.TryParse(Categoty.Text, out BO.Enums.productsCategory Categor);
            bl.Product.UpdateProduct(new() { ID = product.ID, Name = product.Name, Price = product.Price, Category = product.Category, InStock = product.InStock });
        }

        private void updateProduct_Click(object sender, RoutedEventArgs e)
        {
            IBl bl = new Bl();
        }
    }
}
