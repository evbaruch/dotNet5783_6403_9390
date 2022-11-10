using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static DO.Enums;

namespace Dal;

partial class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("welcome to your life ,please enter your choise\n" +
            "0 - exit \n" +
            "1 - Order \n" +
            "2 - Order Item \n" +
            "3 - product.\n" +
            "please enter a choice\n");

        int choice = 1;
        char option = 'a';
        string name = "";
        string email = "";
        string address = "";

        DalOrder order = new DalOrder();
        DalOrderItem item = new DalOrderItem();
        DalProduct dalProduct = new DalProduct();

        for (int i = 0;choice != 0; i++)
        {

            bool isRead = int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 0:
                    Console.WriteLine("goodbye");
                    break;
                case 1:
                    Console.WriteLine("\nOrder\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object details according to the index\n" +
            "d in order to present the object's list\n" +
            "e in order to update the object's details\n" +
            "f in order to delete an object from its list\n" +
            "any other letter in order to exit");
                    isRead = char.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 'a':
                            Console.WriteLine("please enter your name, Email and Address\n");
                            name = Console.ReadLine();
                            email = Console.ReadLine();
                            address = Console.ReadLine();
                            order.Create(name, email, address);
                            break;
                        case 'b':
                            Console.WriteLine("please enter the ID you want to display");
                            isRead = int.TryParse(Console.ReadLine(), out int ID);
                            Console.WriteLine(order.ReadID(ID));
                            break;
                        case 'c':
                            Console.WriteLine("please enter the index you want to display");
                            isRead = int.TryParse(Console.ReadLine(), out int I);
                            Console.WriteLine(order.Read(I));
                            break;
                        case 'd':
                            
                            break;
                        case 'e':
                            Console.WriteLine("please enter the ID of the object you want to update");
                            isRead = int.TryParse(Console.ReadLine(), out ID);
                            Console.WriteLine("please enter your updated name, Email and Address");
                            name = Console.ReadLine();
                            email = Console.ReadLine();
                            address = Console.ReadLine();
                            Order updateObject = new Order(ID, name, email, address);
                            order.Update(ID,updateObject);
                            break;
                        case'f':
                            Console.WriteLine("please enter the ID of the object you want to delete");
                            isRead = int.TryParse(Console.ReadLine(), out ID);
                            order.Delete(ID);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("Order Item\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object details according to the index\n" +
            "d in order to present the object's list\n" +
            "e in order to update the object's details\n" +
            "f in order to delete an object from its list\n" +
            "any other letter in order to exit");
                    isRead = char.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 'a':
                            break;
                        case 'b':
                            break;
                        case 'c':
                            break;
                        case 'd':
                            break;
                        case 'e':
                            break;
                        case 'f':
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("\nProduct\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object details according to the index\n" +
            "d in order to present the object's list\n" +
            "e in order to update the object's details\n" +
            "f in order to delete an object from its list\n" +
            "any other letter in order to exit");
                    isRead = char.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 'a':
                            Console.WriteLine("please enter (name,price,category,inStoke)");
                            double price;
                            productsCategory category;
                            int inStoke;
                            name = Console.ReadLine();
                            double.TryParse(Console.ReadLine(), out price);
                            productsCategory.TryParse(Console.ReadLine(), out category);
                            int.TryParse(Console.ReadLine(), out inStoke);

                            dalProduct.Create(name, price, category, inStoke);
                            break;
                        case 'b':
                            Console.WriteLine("please enter Six digits (id)");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id);

                            Console.WriteLine(dalProduct.Read(id));
                            break;
                        case 'c':
                            Console.WriteLine("please enter (index)");
                            int index = 0;
                            int.TryParse(Console.ReadLine(), out index);

                            Console.WriteLine(dalProduct.Read(index));
                            break;
                        case 'd':
                            for (int _i = 0; _i < dalProduct.Product_Length(); _i++)
                            {
                                Console.WriteLine(dalProduct.Read(_i));
                            }
                            break;
                        case 'e':
                            Console.WriteLine("please enter the ID of the object you want to updat");
                            int _id;
                            int.TryParse(Console.ReadLine(), out _id);

                            Console.WriteLine("please enter (name,price,category,inStoke)");
                            string _name;
                            double _price;
                            productsCategory _category;
                            int _inStoke;

                            _name = Console.ReadLine();
                            double.TryParse(Console.ReadLine(), out _price);
                            productsCategory.TryParse(Console.ReadLine(), out _category);
                            int.TryParse(Console.ReadLine(), out _inStoke);

                            Product product = new Product(_id, _name, _price, _category, _inStoke);

                            dalProduct.Update(_id, product);
                            break;
                        case 'f':
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("wrong input");
                    break;
            }
            Console.WriteLine("please enter another choice\n");
        }
    }

}
