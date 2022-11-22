using DalApi;
using DO;

namespace Dal;


public class DalOrderItem
{
    public DalOrderItem()
    {
        DataSource.s_Initialize();
    }
    public int Create(int ProductID, int OrderID, double Price, int Amount)
    {
        int ID = DataSource.Config.get_ID_OrderItem;
        int IdOrder = DataSource.Config.get_ID_Order;
        int IdProduct = DataSource.Config.get_ID_Product;
        OrderItem orderItem = new OrderItem(ID,ProductID, OrderID, Price, Amount);
        DataSource.listOrderItem.Add(orderItem);
        return ID;
    }
    public OrderItem Read(int I)
    {
        if (I < 0) // if the index exist return the details else throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listOrderItem[I];
    }
    public void Update(int ID, OrderItem orderItem)
    {
        int I = DataSource.searchOrderItem(ID);
        if (I!= -1)// if the ID exist update the details else throw an Error
        {
            DataSource.listOrderItem[I] = orderItem;
            //DataSource.arrOrderItem[I].OrderID = OrderID;
            //DataSource.arrOrderItem[I].OrderID = ProductID;
        }
        else
        {
            throw new IndexOutOfRangeException("object doesn't exist - Update");
        }
    }
    public void Delete(int ID)
    {
        int I = DataSource.searchOrderItem(ID);
        if (I!= -1)// if the index exist delete the details else throw an Error
        {
                DataSource.listOrderItem.Remove(ReadID(ID));
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error ");
        }
    }
    public OrderItem ReadID(int ID)
    {
        int I = DataSource.searchOrderItem(ID);
        if (I == -1)// if the ID exist return the details else throw an Error
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.listOrderItem[I];
    }
    public OrderItem ReadItem(int OrderID)
    {// collect all the orderItems with a specific ID into a list and return it
        int I = 0;
        for (int i = 0; i < DataSource.listOrderItem.Count; i++)
        {
            if (DataSource.listOrderItem[i].OrderID == OrderID )
            {
                I++;
            }
        }
        OrderItem[] specificOrder = new OrderItem[I];
        for (int i = 0, k = 0; i < DataSource.listOrderItem.Count; i++)
        {
            if (DataSource.listOrderItem[i].OrderID == OrderID)
            {
                specificOrder[k] = DataSource.listOrderItem[i];
                k++;
            }
        }
        return specificOrder;
    }
    public int OrderItem_Length()
    {
        return DataSource.listOrderItem.Count;
    }
}