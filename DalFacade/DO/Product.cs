using static DO.Enums;

namespace DO;

public struct Product
{
    public int ID { set; get; }
    public string? Name { set; get; }
    public double? Price { set; get; }
    public /*enum*/productsCategory? Category { set; get; }
    public int? InStoke { set; get; }

    public Product(int id, string name, double price, productsCategory category, int instoke)
    {
        this.ID = id;
        this.Name = name;
        this.Price = price;
        this.Category = category;
        this.InStoke = instoke;
    }

    public override string ToString() => $@"
    ID:        {ID}
    Name:      {Name}
    Price:     {Price}
    Category:  {Category}
    InStoke:   {InStoke}
    ";
}