namespace DO;

public struct OrderItem
{
    public int? ProductID { set; get; }
    public int? OrderID { set; get; }
    public double? Price { set; get; }
    public string? Amount { set; get; }

    public OrderItem(OrderItem orderItem)
    {
       this.ProductID = orderItem.ProductID;
        this.OrderID = orderItem.OrderID;
        this.Price = orderItem.Price;
        this.Amount = orderItem.Amount;
    }

    public OrderItem(int ProductID ,int OrderID ,double Price, string Amount )
    {
        this.ProductID = ProductID;
        this.OrderID = OrderID;
        this.Price = Price;
        this.Amount = Amount;
    }

    public override string ToString() => $@"
    ProductID: {ProductID}
    OrderID: {OrderID}
    Price: {Price}
    Amount: {Amount}
    ";
}
