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
using System.Windows.Shapes;

namespace PL.AdminWindows
{
    /// <summary>
    /// Interaction logic for MainAdminWindow.xaml
    /// </summary>
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
            InitializeComponent();
        }

        private void ProductListWindow_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindows.ProductListWindow().Show();
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void OrderListWindow_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindows.Order.OrderListWindow().Show();
            this.Close();
        }
    }
}
