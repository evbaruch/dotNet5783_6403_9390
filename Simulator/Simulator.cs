﻿using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Simulator
{
    private static IBl bl = Factory.Get();

    private static event EventHandler<Tuple<Order, int>>? updateSimulation;

    private static volatile bool isSimulationStoped = false;

    private static BackgroundWorker backWorker = new BackgroundWorker();


    private static Thread? thread;



    public static void SubscribeToUpdateSimulation(EventHandler<Tuple<Order, int>> handler)
    {
        updateSimulation += handler;
    }

    public static void StartSimulation()
    {
        
        //thread = new Thread(simulation);// { Name = "Simulation" };
        //thread.Start();


        backWorker.DoWork += (sender, e) => { simulation(); };
        backWorker.WorkerSupportsCancellation = true;
        backWorker.RunWorkerCompleted += RunWorkerCompleted;
        backWorker.RunWorkerAsync();


        isSimulationStoped = false;
    }

    public static void StopSimulation()
    {
        isSimulationStoped = true;
        if(backWorker.IsBusy)
        {
            backWorker.CancelAsync();
        }
        //thread?.Interrupt();
    }


    private static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    { 
        // Handle task completion
    }


    private static void sleep(int seconds)
    {
        try { Thread.Sleep(seconds * 1000); } catch (ThreadInterruptedException) { }
    }

    private static void simulation()
    {
        while (!isSimulationStoped && bl.Order.PriorityOrder() != null)
        {
            var order = bl.Order.OrderDetailsRequest(bl.Order.PriorityOrder() ?? throw new NullReferenceException());
            var timeToHandle = new Random().Next(3, 7);
            var aproximateTime = new Random().Next(timeToHandle ,timeToHandle);

            updateSimulation?.Invoke(null, new Tuple<Order, int>(order, aproximateTime));

            sleep(timeToHandle);
            if (isSimulationStoped)
                break;

            if (order.Status == BO.Enums.OrderStatus.ordered)
                bl.Order.OrderShippingUpdate(order.ID);
            else if (order.Status == Enums.OrderStatus.shipped)
                bl.Order.UpdateDeliveryOrder(order.ID);
            else
                StopSimulation();
        }
        StopSimulation();
    }
}