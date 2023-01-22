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

namespace PL.UserWindows;

/// <summary>
/// Interaction logic for UserProfile.xaml
/// </summary>
public partial class UserProfile : Window, INotifyPropertyChanged
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

    public BO.User User
    {
        get { return (BO.User)GetValue(MyUserProperty); }
        set { SetValue(MyUserProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyUserProperty =
        DependencyProperty.Register("MyProperty", typeof(BO.User), typeof(UserProfile));


    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

    public bool hasBeenSorted = true;

    public UserProfile(string userName)
    {
        User = bl.User.UserDetails(userName);
        SelectedColor = Color.FromRgb(130, 231, 239);
        OrderItemObservableCollection = new ObservableCollection<BO.OrderItem>();
        InitializeComponent();
    }

    private void OrderListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {

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
