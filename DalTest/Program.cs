using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DO;

partial class Program
{
    static void Main(string[] args)
    {
        //Console.Write("a1");
        var myOrder = new Order();
        myOrder.CustomerName = "yehuda";
        //a.OrderDate = new DateTime();
        Console.WriteLine(myOrder.ToString());

        var a = new DalOrder();

        //var b = new DataSource();


    }

}
