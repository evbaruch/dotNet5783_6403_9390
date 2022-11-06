namespace Dal;
using DO;
internal static class DataSource
{
    static readonly Random random = new Random();
    internal static Order[] arrOrder = new Order[100];
    internal static OrderItem[] arrOrderItem = new OrderItem[200];
    internal static Product[] arrProduct = new Product[50];
}

//private void createOrderitems()
//{
//    for (int i = 0; i < 10; i++)
//    {
//        Product? product = arrProducts[s_rand.Next(arrProduct.Count)];
//        arrOrderItem.Add
//        (
//            new OrderItem
//            {
//                OrderID = s_rand.Next(Config.s_startOrderNumber, Config.s_startOrderNumber + arrOrder.Count),
//                ProductID = product?.ID ?? 0,
//                Price = product?.Price ?? 0,
//                Amount = s_rand.Next(5),
//            }
//        );
//    }
//}

