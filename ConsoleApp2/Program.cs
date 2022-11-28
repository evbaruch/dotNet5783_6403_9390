using BlImplementation;
using BO;
using Dal;
using DalApi;
using DO;

namespace BiTest;


internal class Program
{
    static Cart cart = new Cart();
    static void optionCart()
    {
        Bl access = new Bl();
        int id = 0, Quantity = 0;
        char option = '1';

        while (option != '0')
        {
            Console.WriteLine("\nOrder\n" +
                "a in order to adding an Product to cart\n" +
                "b in order to Update Product Quantity in cart\n" +
                "c in order to Confirme the Order \n" +
                "p in order to print the cart \n" +
                "0 to exit\n" );
            bool isRead = char.TryParse(Console.ReadLine(), out option);
            switch (option)
            {
                case 'a':
                    Console.WriteLine("Enter the ID number of the product you want to add\n");
                    int.TryParse(Console.ReadLine(), out id);
                    access.Cart.AddProduct(cart,id);
                    break;


                case 'b':
                    Console.WriteLine("Enter the product ID number and the quantity the you want to add\n");
                    int.TryParse(Console.ReadLine(), out id);
                    int.TryParse(Console.ReadLine(), out Quantity);
                    access.Cart.UpdateProductQuantity(cart,id,Quantity);
                    break;


                case 'c':
                    Console.WriteLine("Enter your username and email and address\n");
                    cart.CustomerName = Console.ReadLine();
                    cart.CustomerEmail = Console.ReadLine();
                    cart.CustomerAddress = Console.ReadLine();
                    access.Cart.OrderConfirmation(cart);
                    break;

                case 'p':
                    Console.WriteLine(cart);
                    break;

                case '0':
                    break;

                default:
                    Console.WriteLine("wrong input");
                    break;
            }
        }


    }

    static void optionOrder()
    {
        Bl access = new Bl();
        BO.Order order = new BO.Order();
        Console.WriteLine("\nOrder\n" +
            "a in order to adding an Product to cart\n" +
            "b in order to Update Product Quantity in cart\n" +
            "c in order to Confirme the Order \n" +
            "any other letter in order to exit");
        bool isRead = char.TryParse(Console.ReadLine(), out char option);
        switch (option)
        {
            case 'a':
                Console.WriteLine("aaa\n");
                break;


            case 'b':
                Console.WriteLine("bbb\n");

                break;


            case 'c':
                Console.WriteLine("ccc\n");

                break;

            default:
                break;
        }
    }

    static void optionProduct()
    {
        Bl access = new Bl();
        BO.Product product = new BO.Product();
        Console.WriteLine("\nProduct\n" +
    "a in order to view products catalog \n" +
    "b in order to view the details of a poduct by its id \n" +
    "c in order to view the details of a poduct by its id and cart\n" +
    "d in order to add a prudoct\n" +
    "e in order to delete a product\n" +
    "f in order to update a product\n" +
    "any other letter in order to exit");
        bool isRead = char.TryParse(Console.ReadLine(), out char option);
        switch (option)
        {
            case 'a':
                IEnumerable<ProductForList> products = access.Product.Products();
                foreach (var item in products)
                {
                    Console.WriteLine(item);
                };
                break;

            case 'b':
                Console.WriteLine("Enter the ID product you seek to view\n");
                isRead = int.TryParse(Console.ReadLine(), out int ID);
                product.ID = ID;
                Console.WriteLine(access.Product.ProductDetails(ID));
                break;


            case 'c':
                Console.WriteLine("Enter the ID product and the cart you seek to view\n");
                
                break;

            case 'd':
                Console.WriteLine("Enter the ID ,Name ,Price ,Category ,InStock  of product you seek to add\n");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                product.ID = ID;
                product.Name = Console.ReadLine();
                isRead = int.TryParse(Console.ReadLine(), out int price);
                product.Price = price;
                BO.Enums.productsCategory a;
                BO.Enums.productsCategory.TryParse(Console.ReadLine(), out a );
                product.Category = a;
                product.InStock = Console.Read();
                access.Product.AddProduct(product);
                Console.WriteLine();
                break;

            case 'e':
                Console.WriteLine("Enter the ID product and the cart you seek to delete\n");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                product.ID = ID;
                access.Product.DeleteProduct((int)product.ID);
                break;

            case 'f':
                Console.WriteLine("Enter the ID ,Name ,Price ,Category ,InStock of the product you seek to update\n");
                isRead = int.TryParse(Console.ReadLine(), out ID);
                product.ID = ID;
                product.Name = Console.ReadLine();
                isRead = int.TryParse(Console.ReadLine(), out price);
                product.Price = price;
                BO.Enums.productsCategory.TryParse(Console.ReadLine(), out a);
                product.Category = a;
                product.InStock = Console.Read();
                access.Product.UpdateProduct(product);
                break;

            default:
                break;
        }
    }

    static void Main(string[] args)
    {
        IDal access = new DalList();
        IEnumerable<DO.Product> products = access.product.ReadAll();
        foreach (var item in products)
        {
            Console.WriteLine(item);
        }

        // the purpose's program is to check the Dal layer
        Console.WriteLine("welcome to your life ,please enter your choise\n" +
            "0 - exit \n" +
            "1 - Cart \n" +
            "2 - Order \n" +
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
                        optionCart();
                        break;
                    case "2":
                        optionOrder();
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
            if (choice != "0") // if the user haven't enter 0 ask for a new choice
            {
                if (choice == "")
                {
                    choice = Console.ReadLine();
                }
                Console.WriteLine("please enter another choice\n");
            }
        }
    }
}