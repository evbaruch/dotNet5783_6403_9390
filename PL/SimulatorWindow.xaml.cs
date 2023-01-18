using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using PL.AdminWindows;

namespace PL;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
public partial class SimulatorWindow : Window 
{ 

    public static readonly DependencyProperty MyTrackerProperty =
        DependencyProperty.Register("OrderCurrent", typeof(BO.OrderTracking), typeof(SimulatorWindow));

    public BO.OrderTracking OrderCurrent
    {
        get { return (BO.OrderTracking)GetValue(MyTrackerProperty); }
        set { SetValue(MyTrackerProperty, value); }
    }

    public static readonly DependencyProperty MyTimeProperty =
        DependencyProperty.Register("Time", typeof(string), typeof(SimulatorWindow));

    public string Time
    {
        get { return (string)GetValue(MyTimeProperty); }
        set { SetValue(MyTimeProperty, value); }
    }


    public static readonly DependencyProperty MyButtonProperty =
        DependencyProperty.Register("close", typeof(string), typeof(SimulatorWindow));

    public string close
    {
        get { return (string)GetValue(MyButtonProperty); }
        set { SetValue(MyButtonProperty, value); }
    }

    public int BackTime = 0;

    DispatcherTimer timer = new DispatcherTimer();

    BlApi.IBl? bl = BlApi.Factory.Get();
    int estimatedTime = 0;

    public SimulatorWindow()
    {
        OrderCurrent = bl.Order.OrderTracking((int)bl.Order.PriorityOrder());
        Time ="00:00:00";
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        timer.Start();
        InitializeComponent();

    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        Time =TimeSpan.FromSeconds(++BackTime).ToString(@"hh\:mm\:ss");
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

    private void foo(object sender, Tuple<BO.Order, int> e)
    {
        //MessageBox.Show(e.ToString());
        estimatedTime = e.Item2;
    }

    private void Start_Button(object sender, RoutedEventArgs e)
    {
        Simulator.SubscribeToUpdateSimulation(foo);
        Simulator.StartSimulation();
    }
}
