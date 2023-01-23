using BO;
using PL.UserWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Threading;

namespace PL;

/// <summary>
/// Interaction logic for UserMainWindow.xaml
/// </summary>
public partial class UserMainWindow : Window, INotifyPropertyChanged
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

    DispatcherTimer timer = new DispatcherTimer();

    public string UserName { get; set; }

    public BO.User User { get; set; }

    BlApi.IBl? bl = BlApi.Factory.Get();

    public UserMainWindow(string Name)
    {
        UserName = Name;
        User = bl.User.UserDetails(Name);
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


    private void NewOrderWindow_Click(object sender, RoutedEventArgs e)
    {
        UserWindows.NewOrder newOrder = new UserWindows.NewOrder(true,this);
        this.Close();
        newOrder.ShowDialog();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }

    private void ProfileWindow_Click(object sender, RoutedEventArgs e)
    {
        new UserWindows.UserProfile(UserName).Show();
        
    }
}
