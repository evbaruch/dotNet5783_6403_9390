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
            CartAndProduct.Product productWindow = new CartAndProduct.Product(
                (BO.ProductItem)ProductItemForList.SelectedItem,cart, this ,false
                );           
            productWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
