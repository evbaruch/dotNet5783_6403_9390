﻿using System;
using System.Collections.Generic;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class Simulator : Window
    {
        public Simulator()
        {
            InitializeComponent();
            
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBox.Show("This is not the way", "Let me down slowly", MessageBoxButton.OK, MessageBoxImage.Question, MessageBoxResult.OK);
            e.Cancel = true;
        }

        private void WindowSoftClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure?", "Just making sure", MessageBoxButton.OKCancel, MessageBoxImage.Question,MessageBoxResult.Cancel) == MessageBoxResult.OK)
            {
                this.Closing -= Window_Closing;
                this.Closing += WindowSoftClosing;
                this.Close();
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
