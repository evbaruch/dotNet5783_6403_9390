namespace Dal;
using DO;
public class DalOrderItem
{
    public (int, int) Create(int ID,int ProductID, int OrderID, double Price, string Amount)
    {
        int IdOrder = DataSource.Config.get_ID_Order;
        int IdProduct = DataSource.Config.get_ID_Product;
        OrderItem orderItem = new OrderItem(ID,ProductID, OrderID, Price, Amount);
        return (IdProduct, IdOrder);
    }
    public OrderItem Read(int I)
    {
        if (I == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrderItem[I];
    }
    public void Update(int ProductID, int OrderID, OrderItem orderItem)
    {
        int I = DataSource.searchOrderItem(ProductID, OrderID);
        if (I!= -1)
        {
            DataSource.arrOrderItem[I] = orderItem;
            DataSource.arrOrderItem[I].OrderID = OrderID;
            DataSource.arrOrderItem[I].OrderID = ProductID;
        }
        else
        {
            throw new IndexOutOfRangeException("object doesn't exist - Update");
        }
    }
    public void Delete(int ProductID, int OrderID)
    {
        int I = DataSource.searchOrderItem(ProductID, OrderID);
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
    public OrderItem ReadID(int ProductID, int OrderID)
    {
        int I = DataSource.searchOrderItem(ProductID, OrderID);
        if (I == -1)
        {
            throw new IndexOutOfRangeException("Read range Error");
        }
        return DataSource.arrOrderItem[I];
    }

}