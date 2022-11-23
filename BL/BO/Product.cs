using static BO.Enums;
namespace BO;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public productsCategory Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    ID:        {ID}
    Name:      {Name} 
    Price:     {Price} 
    Category:  {Category}
    InStock:   {InStock}
    ";

}
