﻿using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
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

namespace PL.PProduct
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public bool hasSorted = true;
            
        public ProductListWindow()
        {
            InitializeComponent();
            BlApi.IBl? bl = BlApi.Factory.Get();
            ProductListview.ItemsSource = bl.Product.Products();
            //CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory));
            CategoriesSelector.Items.Add("All");
            for (int i = 0; i<5; i++)
            {
                CategoriesSelector.Items.Add($"{(BO.Enums.productsCategory)i}");
            }
            
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BlApi.IBl? bl = BlApi.Factory.Get();
            if (CategoriesSelector.SelectedItem.ToString() == "All")
            {
                ProductListview.ItemsSource = bl.Product.Products();
            }
            else
            {
                ProductListview.ItemsSource = bl.Product.Products(a => a?.Category.ToString() == CategoriesSelector.SelectedItem.ToString());
            }
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            new modifyProductWindow().Show();
            this.Close();
        }

        private void updateProduct_Click(object sender, MouseButtonEventArgs e)
        {
            if ((BO.ProductForList)ProductListview.SelectedItem != null) 
            {
                var product = (BO.ProductForList)ProductListview.SelectedItem;
                new modifyProductWindow(product.ID).Show();
                this.Close();
            }
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridViewSortByID_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void GridViewSortByName_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void GridViewSortByCategory_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("Category", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }

        private void GridViewSortByPrice_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductListview.ItemsSource);
            view.SortDescriptions.Clear();
            if (hasSorted)
            {
                view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Descending));
                hasSorted = false;
            }
            else
            {
                view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
                hasSorted = true;
            }
        }
    }
}
