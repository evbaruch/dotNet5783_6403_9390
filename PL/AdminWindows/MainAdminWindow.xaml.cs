using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PL.AdminWindows
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window, INotifyPropertyChanged
    {
       // static ManualResetEvent pauseEvent = new ManualResetEvent(false);

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<string> _BackgroundsObservableCollection;
        public ObservableCollection<string> BackgrundsObservableCollection
        {
            get { return _BackgroundsObservableCollection; }
            set
            {
                _BackgroundsObservableCollection = value;
                OnPropertyChanged(nameof(BackgrundsObservableCollection));
            }
        }

        private ImageSource _currentBackgroundImage;
        public ImageSource CurrentBackgroundImage
        {
            get { return _currentBackgroundImage; }
            set
            {
                _currentBackgroundImage = value;
                OnPropertyChanged(nameof(CurrentBackgroundImage));
            }
        }



        public string AdminName
        {
            get { return (string)GetValue(AdminNameProperty); }
            set { SetValue(AdminNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AdminName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdminNameProperty =
            DependencyProperty.Register("AdminName", typeof(string), typeof(MainAdminWindow));



        DispatcherTimer timer = new DispatcherTimer();

        BlApi.IBl? bl = BlApi.Factory.Get();

        public MainAdminWindow(string AdminName)
        {
            this.AdminName =  AdminName;
            BackgrundsObservableCollection = new ObservableCollection<string>(Directory.GetFiles("..\\PL\\background\\main window\\"));
            CurrentBackgroundImage =new BitmapImage(new Uri(BackgrundsObservableCollection[0], UriKind.Relative));
            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += Timer_Tick;
            timer.Start();
            InitializeComponent();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int index = BackgrundsObservableCollection.IndexOf(CurrentBackgroundImage.ToString());
            index = ((index+1)%BackgrundsObservableCollection.Count);
            CurrentBackgroundImage = new BitmapImage(new Uri(BackgrundsObservableCollection[index], UriKind.Relative));
        }


        private void ProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindows.ProductListWindow(AdminName).Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void OrderListWindow_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindows.Order.OrderListWindow(AdminName).Show();
            this.Close();
        }
    }
}
