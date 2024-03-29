﻿using System;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace PL.AdminWindows.Order
{
    /// <summary>
    /// Interaction logic for modifyOrderWindow.xaml
    /// </summary>
    public partial class modifyOrderWindow : Window ,INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Color _selectedColor;
        public Color SelectedColor
        {
            get => _selectedColor;
            set
            {
                _selectedColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedColor"));

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public BO.Order order
        {
            get { return (BO.Order)GetValue(orderProperty); }
            set { SetValue(orderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderProperty =
            DependencyProperty.Register("order", typeof(BO.Order), typeof(modifyOrderWindow));

        private ObservableCollection<BO.Order> _orderObservableCollection;
        public ObservableCollection<BO.Order> orderObservableCollection
        {
            get { return _orderObservableCollection; }
            set
            {
                _orderObservableCollection = value;
                OnPropertyChanged(nameof(orderObservableCollection));
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

        private ObservableCollection<string> _StatusControlersObservableCollection;
        public ObservableCollection<string> StatusControlersObservableCollection
        {
            get { return _StatusControlersObservableCollection; }
            set
            {
                _StatusControlersObservableCollection = value;
                OnPropertyChanged(nameof(StatusControlersObservableCollection));
            }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();

        public bool hasBeenSorted = true;

        private OrderListWindow parentWindow;
        private object stepParentWindow;

        public modifyOrderWindow(BO.OrderForList orderForList, OrderListWindow parentWindow)
        {
            SelectedColor = Color.FromRgb(130, 231, 239);
            this.parentWindow = parentWindow;
            order = bl.Order.OrderDetailsRequest(orderForList.ID);
            orderObservableCollection = new ObservableCollection<BO.Order> { order };
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(order.Items);
            StatusControlersObservableCollection = new ObservableCollection<string>(new() { "true", "false", "Visible" });
            InitializeComponent();

        }

        public modifyOrderWindow(BO.OrderForList orderForList, object parentWindow)
        {
            SelectedColor = Color.FromRgb(130, 231, 239);
            this.stepParentWindow = parentWindow;
            order = bl.Order.OrderDetailsRequest(orderForList.ID);
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(order.Items);
            StatusControlersObservableCollection = new ObservableCollection<string>(new() { "false"  , "true" , "Hidden" });
            InitializeComponent();
        }

        private void OrderItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ShipUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.OrderShippingUpdate((int)(sender as Button).Tag);
                var updatedOrder = bl.Order.OrderDetailsRequest(order.ID);
                // Update the OrderObservableCollection and OrderItemObservableCollection with the updated order details
                order = updatedOrder;
                orderObservableCollection = new ObservableCollection<BO.Order> { order };
                OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(updatedOrder.Items);

                parentWindow.Dispatcher.Invoke(() =>
                {
                    var orderList = bl.Order.OrderListRequest();
                    parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
                });
            }
            catch (BO.DataNotFoundException)
            {
                MessageBox.Show("the order isn't exist or already been shipped", "Not found details error", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (BO.IncorrectDataException)
            {
                MessageBox.Show("The details you entered are not correct", "Uncorrect details error", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            }
            catch (Exception)
            {
                MessageBox.Show("The data you have enter is not found, please try again", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);

            }
        }

        private void DeliveryUpdate_Click(object sender, RoutedEventArgs e)
        {
            try { 
            bl.Order.UpdateDeliveryOrder((int)(sender as Button).Tag);
            order = bl.Order.OrderDetailsRequest(order.ID);
            orderObservableCollection = new ObservableCollection<BO.Order> { order };

                parentWindow.Dispatcher.Invoke(() =>
            {
                var orderList = bl.Order.OrderListRequest();
                parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
            });
        }
        catch (BO.DataNotFoundException)
            {
                MessageBox.Show("the order isn't exist or already been delivered", "Not found details error", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (BO.IncorrectDataException)
            {
                MessageBox.Show("The details you entered are not correct", "Uncorrect details error", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            }
            catch (Exception)
            {
            MessageBox.Show("The data you have enter is not found, please try again", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
            }
        }

        private void OrderItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                var a = (BO.OrderItem)button.DataContext;
                bl.Order.OrderUpdate(a.ID, (int)button!.Tag, -1);
                order = bl.Order.OrderDetailsRequest(order.ID);
                OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(order.Items);
                orderObservableCollection = new ObservableCollection<BO.Order> { order };

                parentWindow.Dispatcher.Invoke(() =>
                {
                    var orderList = bl.Order.OrderListRequest();
                    parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
                });
            }
            catch (BO.DataNotFoundException)
            {

                parentWindow.Dispatcher.Invoke(() =>
                {
                    var orderList = bl.Order.OrderListRequest();
                    parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
                });

                this.Close();

                MessageBox.Show("the order been deleted", "delete Order", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (BO.IncorrectDataException)
            {
                MessageBox.Show("The details you entered are not correct", "Uncorrect details error", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            }
            catch (Exception)
            {
                MessageBox.Show("The data you have enter is not found, please try again", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);

            }

        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var temp = (BO.OrderItem)button.DataContext;
            bl.Order.OrderUpdate(temp.ID, (int)button!.Tag, 1);
            var updatedOrder = bl.Order.OrderDetailsRequest(order.ID);
            order = updatedOrder;
            OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>(updatedOrder.Items);

            parentWindow.Dispatcher.Invoke(() =>
            {
                var orderList = bl.Order.OrderListRequest();
                parentWindow.OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
            });
        }

        private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
            if (gridViewColumnHeader != null)
            {
                string name = (gridViewColumnHeader.Tag as string);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderItemObservableCollection);
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
