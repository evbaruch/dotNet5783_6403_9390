namespace Dal;
using DO;
public class DalOrderItem
{
    public DalOrderItem()
    {
        DataSource.s_Initialize();
    }
    public int Create(int ID,int ProductID, int OrderID, double Price, int Amount)
    {
        ID = DataSource.Config.get_ID_OrderItem;
        int IdOrder = DataSource.Config.get_ID_Order;
        int IdProduct = DataSource.Config.get_ID_Product;
        OrderItem orderItem = new OrderItem(ID,ProductID, OrderID, Price, Amount);
        DataSource.arrOrderItem[DataSource.Config.I_OrderItem] = orderItem;
        return ID;
    }
    public OrderItem Read(int I)
    {
        if (I == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrderItem[I];
    }
    public void Update(int ID, OrderItem orderItem)
    {
        int I = DataSource.searchOrderItem(ID);
        if (I!= -1)
        {
            DataSource.arrOrderItem[I] = orderItem;
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
        if (I!= -1)
        {
            for (int J = I; J < DataSource.arrOrder.Length - 1; J++)
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
    public OrderItem ReadID(int ID)
    {
        int I = DataSource.searchOrderItem(ID);
        if (I == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrderItem[I];
    }
    public OrderItem[] ReadItem(int OrderID)
    {
        int I = 0;
        for (int i = 0; i < DataSource.arrOrderItem.Length; i++)
        {
            if (DataSource.arrOrderItem[i].OrderID == OrderID )
            {
                I++;
            }
        }
        OrderItem[] specificOrder = new OrderItem[I];
        for (int i = 0; i < DataSource.arrOrderItem.Length; i++)
        {
            if (DataSource.arrOrderItem[i].OrderID == OrderID)
            {
                specificOrder[i] = DataSource.arrOrderItem[i];
            }
        }
        return DataSource.arrOrderItem;
    }
    public int OrderItem_Length()
    {
        return DataSource.Config.I_OrderItem;
    }
}