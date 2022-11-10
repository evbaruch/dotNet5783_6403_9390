namespace Dal;
using DO;
using System.Globalization;
using System.Runtime.InteropServices;
using static DO.Enums;

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
        DataSource.arrProduct[DataSource.Config.I_Product] = product;
        return ID;
    }
    public Product Read(int I)
    {
        if(I < 0 || I >50)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrProduct[I];
    }
    public void Update(int ID, Product product)
    {
        int I = DataSource.searchProduct(ID);
        if(I != -1)
        {
            DataSource.arrProduct[I] = product;
            DataSource.arrProduct[I].ID = ID;//צריך עיון
        }
        else
        {
            throw new IndexOutOfRangeException("object doesn't exist - Update");
        }
    }
    public void Delete(int ID)
    {
        int I = DataSource.searchProduct(ID);
        if(I!= -1)
        {
            for(int J = I;J < DataSource.arrProduct.Length - 1 ;J++)
            {
                DataSource.arrProduct[J] = DataSource.arrProduct[J+1];
            }
            DataSource.Config.I_Product = DataSource.Config.I_Product--;
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error ");
        }
    }
    public Product ReadID(int ID)
    {
        if (DataSource.searchProduct(ID) == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrProduct[DataSource.searchProduct(ID)];
    }
    public int Product_Length()
    {
        return DataSource.Config.I_Product;
    }

}