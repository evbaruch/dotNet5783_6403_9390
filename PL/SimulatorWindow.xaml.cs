using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PL.AdminWindows;
using Simulator;

namespace PL;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
public partial class SimulatorWindow : Window 
{ 

    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register("OrderCurrent", typeof(BO.OrderTracking), typeof(modifyProductWindow));

    public BO.OrderTracking OrderCurrent
    {
        get { return (BO.OrderTracking)GetValue(MyPropertyProperty); }
        set { SetValue(MyPropertyProperty, value); }
    }


    BlApi.IBl? bl = BlApi.Factory.Get();

    public SimulatorWindow()
    {
        OrderCurrent = bl.Order.OrderTracking((int)bl.Order.PriorityOrder());
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
        if (MessageBox.Show("Are you sure?", "Just making sure", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) == MessageBoxResult.OK)
        {
            this.Closing -= Window_Closing;
            this.Closing += WindowSoftClosing;
            this.Close();
        }
    }

    private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {

    }

    private void Start_Button(object sender, RoutedEventArgs e)
    {
        

        Simulator.Simulator.RunningSimulation();
    }
}
