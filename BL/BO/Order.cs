
using static BO.Enums;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public OrderStatus? Status { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryrDate { get; set; }
    public List<OrderItem>? Items { get; set; } 
    public double? TotalPrice { get; set; }

    public override string ToString()
    {

        string a = $@"
    ID:              {ID}
    CustomerName:    {CustomerName} 
    CustomerEmail:   {CustomerEmail} 
    CustomerAdress:  {CustomerAddress}
    Status:          {Status}
    OrderDate:       {OrderDate}
    ShipDate:        {ShipDate} 
    DeliveryrDate:   {DeliveryrDate}
    items:";
        foreach (var item in Items)
        {
            a = a + "\n" + item.Name + "\n" + item.ToString();
        }

    a = a + "\n"+ $@"TotalPrice:      {TotalPrice}";
        return a;
    }
}
