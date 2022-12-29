﻿using BO;
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
using System.Windows.Shapes;

namespace PL.UserWindows
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public NewOrder()
        {
            InitializeComponent();
            ProductItemForList.ItemsSource = bl.Product.ProductItemList();
        }

        private void CartWindow(object sender, RoutedEventArgs e)
        {
            CartAndProduct.Cart cartWindow = new CartAndProduct.Cart();
            cartWindow.ShowDialog();
        }

        private void showProduct_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            CartAndProduct.Product productWindow = new CartAndProduct.Product();
            productWindow.productItem = (BO.ProductItem)ProductItemForList.SelectedItem;
            productWindow.ShowDialog();
        }
    }
}
