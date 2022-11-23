
using static BO.Enums;

namespace BO;

public class Order
{
    public int? ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryrDate { get; set; }
    public List<OrderItem>? Items { get; set; } = new List<OrderItem>();
    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
    ID:              {ID}
    CustomerName:    {CustomerName} 
    CustomerEmail:   {CustomerEmail} 
    CustomerAdress:  {CustomerAdress}
    Status:          {Status}
    OrderDate:       {OrderDate}
    PaymentDate:     {PaymentDate} 
    ShipDate:        {ShipDate} 
    DeliveryrDate:   {DeliveryrDate}
    Items:           {Items}
    TotalPrice:      {TotalPrice}
    ";
}
