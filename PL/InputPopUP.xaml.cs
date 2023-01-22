﻿using System;
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
        private static string _FirstInput;
        private static string _SeconedInput;

        public string Label1Text { get; set; }
        public string Label2Text { get; set; }
        public string ButtenText { get; set; }

        public InputPopUP(string first , string seconed ,string third)
        {
            Label1Text = first;
            Label2Text = seconed;
            ButtenText = third;
            InitializeComponent();
            //this.DataContext = this;
        }

        public InputPopUP()
        {
            InitializeComponent();
            //this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _FirstInput = inputTextBox1.Text;
            _SeconedInput  = inputTextBox2.Text;

            var window = Window.GetWindow(this);
            window.Close();
        }

        public string FirstInput
        {
            get
            {
                return _FirstInput;
            }
            set { _FirstInput = value; }
        }

        public string SeconedInput
        {
            get
            {
                return _SeconedInput;
            }
            set { _SeconedInput = value; }
        }


        public void Show(string first, string seconed,string third)
        {
            // Create a window to host the message box
            var messageBoxWindow = new Window();
            messageBoxWindow.Width = 300;
            messageBoxWindow.Height = 190;
            messageBoxWindow.Title = "Enter values";
            messageBoxWindow.ResizeMode = ResizeMode.NoResize;

            // Create an instance of the message box control
            var messageBoxControl = new InputPopUP(first, seconed,third);

            // Set the window's content to the message box control
            messageBoxWindow.Content = messageBoxControl;

            // Show the window and wait for it to close
            messageBoxWindow.ShowDialog();
        }
    }
}
