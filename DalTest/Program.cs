using DO;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using static DO.Enums;

namespace Dal;

partial class Program
{
    static void optionOrder(ref DalOrder dalOrder)
    {
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
                dalOrder.Create(tempOrder);
                break;


            case 'b':
                Console.WriteLine("please enter the ID you want to display");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                tempOrder.ID = ID;
                Console.WriteLine(dalOrder.Read(tempOrder));
                break;


            case 'c':
                IEnumerable<Order> listOfOrder = dalOrder.ReadAll();
                for (int i = 0; i < listOfOrder.Count(); i++)
                {
                    Console.WriteLine(listOfOrder.ElementAt(i));
                }
                break;


            case 'd':
                Console.WriteLine("please enter the ID of the object you want to update");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrder.ID = ID;
                Console.WriteLine(dalOrder.Read(tempOrder));
                Console.WriteLine("please enter your updated name, Email and Address");
                tempOrder.CustomerName = Console.ReadLine();
                tempOrder.CustomerEmail = Console.ReadLine();
                tempOrder.CstomerAddress = Console.ReadLine();
                dalOrder.Update(tempOrder);
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrder.ID = ID;
                dalOrder.Delete(tempOrder);
                break;
            default:
                break;
        }
    }
    static void optionOrderItem(ref DalOrderItem dalOrderItem)
    {
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
                dalOrderItem.Create(tempOrderItem);
                break;


            case 'b':
                Console.WriteLine("please enter the ID you want to display");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                tempOrderItem.ID = ID;
                Console.WriteLine(dalOrderItem.Read(tempOrderItem));
                break;


            case 'c':
                IEnumerable<OrderItem> listOfOrderItem = dalOrderItem.ReadAll();
                for (int i = 0; i < listOfOrderItem.Count(); i++)
                {
                    Console.WriteLine(listOfOrderItem.ElementAt(i));
                }
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to updat");
                int.TryParse(Console.ReadLine(), out ID);
                tempOrderItem.ID = ID;
                Console.WriteLine(dalOrderItem.Read(tempOrderItem));
                Console.WriteLine("please enter (ProductID,OrderID,Price,Amount)");
                tempOrderItem.ProductID  = Console.Read();
                tempOrderItem.OrderID = Console.Read();
                tempOrderItem.Price = Console.Read();
                tempOrderItem.Amount = Console.Read();
                dalOrderItem.Update(tempOrderItem);
                break;


            case 'f':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempOrderItem.ID = ID;
                dalOrderItem.Delete(tempOrderItem);
                break;


            default:
                break;
        }
    }
    static void optionProduct(ref DalProduct dalProduct)
    {
        bool isRead;
        char option;
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
                dalProduct.Create(tempProduct);
                break;


            case 'b':
                Console.WriteLine("please enter Six digits (id)");
                int.TryParse(Console.ReadLine(), out int ID);
                tempProduct.ID = ID;
                Console.WriteLine(dalProduct.Read(tempProduct));
                break;


            case 'd':
                IEnumerable<Product> listOfProduct = dalProduct.ReadAll();
                for (int i = 0; i < listOfProduct.Count(); i++)
                {
                    Console.WriteLine(listOfProduct.ElementAt(i));
                }
                break;


            case 'e':
                Console.WriteLine("please enter the ID of the object you want to updat");
                int.TryParse(Console.ReadLine(), out ID);
                tempProduct.ID = ID;
                Console.WriteLine(dalProduct.Read(tempProduct));
                Console.WriteLine("please enter (name,price,category,inStoke)");
                tempProduct.Name = Console.ReadLine();
                tempProduct.Price = Console.Read();
                productsCategory.TryParse(Console.ReadLine(), out category);
                tempProduct.Category = category;
                tempProduct.InStoke = Console.Read();
                dalProduct.Update(tempProduct);
                break;


            case 'f':
                Console.WriteLine("please enter the ID of the object you want to delete");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                tempProduct.ID = ID;
                dalProduct.Delete(tempProduct);
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
        DalOrder dalOrder = new DalOrder();
        DalOrderItem dalOrderItem = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();

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
                        optionOrder(ref dalOrder);
                        break;
                    case "2":
                        optionOrderItem(ref dalOrderItem);
                        break;
                    case "3":
                        optionProduct(ref dalProduct);
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
