using System.Runtime.InteropServices;

namespace DO;

public struct Order
{
    public int ID { set; get; }
    public string? CustomerName { set; get; }
    public string? CustomerEmail { set; get; }
    public string? CstomerAddress { set; get; }
    public DateTime? OrderDate { set; get; }
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

    public Order(int id, string customerName, string customerEmail, string customerAddress, DateTime orderDate, DateTime shipDate, DateTime deliveryDate)
    {
        this.ID = id;
        this.CustomerName = customerName;
        this.CustomerEmail = customerEmail;
        this.CstomerAddress = customerEmail;
        this.DeliveryDate = deliveryDate;
        this.OrderDate = orderDate;
        this.ShipDate = shipDate;
    }

    public override string ToString() => $@"
    ID: {ID}
    CustomerName: {CustomerName} 
    CustomerEmail: {CustomerEmail} 
    CstomerAddress: {CstomerAddress}
    DeliveryDate: {DeliveryDate}
    ";
};