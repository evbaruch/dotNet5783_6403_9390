using MaterialDesignThemes.Wpf;
using PL.AdminWindows;
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
            new MainAdminWindow().Show();
            this.Close();
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
            
            InputPopUP inputPopUP = new InputPopUP();
            inputPopUP.Show();
            if (inputPopUP.OrderID != null && inputPopUP.OrderID != "" )
            {
                new PL.OrderTracking.OrderTracker(int.Parse(inputPopUP.OrderID)).Show();
                this.Close();
            }

        }
    }
}
