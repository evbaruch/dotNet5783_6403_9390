using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome9390();
            Welcome6403();
            Console.ReadKey();
        }

        static partial void Welcome6403();
        private static void Welcome9390()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}

