
namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public  List<OrderItem>? listOfOrder { get; set; } = new List<OrderItem>();
    public double? TotalPrice { get; set; }

    public Cart()
    {
        TotalPrice = 0;
    }

    public override string ToString()
    {

        string a = $@"
    CustomerName:     {CustomerName}
    CustomerEmail:    {CustomerEmail} 
    CustomerAddress:  {CustomerAddress} ";
        foreach (var item in listOfOrder)
        {
            a = a + "\n" + item.Name + "\n" + item.ToString();
        }
       a = a + "\n"+$@" TotalPrice:       {TotalPrice}";
        return a; 
    }
    
}
