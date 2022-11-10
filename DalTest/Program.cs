using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static DO.Enums;

namespace DO;

partial class Program
{
    static void Main(string[] args)
    {
        //Console.Write("a1");
        //var myOrder = new Order();
        //myOrder.CustomerName = "yehuda";
        //a.OrderDate = new DateTime();
        Console.WriteLine("GG");
        //products a;
        //var a = new DalOrder();
        Product b = new Product();
        //b.Category = products.healing;
        //var b = new DataSource();



        for (int i = 0; i < 10; i++)
        {
            Random _R = new Random();
            var v = Enum.GetValues(typeof(productsCategory));
            productsCategory Category = (productsCategory)v.GetValue(_R.Next(v.Length));

            Console.WriteLine(Category);
            Console.WriteLine(_R.Next(0,3));
        }

    }

}
