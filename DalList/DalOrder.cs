namespace Dal;
using DO;
using Microsoft.VisualBasic;
using System.Globalization;
using System.Runtime.InteropServices;

public class DalOrder
{
    public DalOrder()
    {
        DataSource.s_Initialize();
    }
    public int Create(string customerName, string customerEmail, string customerAddress)
    {
        int ID = DataSource.Config.get_ID_Order;
        Order order = new Order(ID, customerName, customerEmail, customerAddress);
        DataSource.listOrder.Add(order);
        return ID;
    }
    public Order Read(int I)
    {
        if (I < 0) // if the index is out of the specified range throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listOrder[I];
    }
    public void Update(int ID, Order order)
    {
        int I = DataSource.searchOrder(ID);
        if(I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listOrder[I] = order;
        }
        else
        {
            throw new IndexOutOfRangeException("object doesn't exist - Update");
        }
    }
    public void Delete(int ID)
    {
        int I = DataSource.searchOrder(ID);
        if(I!= -1) // if the ID exist delete the details else throw an Error
        {
           
                DataSource.listOrder.Remove(ReadID(ID));
            
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error ");
        }
    }
    public Order ReadID(int ID)
    {
        if (DataSource.searchOrder(ID) == -1) // if the ID exist return the details else throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listOrder[DataSource.searchOrder(ID)];
    }
    public int Order_Length()
    {
        return DataSource.listOrder.Count;
    }

}
