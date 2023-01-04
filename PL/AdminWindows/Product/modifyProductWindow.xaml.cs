﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.AdminWindows
{
    /// <summary>
    /// Interaction logic for modifyProductWindow.xaml
    /// </summary>
    public partial class modifyProductWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;



        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<BO.Product> _ProductObservableCollection;
        public ObservableCollection<BO.Product> ProductObservableCollection
        {
            get { return _ProductObservableCollection; }
            set
            {
                _ProductObservableCollection = value;
                OnPropertyChanged(nameof(ProductObservableCollection));
            }
        }

        public IEnumerable<BO.Enums.productsCategory> categories { get; set; }
 
        BlApi.IBl? bl = BlApi.Factory.Get();

        public modifyProductWindow()
        {
            categories =  (IEnumerable<BO.Enums.productsCategory>?)Enum.GetValues(typeof(BO.Enums.productsCategory));
            // CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory));
            InitializeComponent();
            updateProduct.IsEnabled = false;
            updateProduct.Visibility = Visibility.Hidden;
        }

        public modifyProductWindow(BO.ProductForList product) // update mode
        {
            ProductObservableCollection = new ObservableCollection<BO.Product> { bl.Product.ProductDetails(product.ID) };
            categories =  (IEnumerable<BO.Enums.productsCategory>?)Enum.GetValues(typeof(BO.Enums.productsCategory));
            InitializeComponent();

            HeadLine.Text = "Updete product"; // defining to match the purpose in this run

            this.DataContext = product;

            AddProduct.IsEnabled = false; // disable the unnecessary butten and hide it
            AddProduct.Visibility = Visibility.Hidden;

            //CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.productsCategory));
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            { //  notice: the ID is able to not be entered because of it randomness property 
              // notice: we had make some improvment about the ID , the ID can be entered by the manager or be determined automatically
              // in case the manager is entering a too short or too long ID the system will notify him and offer to determine it automatically
              // in evry case of exeption the system notify the manager and give him a chance to fix the exeption by changing the input
                if ((Name.Text == "") || (Price.Text == "") || (CategoriesSelector.Text == "") || (inStock.Text == "")) // making sure the text box isn't empty 
                {
                    MessageBox.Show("you missed some details", "Missing details error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                else
                {
                    if (IDTextBox.Text == "")
                    {
                        IDTextBox.Text = "0";
                    }
                    else if (int.Parse(IDTextBox.Text) < 100000 || int.Parse(IDTextBox.Text) > 999999)// making sure the ID is in the right lenght
                    {
                        if (MessageBox.Show("The ID you entered isn't valid ,would you like to get an automatic ID?", "invalid ID", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) == MessageBoxResult.OK)
                        {
                            IDTextBox.Text = "0";
                        }
                        else
                        {
                            return;
                        }
                    }
                    BO.Enums.productsCategory.TryParse(CategoriesSelector.Text, out BO.Enums.productsCategory Categor);
                    bl.Product.AddProduct(new() { ID= int.Parse(IDTextBox.Text), Name = Name.Text, Price = int.Parse(Price.Text), Category = Categor, InStock = int.Parse(inStock.Text) });
                    new ProductListWindow().Show();
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

        private void updateProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((IDTextBox.Text == "") || (Name.Text == "") || (Price.Text == "") || (CategoriesSelector.Text == "") || (inStock.Text == "")) // making sure the text box isn't empty 
                {
                    MessageBox.Show("you missed some details", "Missing details error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
                }
                BO.Enums.productsCategory.TryParse(CategoriesSelector.Text, out BO.Enums.productsCategory Categor);
                bl.Product.UpdateProduct(new() { ID= int.Parse(IDTextBox.Text), Name = Name.Text, Price = int.Parse(Price.Text), Category = Categor, InStock = int.Parse(inStock.Text) });
                new ProductListWindow().Show();
                this.Close();
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
                MessageBox.Show("An error has occurred , it might occurred because of some incorrect input", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Hand, MessageBoxResult.Cancel);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
            this.Close();
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
