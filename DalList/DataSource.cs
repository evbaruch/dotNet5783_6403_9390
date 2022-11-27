﻿using DO;
using System.Diagnostics;
using System.Xml.Linq;
using static DO.Enums;


namespace Dal;
internal static class DataSource
{
    //Our temporary database
    internal static readonly Random random = new Random();
    internal static List<Order> listOrder = new List<Order>();
    internal static List<OrderItem> listOrderItem = new List<OrderItem>();
    internal static List<Product> listProduct = new List<Product>();

    //Name data and price data
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
        //The constructor fills some of the arrays in a random way at the very beginning
        s_Initialize();
    }

    internal static bool addOrder(Order a)
    {
        if (true) 
        { 
            listOrder.Add(a);
            return true;
        }
        return false;
    }
    private static Order addOrder(string CustomerName, string CustomerEmail, string CstomerAddress)
    {
        //It randomly updates order data other than user data

        Order order = new Order();
        order.ID = Config.get_ID_Order;
        order.CustomerName = CustomerName;
        order.CustomerEmail = CustomerEmail;
        order.CstomerAddress = CstomerAddress;
        order.OrderDate = DateTime.Now;

        //It randomly updates when the product is shipped eighty percent of the time it has already been shipped otherwise it hasn't been shipped yet
        int _random = random.Next(0, 10);
        if (_random <= 8)
        {
            order.ShipDate = order.OrderDate + new TimeSpan(random.Next(0, 2), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));

            //In case it is sent then fifty percent that yours has already reached the customer
            if (_random <= 4)
            {
                order.DeliveryDate = order.ShipDate + new TimeSpan(random.Next(0, 7), random.Next(0, 59), random.Next(0, 59), random.Next(0, 59));
            }
            else
            {
                order.DeliveryDate = DateTime.MinValue;

            }
        }
        else
        {
            order.ShipDate = DateTime.MinValue;

        }

        return order;
    }
    internal static bool addOrderItem(OrderItem a)
    {
        //We will check here that we have space left in the dataset
        if (true)
        {
            listOrderItem.Add(a);
            return true;
        }
        return false;
    }
    private static OrderItem addOrderItem(int ProductID, int OrderID, double Price)
    {
        //This constructor depends on both user and order data

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
        //We will check here that we have space left in the dataset
        if (true)
        {
            listProduct.Add(a);  
            return true;
        }
        return false;
    }
    private static Product addProduct()
    {
        Product _product = new Product();

        _product.ID = Config.get_ID_Product;

        //All this fuss is to get a random category
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
        //This function builds our python arrays with data

        //User data
        string[] CustomerName = new string[] { "yehuda", "Batman", "Evyatar", "Rabin", "Shmuel", "Kaplan", "sapphire", "bshan", "harry", "potter" };
        string[] CustomerEmail = new string[] { "yehuda@gmail.com", "Batman@gmail.com", "Evyatar@gmail.com", "Rabin@gmail.com", "Shmuel@gmail.com", "Kaplan@gmail.com", "sapphire@gmail.com", "bshan@gmail.com", "harry@gmail.com", "potter@gmail.com" };
        string[] CustomerAddress = new string[] { "Jerusalem", "Paris", "Tsfat", "Tel aviv", "Barcelona", "Vina", "Hugurts", "Tberia", "london", "no were street 26 secund floor" };

        //Order
        for (int i = 0; i < 20; i++)
        {
            int Index = random.Next(0, 10);
            listOrder.Add(addOrder(CustomerName[Index], CustomerEmail[Index], CustomerAddress[Index]));
        }

        //Product
        for (int i = 0; i < 10; i++)
        {
            listProduct.Add(addProduct());
        }

        //OrderItem
        for (int i = 0; i < 40; i++)
        {
            int Index = random.Next(0, 10);
            int ProductID = (int)listProduct[Index].ID;
            double Price = (double)listProduct[Index].Price;
            Index = random.Next(0, 20);
            int OrderID = listOrder[Index].ID;
            listOrderItem.Add(addOrderItem(ProductID, OrderID, Price));
        }

    }

    //These three functions get the hash of the datum and return its index to me
    internal static int searchOrder(int? ID)
    {
        for (int i = 0; i < listOrder.Count; i++)
		{
            if (listOrder[i].ID == ID)
            {
                return i;
            }
		}
        return -1;
    }
    internal static int searchProduct(int ID)
    {
        for (int i = 0; i < listProduct.Count; i++)
		{
            if (listProduct[i].ID == ID)
            {
                return i;
            }
		}
        return -1;
    }
    internal static int searchOrderItem(int ID)
    {
        for (int i = 0; i < listOrderItem.Count; i++)
		{
            if (listOrderItem[i].OrderID == ID)
            {
                return i;
            }
		}
        return -1;
    }

    internal static class Config
    {
        //The index of how many we filled the dataset
        //internal static int I_Order = 0;
        //internal static int I_OrderItem = 0;
        //internal static int I_Product = 0;

        //The product will go up by one every time we make another order
        private static int ID_Order = 100000;
        public static int get_ID_Order
        {
            get
            {
                ID_Order++;
                return ID_Order;
            }
        }

        //OrderItem will go up by one every time we make another order
        private static int ID_OrderItem = 100000;
        public static int get_ID_OrderItem
        {
            get
            {
                ID_OrderItem++;
                return ID_OrderItem;
            }
        }

        //This product gives a random number with six digits
        private static int ID_Product = 0;
        public static int get_ID_Product
        {
            get
            {
                ID_Product = random.Next(100000, 1000000);
                return ID_Product;
            }
        }
    }
}