namespace DO;

public struct OrderItem
{
    public int ID { set; get; }
    public int? ProductID { set; get; }
    public int? OrderID { set; get; }
    public double? Price { set; get; }
    public int? Amount { set; get; }

    public OrderItem(OrderItem orderItem)
    {
        this.ID = orderItem.ID;
        this.ProductID = orderItem.ProductID;
        this.OrderID = orderItem.OrderID;
        this.Price = orderItem.Price;
        this.Amount = orderItem.Amount;
    }

    public OrderItem(int ID,int ProductID ,int OrderID ,double Price, int Amount )
    {
        this.ID = ProductID;
        this.ProductID = ProductID;
        this.OrderID = OrderID;
        this.Price = Price;
        this.Amount = Amount;
    }

    public override string ToString() => $@"
    ID: {ID}
    ProductID: {ProductID}
    OrderID: {OrderID}
    Price: {Price}
    Amount: {Amount}
    ";
}
