using System;
using System.Runtime.InteropServices;

namespace DO;

public struct Order
{
    Random random = new Random();
    public int ID { set; get; }
    public string? CustomerName { set; get; }
    public string? CustomerEmail { set; get; }
    public string? CstomerAddress { set; get; }
    public DateTime? OrderDate { set ; get; }
    public DateTime? ShipDate { set; get; }
    public DateTime? DeliveryDate { set; get; }
    
    public Order(Order order)
    {
        ID = order.ID;
        CustomerName = order.CustomerName;
        CustomerEmail = order.CustomerEmail;
        CstomerAddress = order.CstomerAddress; 
        OrderDate = order.OrderDate;
        ShipDate = order.ShipDate;
        DeliveryDate = order.DeliveryDate;
    }

    public Order(int id, string customerName, string customerEmail, string customerAddress)
    {

        this.ID = id;
        this.CustomerName = customerName;
        this.CustomerEmail = customerEmail;
        this.CstomerAddress = customerEmail;
        this.OrderDate = DateTime.Now;
        this.ShipDate = this.OrderDate + new TimeSpan(random.Next(0, 2), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59)); // random time from the order time 
        this.DeliveryDate = this.ShipDate + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59)); // random time from the ship time
    }
    public override string ToString() => $@"
    ID: 
    {ID}
    CustomerName: 
    {CustomerName} 
    CustomerEmail: 
    {CustomerEmail} 
    CstomerAddress: 
    {CstomerAddress}
    DeliveryDate: 
    {DeliveryDate}
    ";
};