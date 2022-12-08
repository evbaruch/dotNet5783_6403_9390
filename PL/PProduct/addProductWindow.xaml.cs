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
    public partial class addProductWindow : Window
    {
        public addProductWindow()
        {
            IBl bl = new Bl();
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            IBl bl = new Bl();
            BO.Enums.productsCategory.TryParse(Categoty.Text,out BO.Enums.productsCategory Categor);
            bl.Product.AddProduct(new() {ID= int.Parse(ID.Text), Name = Name.Text, Price = int.Parse(Price.Text), Category = Categor , InStock = int.Parse(inStock.Text)});
        }
    }
}
