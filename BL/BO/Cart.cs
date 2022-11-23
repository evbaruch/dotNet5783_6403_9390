
namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public  List<OrderItem>? listOfOrder { get; set; } = new List<OrderItem>();
    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
    CustomerName:     {CustomerName}
    CustomerEmail:    {CustomerEmail} 
    CustomerAddress:  {CustomerAddress} 
    listOfOrder:      {listOfOrder}
    TotalPrice:       {TotalPrice}
    ";
}
