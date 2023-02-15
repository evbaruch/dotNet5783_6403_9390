using BO;
using PL.AdminWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
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

namespace PL.UserWindows
{
    /// <summary>
    /// Interaction logic for SignIN.xaml
    /// </summary>
    public partial class SignIN : Window
    {
        public static readonly DependencyProperty MyPropertyProperty =
       DependencyProperty.Register("Insert", typeof(BO.User), typeof(SignIN));

        public BO.User Insert
        {
            get { return (BO.User)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();

        public bool toAdmin { get; set; }

        UserListWindow parentWindow;

        public SignIN(bool toAdmin, UserListWindow parant = null)
        {
            parentWindow = parant;
            Insert = new BO.User();
            InitializeComponent();
            this.toAdmin=toAdmin;
        }

        private void sign_in(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((Insert.UserName == "") || (Insert.Address == "") || (Insert.Email == "") || (Insert.Password == "")) // making sure the text box isn't empty 
                {
                    MessageBox.Show("you missed some details", "Missing details error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                else if(!bl.User.IsUserNameUnique(Insert.UserName))
                {
                    MessageBox.Show("The user name you entered is already in use", "details error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                else if(!Insert.Email.Contains("@"))
                {
                    MessageBox.Show("use a legal email address", "Missing details error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                else
                {
                    bl.User.SighIn(new() { UserName=Insert.UserName, Address = Insert.Address, Email = Insert.Email, Password = Insert.Password, listOfOrder = new List<BO.OrderForList>() , currentCart = new List<OrderItem>(),IsAdmin = toAdmin });

                    if (parentWindow != null)
                    {
                        parentWindow.Dispatcher.Invoke(() =>
                        {
                            parentWindow.UserForObservableCollection = new ObservableCollection<BO.UserForList>(bl.User.Users());
                        });
                    }
                    this.Close();
                }
            }
            catch (BO.DataNotFoundException)
            {
                MessageBox.Show("The data you have enter is not found, please try again", "Not found details error", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            }
            catch (BO.IncorrectDataException)
            {
                MessageBox.Show("The details you entered are not correct", "Uncorrect details error", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            }
            catch (Exception)
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
            }

        }
    }
}
