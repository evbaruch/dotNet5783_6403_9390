using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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

    public static readonly DependencyProperty MyEstimatedTimeProperty =
        DependencyProperty.Register("estimatedTime", typeof(int), typeof(SimulatorWindow));

    public int estimatedTime
    {
        get { return (int)GetValue(MyEstimatedTimeProperty); }
        set { SetValue(MyEstimatedTimeProperty, value); }
    }

    public int BackTime = 0;

    DispatcherTimer timer = new DispatcherTimer();

    BlApi.IBl? bl = BlApi.Factory.Get();

    public SimulatorWindow()
    {
        //OrderCurrent = bl.Order.OrderTracking((int)bl.Order.PriorityOrder());
        Time ="00:00:00";
        close = "Colse";
        estimatedTime = 10;
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
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

    private async void Close(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Are you sure?", "Just making sure", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel) == MessageBoxResult.OK)
        {
            timer.Stop();
            this.Closing -= Window_Closing;
            await Ramaining_Time();
            this.Closing += WindowSoftClosing;
            this.Close();
        }
    }

    private async Task Ramaining_Time()
    {
        while (estimatedTime > 0)
        {
            estimatedTime--;
            close = "colsing in " + estimatedTime;
            await Task.Delay(1000);
        }
    }


    private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {

    }

    private void SimulationData(object sender, Tuple<BO.Order, int> e)
    {
        //MessageBox.Show(e.ToString());
        estimatedTime = e.Item2;
        OrderCurrent = bl.Order.OrderTracking(e.Item1.ID);
    }

    private void StopSimulation(object sender, EventArgs e)
    {
        MessageBox.Show("end");
        Simulator.StopSimulation();
    }

    private void Start_Button(object sender, RoutedEventArgs e)
    {
        Simulator.SubscribeToUpdateSimulation(SimulationData);
        Simulator.SubscribeToStopSimulation(StopSimulation);
        Simulator.StartSimulation();

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
}
