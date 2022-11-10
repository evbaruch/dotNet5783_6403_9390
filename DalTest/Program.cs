using DO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            "3 - product.");

        int choice = 1;
        char option = 'a';
        for (int i = 0;choice != 0; i++)
        {
            bool isRead = int.TryParse(Console.ReadLine(), out int choise);
            switch (choise)
            {
                case 1:
                    Console.WriteLine("\nEnter\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n");
                    isRead = char.TryParse(Console.ReadLine(), out option);
                    switch (option)
                    {
                        case 'a':

                            break;
                        case 'b':
                            break;
                        case 'c':
                            break;
                        case'd':
                            break;
                        case 'e':
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n");
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
                        default:
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter\n" +
            "a in order to adding an object\n" +
            "b in order to present the object details according to the ID\n" +
            "c in order to present the object's list\n" +
            "d in order to update the object's details\n" +
            "e in order to delete an object from its list\n");
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
                        default:
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("wrong input");
                    break;

            }
        }
    }

}
