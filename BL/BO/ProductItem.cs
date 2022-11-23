
using static BO.Enums;
namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public productsCategory Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }
    
    public override string ToString() => $@"
    ID:        {ID}
    Name:      {Name} 
    Price:     {Price} 
    Category:  {Category}
    Amount:    {Amount}
    InStock:   {InStock}
    ";
}