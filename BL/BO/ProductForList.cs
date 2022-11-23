using static BO.Enums;
namespace BO;

public class ProductForList
{
    public int? ID { get; set; }
    public string? CustomerName { get; set; }
    public productsCategory? Status { get; set;}
    public int? AmountOfItems { get; set; }
    public double? TotalPrice { get; set; }

    public override string ToString() => $@"
    ID:             {ID}
    CustomerName:   {CustomerName} 
    Status:         {Status} 
    AmountOfItems:  {AmountOfItems}
    TotalPrice:     {TotalPrice}
    ";
}


