﻿namespace Dal;
using DO;
internal static class DataSource
{
    internal static string[,] products = new string[5,3] { {"angel","centaur","satyr" },
                                                           {"muscles","brain","sight" },
                                                           {"cancer","inherited illness","fast healing" },
                                                           {"boy","girl","hermaphrodite" },
                                                           {"computer brain","hand weapon","Rick and Morty" } };
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

