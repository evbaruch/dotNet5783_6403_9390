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
        DataSource.arrOrder[DataSource.Config.I_Order] = order;
        DataSource.Config.I_Order++;
        return ID;
    }
    public Order Read(int I)
    {
        if (I < 0) // if the index is out of the specified range throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrder[I];
    }
    public void Update(int ID, Order order)
    {
        int I = DataSource.searchOrder(ID);
        if(I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.arrOrder[I] = order;
            DataSource.arrOrder[I].ID = ID;
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
            for(int J = I;J < DataSource.arrOrder.Length - 1 ;J++) // push all the orders beyond the spesified order one step back
            {
                DataSource.arrOrder[J] = DataSource.arrOrder[J+1];
            }
            DataSource.Config.I_Order = DataSource.Config.I_Order--;
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
        return DataSource.arrOrder[DataSource.searchOrder(ID)];
    }
    public int Order_Length()
    {
        return DataSource.Config.I_Order;
    }

}
