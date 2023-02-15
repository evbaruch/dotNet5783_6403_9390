using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using PL.AdminWindows;

namespace PL;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
public partial class SimulatorWindow : Window 
{



    public string NewTime
    {
        get { return (string)GetValue(NewTimeProperty); }
        set { SetValue(NewTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NewTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NewTimeProperty =
        DependencyProperty.Register("NewTime", typeof(string), typeof(SimulatorWindow));



    public string currentTime
    {
        get { return (string)GetValue(currentTimeProperty); }
        set { SetValue(currentTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for currentTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty currentTimeProperty =
        DependencyProperty.Register("currentTime", typeof(string), typeof(SimulatorWindow));




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
        set {SetValue(MyEstimatedTimeProperty, value);}
    }

    public static readonly DependencyProperty MymaxBarProperty =
        DependencyProperty.Register("maxBar", typeof(int), typeof(SimulatorWindow));

    public int maxBar
    {
        get { return (int)GetValue(MymaxBarProperty); }
        set { SetValue(MymaxBarProperty, value); }
    }

    public static readonly DependencyProperty MyBarProperty =
       DependencyProperty.Register("BarProgress", typeof(int), typeof(SimulatorWindow));

    public int BarProgress
    {
        get { return (int)GetValue(MyBarProperty); }
        set { SetValue(MyBarProperty, value); }
    }



    public bool flag
    {
        get { return (bool)GetValue(flagProperty); }
        set { SetValue(flagProperty, value); }
    }

    // Using a DependencyProperty as the backing store for flag.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty flagProperty =
        DependencyProperty.Register("flag", typeof(bool), typeof(SimulatorWindow));



    Stopwatch timer2 = new Stopwatch();

    public int BackTime = 0;

    DispatcherTimer timer = new DispatcherTimer();

    BlApi.IBl? bl = BlApi.Factory.Get();

    public SimulatorWindow()
    {
        
        flag = true;
        BarProgress = 0;
        Time ="00:00:00";
        close = "Close";
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += Timer_Tick;
        InitializeComponent();

    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        BarProgress++;

        estimatedTime--;
        if (estimatedTime<0)
        {
            Simulator.StopSimulation();
            timer.Stop();
            MessageBox.Show("Simulation finish", "goog bye!", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
            estimatedTime = 0;
        }
        else
        {
             Time = timer2.Elapsed.ToString();
            //Time = TimeSpan.FromSeconds(++BackTime).ToString(@"hh\:mm\:ss");
        }
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        MessageBox.Show("This is not the way", "Let me down slowly", MessageBoxButton.OK, MessageBoxImage.Question, MessageBoxResult.OK);
        e.Cancel = true;
    }

    private void Window_Closing_whileClose(object sender, CancelEventArgs e)
    {
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
            this.Closing -= Window_Closing;
            this.Closing += Window_Closing_whileClose;
            Simulator.StopSimulation();
            timer2.Stop();
            await Ramaining_Time();
            this.Close();
        }
    }

    private async Task Ramaining_Time()
    {
        while (estimatedTime != 0)
        {
            estimatedTime--;
            close = "Closing in " + estimatedTime;
            await Task.Delay(1000);
        }
        this.Closing += Window_Closing_whileClose;
        this.Closing += WindowSoftClosing;
    }


    private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {

    }

    private void SimulationData(object sender, Tuple<BO.Order, int> e)
    {
        EstimatedTime(e.Item2);
        CurrentOrder(bl.Order.OrderTracking(e.Item1.ID));
    }

    private void CurrentOrder(BO.OrderTracking a)
    {
        if (!CheckAccess())
        {
            Action<BO.OrderTracking> d = CurrentOrder;
            Dispatcher.BeginInvoke(d, new BO.OrderTracking() { ID =  a.ID, OrderStatuses = a.OrderStatuses, Status = a.Status});
        }
        else
        {
            OrderCurrent = a;
        }
    }

    private void EstimatedTime(int a)
    {
        if (!CheckAccess())
        {
            Action<int> d = EstimatedTime;
            Dispatcher.BeginInvoke(d, new object[] { a });
        }
        else
        {
            estimatedTime = a;
            maxBar = a;
            BarProgress = 0;
            NewTime = (DateTime.Now + TimeSpan.FromSeconds(a)).ToString();
            currentTime = DateTime.Now.ToString();
        }
    }

    //private void StopSimulation(object sender, EventArgs e)
    //{
    //    MessageBox.Show("end");
    //    Simulator.StopSimulation();
    //}

    private void Start_Button(object sender, RoutedEventArgs e)
    {
        flag = false;
        timer2.Start();
        timer.Start();
        Simulator.SubscribeToUpdateSimulation(SimulationData);
        //Simulator.SubscribeToStopSimulation(StopSimulation);
        Simulator.StartSimulation();

    }

    private void Stop_Click(object sender, RoutedEventArgs e)
    {
        flag = true;
        timer.Stop();
        timer2.Stop();
        Simulator.StopSimulation();
    }
}
