using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class OrderListWindow : Window, INotifyPropertyChanged
    {
        static ManualResetEvent pauseEvent = new ManualResetEvent(false);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.OrderForList> _orderForObservableCollection;
        public ObservableCollection<BO.OrderForList> OrderForObservableCollection
        {
            get { return _orderForObservableCollection; }
            set
            {
                _orderForObservableCollection = value;
                OnPropertyChanged(nameof(OrderForObservableCollection));
            }
        }

        public string name { get; set; }

        public bool hasBeenSorted = true;

        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderListWindow(string Name)
        {
            
            name = Name;
            var orderList = bl.Order.OrderListRequest();
            OrderForObservableCollection = new ObservableCollection<BO.OrderForList>(orderList);
            //OrderListview.ItemsSource = bl.Order.OrderListRequest();
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainAdminWindow(name).Show();
            this.Close();
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((BO.OrderForList)((ListView)sender).SelectedItem) != null)
            {
                new AdminWindows.Order.modifyOrderWindow(((BO.OrderForList)((ListView)sender).SelectedItem), this).Show();
            }
        }

        private void OrderListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
            if (gridViewColumnHeader != null)
            {
                string name = (gridViewColumnHeader.Tag as string);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(OrderForObservableCollection);//
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

