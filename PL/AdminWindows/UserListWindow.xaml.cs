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

namespace PL.AdminWindows;

/// <summary>
/// Interaction logic for UserListWindow.xaml
/// </summary>
public partial class UserListWindow : Window, INotifyPropertyChanged
{
    static ManualResetEvent pauseEvent = new ManualResetEvent(false);

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private ObservableCollection<BO.UserForList> _UserForObservableCollection;
    public ObservableCollection<BO.UserForList> UserForObservableCollection
    {
        get { return _UserForObservableCollection; }
        set
        {
            _UserForObservableCollection = value;
            OnPropertyChanged(nameof(UserForObservableCollection));
        }
    }

    public string name { get; set; }

    public bool hasBeenSorted = true;

    BlApi.IBl? bl = BlApi.Factory.Get();

    public UserListWindow(String Name)
    {
        name = Name;
        UserForObservableCollection = new ObservableCollection<BO.UserForList>(bl.User.Users());
        InitializeComponent();
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        new MainAdminWindow(name).Show();
        this.Close();
    }

    private void UserListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (((BO.UserForList)((ListView)sender).SelectedItem) != null)
        {
            new UserWindows.UserProfile(((BO.UserForList)((ListView)sender).SelectedItem).UserName).Show();
        }
    }

    private void UserListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void GridViewColumnHeaderSort_Click(object sender, RoutedEventArgs e)
    {
        GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader);
        if (gridViewColumnHeader != null)
        {
            string name = (gridViewColumnHeader.Tag as string);
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(UserForObservableCollection);//
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

    private void CreateNewAdmin_Click(object sender, RoutedEventArgs e)
    {
        UserWindows.SignIN signIN = new UserWindows.SignIN(true , this);
        signIN.ShowDialog();
    }
}
