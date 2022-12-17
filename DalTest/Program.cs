using DO;
using DalApi;
using static DO.Enums;

namespace Dal;

partial class Program
{
    static void optionOrder()
    {
        DalApi.IDal? access = DalApi.Factory.Get();
        Order tempOrder = new Order();
        Console.WriteLine("\nOrder\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n" +
            "any other letter in order to exit");
        bool isRead = char.TryParse(Console.ReadLine(), out char option);
        switch (option)
        {
            case 'a':
                Console.WriteLine("please enter your name, Email and Address\n");
                tempOrder.CustomerName = Console.ReadLine();
                tempOrder.CustomerEmail = Console.ReadLine();
                tempOrder.CstomerAddress = Console.ReadLine();
                access.order.Create(tempOrder);
                break;


            case 'b':
                Console.WriteLine("please enter the ID you want to display");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                tempOrder.ID = ID;
                Console.WriteLine(access.order.Read(tempOrder));
                break;


            case 'c':
                IEnumerable<Order?> listOfOrder = access.order.ReadAll();
                for (int i = 0; i < listOfOrder.Count(); i++)
                {
                    Console.WriteLine(listOfOrder.ElementAt(i));
                }
                break;


            case 'd':
                Console.WriteLine("please enter the ID of the object you want to update");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrder.ID = ID;
                Console.WriteLine(access.order.Read(tempOrder));
                Console.WriteLine("please enter your updated name, Email and Address");
                tempOrder.CustomerName = Console.ReadLine();
                tempOrder.CustomerEmail = Console.ReadLine();
                tempOrder.CstomerAddress = Console.ReadLine();
                access.order.Update(tempOrder);
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrder.ID = ID;
                access.order.Delete(tempOrder);
                break;
            default:
                break;
        }
    }
    static void optionOrderItem()
    {
        DalApi.IDal? access = DalApi.Factory.Get();
        OrderItem tempOrderItem = new OrderItem();
        Console.WriteLine("Order Item\n" +
                "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n" +
            "any other letter in order to exit");

        bool isRead = char.TryParse(Console.ReadLine(), out char option);
        switch (option)
        {
            case 'a':
                Console.WriteLine("please enter (ProductID,OrderID,Price,Amount)");
                tempOrderItem.ProductID  = Console.Read();
                tempOrderItem.OrderID = Console.Read();
                tempOrderItem.Price = Console.Read();
                tempOrderItem.Amount = Console.Read();
                access.orderItem.Create(tempOrderItem);
                break;


            case 'b':
                Console.WriteLine("please enter the ID you want to display");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                tempOrderItem.ID = ID;
                Console.WriteLine(access.orderItem.Read(tempOrderItem));
                break;


            case 'c':
                IEnumerable<OrderItem?> listOfOrderItem = access.orderItem.ReadAll();
                for (int i = 0; i < listOfOrderItem.Count(); i++)
                {
                    Console.WriteLine(listOfOrderItem.ElementAt(i));
                }
                break;


            case 'd':
                Console.WriteLine("please enter the ID of the object you want to updat");
                int.TryParse(Console.ReadLine(), out ID);
                tempOrderItem.ID = ID;
                Console.WriteLine(access.orderItem.Read(tempOrderItem));
                Console.WriteLine("please enter (ProductID,OrderID,Price,Amount)");
                tempOrderItem.ProductID  = Console.Read();
                tempOrderItem.OrderID = Console.Read();
                tempOrderItem.Price = Console.Read();
                tempOrderItem.Amount = Console.Read();
                access.orderItem.Update(tempOrderItem);
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrderItem.ID = ID;
                access.orderItem.Delete(tempOrderItem);
                break;


            default:
                break;
        }
    }
    static void optionProduct()
    {
        bool isRead;
        char option;
        DalApi.IDal? access = DalApi.Factory.Get();
        Product tempProduct = new Product();
        Console.WriteLine("\nProduct\n" +
            "a in order to adding an object\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n" +
            "any other letter in order to exit");

        isRead = char.TryParse(Console.ReadLine(), out option);
        switch (option)
        {
            case 'a':
                productsCategory category;
                Console.WriteLine("please enter (name,price,category,inStoke)");
                tempProduct.Name = Console.ReadLine();
                tempProduct.Price = Console.Read();
                productsCategory.TryParse(Console.ReadLine(), out category);
                tempProduct.Category = category;
                tempProduct.InStoke = Console.Read();
                access.product.Create(tempProduct);
                break;


            case 'b':
                Console.WriteLine("please enter Six digits (id)");
                int.TryParse(Console.ReadLine(), out int ID);
                tempProduct.ID = ID;
                Console.WriteLine(access.product.Read(tempProduct));
                break;


            case 'c':
                IEnumerable<Product?> listOfProduct = access.product.ReadAll();
                for (int i = 0; i < listOfProduct.Count(); i++)
                {
                    Console.WriteLine(listOfProduct.ElementAt(i));
                }
                break;


            case 'd':
                Console.WriteLine("please enter the ID of the object you want to updat");
                int.TryParse(Console.ReadLine(), out ID);
                tempProduct.ID = ID;
                Console.WriteLine(access.product.Read(tempProduct));
                Console.WriteLine("please enter (name,price,category,inStoke)");
                tempProduct.Name = Console.ReadLine();
                tempProduct.Price = Console.Read();
                productsCategory.TryParse(Console.ReadLine(), out category);
                tempProduct.Category = category;
                tempProduct.InStoke = Console.Read();
                access.product.Update(tempProduct);
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempProduct.ID = ID;
                access.product.Delete(tempProduct);
                break;


            default:
                break;
        }
    }
    static void Main(string[] args)
    {


        // the purpose's program is to check the Dal layer
        Console.WriteLine("welcome to your life ,please enter your choise\n" +
            "0 - exit \n" +
            "1 - Order \n" +
            "2 - Order Item \n" +
            "3 - product.\n" +
            "please enter a choice\n");

        string choice = "";

        for (int i = 0; choice != "0"; i++) // as long as the user haven't enter 0 continue to ask for new choice
        {
            try
            {
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        Console.WriteLine("goodbye");
                        break;
                    case "1":
                        optionOrder();
                        break;
                    case "2":
                        optionOrderItem();
                        break;
                    case "3":
                        optionProduct();
                        break;
                    default:
                        Console.WriteLine("wrong input");
                        break;
                }
            }
            catch (Exception messege)
            {
                Console.WriteLine(messege.Message);
            }
            if (choice!="0") // if the user haven't enter 0 ask for a new choice
            {
                Console.WriteLine("please enter another choice\n");
            }
        }
        
    }       
}
