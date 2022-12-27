using DalApi;
using DO;

namespace Dal;

public class DalProduct : IProduct
{
    public int Create(Product product)
    {
        int checkIDProduct = product.ID;
        try
        {
            if (checkIDProduct >= 100000 && checkIDProduct <= 999999)
            {
                Read(product);
            }
            if(product.Name != null)
            {
                do
                {
                    checkIDProduct = DataSource.Config.get_ID_Product;
                }
                while (Read(new() { ID = checkIDProduct }).Name != null);
            }
                return checkIDProduct;
            
        }
        catch (IDWhoException)
        {
            product.ID = checkIDProduct;
            DataSource.listProduct.Add(product);
            return (int)product.ID;
        }
    }
    public Product Read(Product product)
    {
        Product? isNULL = ReadObject(a => a?.ID == product.ID);
        int I = DataSource.listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist return the details else throw an Error
        {

            return (Product)DataSource.listProduct[I];

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }

    }
    public void Update(Product product)
    {
        Product? isNULL = ReadObject(
            a => a?.ID == product.ID
                            );
        int I = DataSource.listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listProduct[I] = product;
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }
    public void Delete(Product product)
    {
        Product? isNULL = ReadObject(
            a => a?.ID == product.ID
                            );
        int I = DataSource.listProduct.IndexOf(isNULL);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            DataSource.listProduct.Remove(product);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }
    public IEnumerable<Product?> ReadAll(Func<Product?, bool>? func)
    {
        //List<Product?> finalResult = new List<Product?>();
        if (func != null)
        {
            var finalResult = from item in DataSource.listProduct
                              where func(item)
                              select item;

            return finalResult;
        }
        else
        {
            List<Product?> finalResult = new List<Product?>();

            finalResult = DataSource.listProduct;
            return finalResult;
        }
    }

    public Product ReadObject(Func<Product?, bool>? func)
    {


        if (func != null)
        {
            var product = DataSource.listProduct.Find(x => func(x));
            return (Product)product;
        }

        //foreach (var item in DataSource.listProduct)
        //{

        //    if (func(item))
        //    {
        //        return (Product)item;
        //    }
        //}

        return new()
        {
            ID = -1,
            Name = null,
            InStoke = null,
            Price = null,
            Category = null
        };
    }
}