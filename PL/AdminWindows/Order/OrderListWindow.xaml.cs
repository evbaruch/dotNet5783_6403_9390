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

namespace PL.AdminWindows.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        public OrderListWindow()
        {
            InitializeComponent();
            BlApi.IBl? bl = BlApi.Factory.Get();
            OrderListview.ItemsSource = bl.Order.OrderListRequest();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainAdminWindow().Show();
            this.Close();
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new AdminWindows.Order.modifyOrderWindow((BO.OrderForList)(OrderListview.SelectedItem)).Show();
        }

        private void OrderListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
