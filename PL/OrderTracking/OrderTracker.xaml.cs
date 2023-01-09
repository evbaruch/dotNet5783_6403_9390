using BO;
using MaterialDesignThemes.Wpf;
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


namespace PL.OrderTracking
{
    /// <summary>
    /// Interaction logic for OrderTracker.xaml
    /// </summary>
    public partial class OrderTracker : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.OrderTracking> _TrackedObservableCollection;
        public ObservableCollection<BO.OrderTracking> TrackedObservableCollection
        {
            get { return _TrackedObservableCollection; }
            set
            {
                _TrackedObservableCollection = value;
                OnPropertyChanged(nameof(TrackedObservableCollection));
            }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();

        public OrderTracker(int ID)
        {

            var Tracked = bl.Order.OrderTracking(ID);
            TrackedObservableCollection = new ObservableCollection<BO.OrderTracking> { Tracked };
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void OredrDetails_Click(object sender, RoutedEventArgs e)
        {
            new PL.AdminWindows.Order.modifyOrderWindow(bl.Order.OrderListRequest().First(x => x.ID == int.Parse(IDValueTextBlock.Text)), this).Show(); // yhedoa is a ...
        }
    }
}
