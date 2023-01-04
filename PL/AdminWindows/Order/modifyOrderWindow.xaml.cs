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

namespace PL.AdminWindows.Order
{
    /// <summary>
    /// Interaction logic for modifyOrderWindow.xaml
    /// </summary>
    public partial class modifyOrderWindow : Window ,INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.Order> _orderObservableCollection;
        public ObservableCollection<BO.Order> OrderObservableCollection
        {
            get { return _orderObservableCollection; }
            set
            {
                _orderObservableCollection = value;
                OnPropertyChanged(nameof(OrderObservableCollection));
            }
        }

        private ObservableCollection<BO.OrderItem> _orderItemObservableCollection;
        public ObservableCollection<BO.OrderItem> OrderItemObservableCollection
        {
            get { return _orderItemObservableCollection; }
            set
            {
                _orderItemObservableCollection = value;
                OnPropertyChanged(nameof(OrderItemObservableCollection));
            }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();

        private OrderListWindow parentWindow;

        public modifyOrderWindow(BO.OrderForList orderForList, OrderListWindow parentWindow)
        {
            this.parentWindow = parentWindow;
            var order = bl.Order.OrderDetailsRequest(orderForList.ID);
            OrderObservableCollection = new ObservableCollection<BO.Order> { order };
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(order.Items);
            InitializeComponent();
        }

        private void OrderItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShipUpdate_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.OrderShippingUpdate(int.Parse(IDTextBox.Text));
            var updatedOrder = bl.Order.OrderDetailsRequest(OrderObservableCollection[0].ID);
            // Update the OrderObservableCollection and OrderItemObservableCollection with the updated order details
            OrderObservableCollection[0] = updatedOrder;
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(updatedOrder.Items);

            parentWindow.Dispatcher.Invoke(() =>
            {
                var orderList = bl.Order.OrderListRequest();
                parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
            });
        }

        private void DeliveryUpdate_Click(object sender, RoutedEventArgs e)
        {
            bl.Order.UpdateDeliveryOrder(int.Parse(IDTextBox.Text));
            var updatedOrder = bl.Order.OrderDetailsRequest(OrderObservableCollection[0].ID);
            // Update the OrderObservableCollection and OrderItemObservableCollection with the updated order details
            OrderObservableCollection[0] = updatedOrder;
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(updatedOrder.Items);

            parentWindow.Dispatcher.Invoke(() =>
            {
                var orderList = bl.Order.OrderListRequest();
                parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
            });
        }

        private void OrderItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
