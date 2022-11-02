namespace DO;

public struct OrderItem
{
    public int ProductID { set; get; }
    public int? OrderID { set; get; }
    public double? Price { set; get; }
    public string? Amount { set; get; }

    public override string ToString() => $@"
    ProductID: {ProductID}
    OrderID: {OrderID}
    Price: {Price}
    Amount: {Amount}
    ";
}
