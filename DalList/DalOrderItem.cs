using DalApi;
using DO;

namespace Dal;


public class DalOrderItem : IOrderItem
{
    public int Create(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.get_ID_OrderItem;
        DataSource.listOrderItem.Add(orderItem);
        return orderItem.ID;
    }
    public OrderItem Read(OrderItem orderItem)
    {
        int I = DataSource.searchOrder(orderItem.ID);
        if (I != -1) // if the ID exist return the details else throw an Error
        {
            return (OrderItem)DataSource.listOrderItem[I];
        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }
    public void Update(OrderItem orderItem)
    {
        int I = DataSource.searchOrderItem(orderItem.ID);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listOrderItem[I] = orderItem;
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }
    public void Delete(OrderItem orderItem)
    {
        int I = DataSource.searchOrderItem(orderItem.ID);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            DataSource.listOrderItem.Remove(orderItem);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }
    public IEnumerable<OrderItem?> ReadAll(Func<OrderItem?, bool>? func)
    {
        return DataSource.listOrderItem;
    }
}