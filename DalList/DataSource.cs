namespace Dal;
using DO;
using System.Collections.Concurrent;
using System.Data;
using System.Xml.Linq;

internal static class DataSource
{
    internal static string[,] products = new string[5,3] { {"angel 700","centaur 650","satyr 500" },
                                                           {"muscles 250","brain 800","sight 300" },
                                                           {"cancer 1000","inherited illness 900","fast healing 750" },
                                                           {"boy 2000","girl 2000","hermaphrodite 1500" },
                                                           {"computer brain 5000","hand weapon 400","Rick and Morty 690" } };
    internal static readonly Random random = new Random();
    internal static Order[] arrOrder = new Order[100];
    internal static OrderItem[] arrOrderItem = new OrderItem[200];
    internal static Product[] arrProduct = new Product[50];


    internal static bool addOrder(Order a)
    {
        if(Config.I_Order < arrOrder.Length)
        {
            arrOrder[Config.I_Order] = a;
            Config.I_Order++;
            return true;
        }
        return false;
    }

    internal static bool addOrderItem(OrderItem a)
    {
        if(Config.I_OrderItem < arrOrderItem.Length)
        {
            arrOrderItem[Config.I_OrderItem] = a;
            Config.I_OrderItem++;
            return true;
        }
        return false;
    }

    internal static bool addProduct(Product a)
    {
        if(Config.I_Product < arrProduct.Length)
        {
            arrProduct[Config.I_Product] = a;
            Config.I_Product++;
            return true;
        }
        return false;
    }

    internal static void s_Initialize()
    {
        string[] CustomerName = new string[] {"yehuda", "Batman", "Evyatar", "Rabin", "Shmuel", "Kaplan", "sapphire", "bshan", "harry","potter" };
        string[] CustomerEmail = new string[] {"yehuda@gmail.com", "Batman@gmail.com", "Evyatar@gmail.com", "Rabin@gmail.com", "Shmuel@gmail.com", "Kaplan@gmail.com", "sapphire@gmail.com", "bshan@gmail.com", "harry@gmail.com", "potter@gmail.com" };
        string[] CstomerAddress = new string[] { "Jerusalem" };
    }

    internal static int searchOrder(int ID)
    {
        for (int i = 0; i < Config.I_Order; i++)
		{
            if (arrOrder[i].ID == ID)
            {
                return i;
            }
		}
        return -1;
    }

    internal static int searchProduct(int ID)
    {
        for (int i = 0; i < Config.I_Product; i++)
		{
            if (arrProduct[i].ID == ID)
            {
                return i;
            }
		}
        return -1;
    }

    internal static int searchOrderItem(int ID)
    {
        for (int i = 0; i < Config.I_OrderItem; i++)
		{
            if (arrOrderItem[i].OrderID == ID)
            {
                return i;
            }
		}
        return -1;
    }

    private static Order addOrder(string CustomerName, string CustomerEmail,string CstomerAddress, )
    {
        Order order = new Order();
        order.ID = Config.get_ID_Order; 
        order.CustomerName = CustomerName;
        order.CustomerEmail = CustomerEmail;
        order.CstomerAddress = CstomerAddress;
        order.OrderDate = DateTime.Now;
        TimeSpan convert = TimeSpan.FromMinutes(random.Next(60, 1440));
        order.ShipDate = DateTime.Parse(convert.ToString() + order.OrderDate.ToString());
        order.DeliveryDate = DateTime.Parse(convert.ToString() + order.ShipDate.ToString());
        return order;              
    }

    private static OrderItem addOrderItem(int ProductID, int OrderID, double Price)
    {
        OrderItem orderItem = new OrderItem();
        orderItem.ID = Config.get_ID_OrderItem;
        orderItem.ProductID = ProductID;
        orderItem.OrderID = OrderID;
        orderItem.Price = Price;
        orderItem.Amount = random.Next(1,4);
        return orderItem;
    }

    internal static class Config
    {
        internal static int I_Order = 0;
        internal static int I_OrderItem = 0;
        internal static int I_Product = 0;

        private static int ID_Order = 100000;
        public static int get_ID_Order 
        {
            get 
            { 
                ID_Order++;
                return ID_Order; 
            }
        }

        private static int ID_OrderItem = 100000;
        public static int get_ID_OrderItem
        {
            get
            {
                ID_OrderItem++;
                return ID_OrderItem;
            }
        }

        private static int ID_Product = 0;
        public static int get_ID_Product 
        {
            get 
            {
                ID_Product = random.Next(100000,1000000);
                return ID_Product; 
            }
        }
    }

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

