using DalApi;
using DO;
using static DO.Enums;

namespace Dal;



public class DalProduct
{
    public DalProduct()
    {
        DataSource.s_Initialize();
    }
    public int Create(string Name, double Price, productsCategory Category, int InStoke)
    {
        int ID = DataSource.Config.get_ID_Product;
        Product product = new Product(ID, Name, Price, Category, InStoke);
        DataSource.listProduct.Add(product);
        return ID;
    }
    public Product Read(int I)
    {
        if(I < 0 || I >50) // if the index exist return the details else throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listProduct[I];
    }
    public void Update(int ID, Product product)
    {
        int I = DataSource.searchProduct(ID);
        if(I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listProduct[I] = product;
        }
        else
        {
            throw new IndexOutOfRangeException("object doesn't exist - Update");
        }
    }
    public void Delete(int ID)
    {
        int I = DataSource.searchProduct(ID);
        if(I!= -1)// if the ID exist delete the details else throw an Error
        {
            DataSource.listProduct.Remove(ReadID(ID));
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error ");
        }
    }
    public Product ReadID(int ID)
    {
        if (DataSource.searchProduct(ID) == -1)// if the ID exist return the details else throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listProduct[DataSource.searchProduct(ID)];
    }
    public int Product_Length()
    {
        return DataSource.listProduct.Count;
    }
}