﻿using PL.AdminWindows;
using System;
using System.Collections.Generic;
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
       DependencyProperty.Register("Insert", typeof(BO.User), typeof(modifyProductWindow));

        public BO.User Insert
        {
            get { return (BO.User)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        BlApi.IBl? bl = BlApi.Factory.Get();

        public SignIN()
        {
            Insert = new BO.User();
            InitializeComponent();
        }

        private void sign_in(object sender, RoutedEventArgs e)
        {

            try
            {
                if ((Insert.UserName == "") || (Insert.Address == "") || (Insert.Email == "") || (Insert.Password == "")) // making sure the text box isn't empty 
                {
                    MessageBox.Show("you missed some details", "Missing details error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                else
                {
                    bl.User.SighIn(new() { UserName=Insert.UserName, Address = Insert.Address, Email = Insert.Email, Password = Insert.Password, listOfOrder = new List<BO.Order>() , currentCart = new BO.Cart(),IsAdmin = false });
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