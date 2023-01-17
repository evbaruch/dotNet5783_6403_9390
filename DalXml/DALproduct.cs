using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalApi;
using System.Threading.Tasks;
using DO;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    const string productPath = "Product";
    static XElement config = XmlTools.LoadConfig();
    Random random = new Random();

    public int Create(Product product)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        int checkIDProduct = product.ID;
        try
        {
            if (checkIDProduct >= 100000 && checkIDProduct <= 999999)
            {
                Read(product);
            }
            if (product.Name != null)
            {
                do
                {
                    checkIDProduct = random.Next(100000, 1000000);
                }
                while (Read(new() { ID = checkIDProduct }).Name != null);
            }
            return checkIDProduct;

        }
        catch (IDWhoException)
        {
            product.ID = checkIDProduct;
            listProduct.Add(product);
            XmlTools.SaveListToXMLSerializer(listProduct, productPath);
            return (int)product.ID;
        }
    }

    public Product Read(Product product)
    {

        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        Product? isNULL = ReadObject(a => a?.ID == product.ID);
        int I = listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist return the details else throw an Error
        {

            return (Product)listProduct[I];

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }

    public void Update(Product product)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        Product? isNULL = ReadObject(
            a => a?.ID == product.ID
                            );
        int I = listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listProduct[I] = product;
            XmlTools.SaveListToXMLSerializer(listProduct, productPath);
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }

    public void Delete(Product product)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        Product? isNULL = ReadObject(
           a => a?.ID == product.ID
                           );
        int I = listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            listProduct.Remove(product);
            XmlTools.SaveListToXMLSerializer(listProduct, productPath);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }

    public IEnumerable<Product?> ReadAll(Func<Product?, bool>? func = null)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        //List<Product?> finalResult = new List<Product?>();
        if (func != null)
        {
            var finalResult = from item in listProduct
                              where func(item)
                              select item;

            return finalResult;
        }
        else
        {
            List<Product?> finalResult = new List<Product?>();

            finalResult = listProduct;
            return finalResult;
        }
    }

    public Product ReadObject(Func<Product?, bool>? func)
    {
        List<DO.Product?> listProduct = XmlTools.LoadListFromXMLSerializer<DO.Product>(productPath);

        if (func != null)
        {
            var product = listProduct.Find(x => func(x));
            if (product != null)
            {
                return (Product)product;
            }
        }

        return new()
        {
            ID = -1,
            Name = null,
            InStock = null,
            Price = null,
            Category = null
        };
    }

    
}
