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
    /// Interaction logic for modifyOrderWindow.xaml
    /// </summary>
    public partial class modifyOrderWindow : Window
    {
        public modifyOrderWindow(BO.OrderForList orderForList)
        {
            InitializeComponent();
            BlApi.IBl? bl = BlApi.Factory.Get();
            OrderItemListview.ItemsSource = bl.Order.OrderDetailsRequest(orderForList.ID).Items;
            DataContext = bl.Order.OrderDetailsRequest(orderForList.ID);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OrderItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void OrderItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
