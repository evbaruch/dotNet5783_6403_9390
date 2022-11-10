namespace Dal;
using DO;
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
        return ID;
    }
    public Order Read(int I)
    {
        if(I == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrder[I];
    }
    public void Update(int ID, Order order)
    {
        int I = DataSource.searchOrder(ID);
        if(I != -1)
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
        if(I!= -1)
        {
            for(int J = I;J < DataSource.arrOrder.Length - 1 ;J++)
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
        if (DataSource.searchOrder(ID) == -1)
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
