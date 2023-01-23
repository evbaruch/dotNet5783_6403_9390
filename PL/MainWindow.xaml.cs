using MaterialDesignThemes.Wpf;
using PL.AdminWindows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminMainWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                InputPopUP inputPopUP = new InputPopUP();
                inputPopUP.FirstInput = null;
                inputPopUP.SeconedInput = null;
                inputPopUP.Show("User name:", "Pin:", "log in");
                if (inputPopUP.SeconedInput != null && inputPopUP.SeconedInput != "" && inputPopUP.FirstInput != null && inputPopUP.FirstInput != "")
                {
                    if (inputPopUP.SeconedInput == bl.User.UserDetails(inputPopUP.FirstInput).Password.ToString() && bl.User.UserDetails(inputPopUP.FirstInput).IsAdmin == true)
                    {

                        var MainAdminWindow = new MainAdminWindow(inputPopUP.FirstInput);
                        MainAdminWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                        MainAdminWindow.Left = this.Left;
                        MainAdminWindow.Top = this.Top;
                        MainAdminWindow.Show();
                        this.Close();

                        //new MainAdminWindow().Show();
                        //this.Close();
                    }
                    else 
                    {
                        MessageBox.Show("you are not a god ,you may apply a request to become a god", "peasent!!!!", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
                    }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserWindows.NewOrder(false).Show();
            this.Close();
        }

        private void observation_Click(object sender, RoutedEventArgs e)
        {

            //var inputDialog = new Window();
            //inputDialog.Width = 300;
            //inputDialog.Height = 190;
            //inputDialog.Title = "Enter values";

            //// Create a panel to hold the controls
            //var panel = new StackPanel();

            //var inputTextBox1 = new TextBox();

            //var inputTextBox2 = new TextBox();

            //inputTextBox1.Width = 200;
            //inputTextBox2.Width = 200;

            //// Create a label and textbox for the "user name" input
            //var userNameLabel = new Label();
            //userNameLabel.Content = "User name:";
            //userNameLabel.Target = inputTextBox1;

            //inputTextBox1.Margin = new Thickness(10);

            //StackPanel userNamePanel = new StackPanel();
            //userNamePanel.Orientation = Orientation.Horizontal;
            //userNamePanel.Children.Add(userNameLabel);
            //userNamePanel.Children.Add(inputTextBox1);
            //panel.Children.Add(userNamePanel);

            //// Create a label and textbox for the "order ID" input
            //var orderIdLabel = new Label();
            //orderIdLabel.Content = "Order ID:";
            //orderIdLabel.Target = inputTextBox2;

            //inputTextBox2.Margin = new Thickness(10);

            //StackPanel orderIdPanel = new StackPanel();
            //orderIdPanel.Orientation = Orientation.Horizontal;
            //orderIdPanel.Children.Add(orderIdLabel);
            //orderIdPanel.Children.Add(inputTextBox2);
            //panel.Children.Add(orderIdPanel);

            //userNameLabel.VerticalAlignment = VerticalAlignment.Center;
            //inputTextBox1.VerticalAlignment = VerticalAlignment.Center;

            //orderIdLabel.VerticalAlignment = VerticalAlignment.Center;
            //inputTextBox2.VerticalAlignment = VerticalAlignment.Center;

            //// Create the "Search" button and add it to the panel
            //var searchButton = new Button();
            //searchButton.Content = "Search";
            //searchButton.Margin = new Thickness(10);
            //searchButton.Click += (sender, e) => inputDialog.Close();
            //searchButton.HorizontalAlignment = HorizontalAlignment.Center;
            //panel.Children.Add(searchButton);

            //// Add the panel as the window's content
            //inputDialog.Content = panel;
            //inputDialog.ResizeMode = ResizeMode.NoResize;

            //// Show the input dialog and wait for it to close
            //inputDialog.ShowDialog();

            try
            {
                InputPopUP inputPopUP = new InputPopUP();
                inputPopUP.Show("User name:", "Order ID:", "search");
                if (inputPopUP.SeconedInput != null && inputPopUP.SeconedInput != "")// && inputPopUP.FirstInput != null && inputPopUP.FirstInput != "")
                {
                    new PL.OrderTracking.OrderTracker(int.Parse(inputPopUP.SeconedInput)).Show();
                    this.Close();
                }
            }
            catch (BO.DataNotFoundException)
            {
                MessageBox.Show("the order isn't exist", "Not found details error", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
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

        private void Sign_in(object sender, RoutedEventArgs e)
        {
            UserWindows.SignIN signIN = new UserWindows.SignIN(false);
            signIN.ShowDialog();
        }

        private void Simulator(object sender, RoutedEventArgs e)
        {
            new PL.SimulatorWindow().Show();
        }

        private void LogIN(object sender, RoutedEventArgs e)
        {
            InputPopUP inputPopUP = new InputPopUP();
            inputPopUP.FirstInput = null;
            inputPopUP.SeconedInput = null;
            inputPopUP.Show("User name:", "Pin:", "log in");
            if (inputPopUP.SeconedInput != null && inputPopUP.SeconedInput != "" && inputPopUP.FirstInput != null && inputPopUP.FirstInput != "")
            {
                if (inputPopUP.SeconedInput == bl.User.UserDetails(inputPopUP.FirstInput).Password.ToString() && bl.User.UserDetails(inputPopUP.FirstInput).IsAdmin == false)
                {
                    var UserMainWindow = new UserMainWindow(inputPopUP.FirstInput);
                    UserMainWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    UserMainWindow.Left = this.Left;
                    UserMainWindow.Top = this.Top;
                    UserMainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("His highness is a god ,gods have there ways", "wrong path", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedContent = (sender as ComboBox).SelectedIndex;
            switch (selectedContent)
            {
                case 0:
                    Sign_in(sender,sender as RoutedEventArgs);
                    break;
                case 1:
                    LogIN(sender,sender as RoutedEventArgs);
                    break;
                case 2:
                    Simulator(sender,sender as RoutedEventArgs);
                    break;
                default:
                    break;
            }
        }

        
    }
}
