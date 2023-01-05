using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for InputPopUPxaml.xaml
    /// </summary>
    public partial class InputPopUP : UserControl
    {
        private static string _UserName;
        private static string _OrderID;

        public InputPopUP()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _UserName = inputTextBox1.Text;
            _OrderID  = inputTextBox2.Text;

            var window = Window.GetWindow(this);
            window.Close();
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }
        }

        public string OrderID
        {
            get
            {
                return _OrderID;
            }
        }


        public void Show()
        {
            // Create a window to host the message box
            var messageBoxWindow = new Window();
            messageBoxWindow.Width = 300;
            messageBoxWindow.Height = 190;
            messageBoxWindow.Title = "Enter values";
            messageBoxWindow.ResizeMode = ResizeMode.NoResize;

            // Create an instance of the message box control
            var messageBoxControl = new InputPopUP();

            // Set the window's content to the message box control
            messageBoxWindow.Content = messageBoxControl;

            // Show the window and wait for it to close
            messageBoxWindow.ShowDialog();
        }
    }
}
