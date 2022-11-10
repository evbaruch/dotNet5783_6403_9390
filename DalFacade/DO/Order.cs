using System.Runtime.InteropServices;

namespace DO;

public struct Order
{
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
        this.ShipDate = this.OrderDate + new TimeSpan(1,2,3,4);//possebility for random time
        this.DeliveryDate = this.ShipDate + new TimeSpan(1,3,5,7);
    }

    public override string ToString() => $@"
    ID: {ID}
    CustomerName: {CustomerName} 
    CustomerEmail: {CustomerEmail} 
    CstomerAddress: {CstomerAddress}
    DeliveryDate: {DeliveryDate}
    ";
};