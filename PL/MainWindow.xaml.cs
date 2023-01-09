using MaterialDesignThemes.Wpf;
using PL.AdminWindows;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminMainWindow_Click(object sender, RoutedEventArgs e)
        {
            InputPopUP inputPopUP = new InputPopUP();
            inputPopUP.Show("User name:", "Pin:","log in");
            if (inputPopUP.SeconedInput == "12345")
            {
                
                var MainAdminWindow = new MainAdminWindow();
                MainAdminWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                MainAdminWindow.Left = this.Left;
                MainAdminWindow.Top = this.Top;
                MainAdminWindow.Show();
                this.Close();

                //new MainAdminWindow().Show();
                //this.Close();
            }
            else if(inputPopUP.SeconedInput == null)
            {

            }
            else 
            {
                MessageBox.Show("the User name or the pin is not correct", "Error", MessageBoxButton.OK, MessageBoxImage.Hand, MessageBoxResult.Cancel);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            UserWindows.NewOrder newOrder = new UserWindows.NewOrder();
            this.Close();
            newOrder.ShowDialog();

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
                if (inputPopUP.SeconedInput != null && inputPopUP.SeconedInput != "")
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
    }
}
