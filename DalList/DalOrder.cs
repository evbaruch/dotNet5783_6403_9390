namespace Dal;
using DO;
using System.Globalization;
using System.Runtime.InteropServices;

public class DalOrder
{
    public int Create(string customerName, string customerEmail, string customerAddress, DateTime orderDate, DateTime shipDate, DateTime deliveryDate)
    {
        int ID = DataSource.Config.get_ID_Order;
        Order order = new Order(ID, customerName, customerEmail, customerAddress, orderDate, shipDate, deliveryDate);
        return ID;
    }
    public Order Read(int I)
    {
        if(I == -1 || I > 100 )
        {
            throw new Exception("Read range Error");
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
            throw new Exception("object doesn't exist - Update");
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
            throw new Exception("Delete range Error ");
        }
    }

}
