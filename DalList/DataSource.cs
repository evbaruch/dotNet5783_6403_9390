﻿namespace Dal;
using DO;
using System.Diagnostics;
using System.Xml.Linq;
using static DO.Enums;

internal static class DataSource
{
    internal static readonly Random random = new Random();
    internal static Order[] arrOrder = new Order[100];
    internal static OrderItem[] arrOrderItem = new OrderItem[200];
    internal static Product[] arrProduct = new Product[50];

    internal static string[,] productsNames = new string[5, 3] { {"angel","centaur","satyr" },
                                                           {"muscles","brain","sight" },
                                                           {"cancer","inherited illness","fast healing" },
                                                           {"boy","girl","hermaphrodite" },
                                                           {"computer brain","hand weapon","Rick and Morty" } };
    internal static int[,] productsPrice = new int[5, 3] { { 700, 650,500},
                                                           {250,800,300},
                                                           {1000,900,750},
                                                           {2000,2000,1500},
                                                           {5000,400,690} };

    static DataSource()
    {
        s_Initialize();
    }

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
    private static Order addOrder(string CustomerName, string CustomerEmail, string CstomerAddress)
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
    private static OrderItem addOrderItem(int ProductID, int OrderID, double Price)
    {
        OrderItem orderItem = new OrderItem();
        orderItem.ID = Config.get_ID_OrderItem;
        orderItem.ProductID = ProductID;
        orderItem.OrderID = OrderID;
        orderItem.Price = Price;
        orderItem.Amount = random.Next(1, 4);
        return orderItem;
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
    private static Product addProduct()
    {
        Product _product = new Product();

        _product.ID = Config.get_ID_Product;

        //כול הקישקוש הזה זה כדי לקבל קטגוריה רנדולמלית
        var v = Enum.GetValues(typeof(productsCategory));
        productsCategory eunmVal = (productsCategory)v.GetValue(random.Next(v.Length));
        int num = random.Next(0, 3);

        _product.Category = eunmVal;
        _product.Name = productsNames[(int)eunmVal,num];
        _product.Price = productsPrice[(int)eunmVal, num];   
        _product.InStoke = random.Next(2, 12);

        return _product;
    }

    internal static void s_Initialize()
    {
        string[] CustomerName = new string[] {"yehuda", "Batman", "Evyatar", "Rabin", "Shmuel", "Kaplan", "sapphire", "bshan", "harry","potter" };
        string[] CustomerEmail = new string[] {"yehuda@gmail.com", "Batman@gmail.com", "Evyatar@gmail.com", "Rabin@gmail.com", "Shmuel@gmail.com", "Kaplan@gmail.com", "sapphire@gmail.com", "bshan@gmail.com", "harry@gmail.com", "potter@gmail.com" };
        string[] CstomerAddress = new string[] { "Jerusalem" };

        //ממלה 
        for (int i = 0; i < 20; i++)
        {
            int Index = random.Next(0, 10);
            arrOrder[i] = addOrder(CustomerName[Index], CustomerEmail[Index], CstomerAddress[Index]);
        }
        for (int i = 0; i < 10; i++)
        {
            arrProduct[i] = addProduct();
        }
        for (int i = 0; i < 40; i++)
        {
            int Index = random.Next(0, 10);
            int ProductID = arrProduct[Index].ID;
            double Price = (double)arrProduct[Index].Price;

            Index = random.Next(0, 20);
            int OrderID = arrOrder[Index].ID;

            arrOrderItem[i] = addOrderItem(ProductID, OrderID, Price);
        }
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