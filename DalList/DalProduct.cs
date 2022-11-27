using DalApi;
using DO;

namespace Dal;



public class DalProduct : IProduct
{
    public int Create(Product product)
    {
        product.ID = DataSource.Config.get_ID_Product;
        DataSource.listProduct.Add(product);
        return (int)product.ID;
    }
    public Product Read(Product product)
    {
        int I = DataSource.searchOrder(product.ID);
        if (I != -1) // if the ID exist return the details else throw an Error
        {

            return DataSource.listProduct[I];

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }

    }
    public void Update(Product product)
    {
        int I = DataSource.searchOrder(product.ID);
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
        int I = DataSource.searchOrder(product.ID);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            DataSource.listProduct.Remove(product);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }
    public IEnumerable<Product> ReadAll()
    {
        return DataSource.listProduct;
    }
}