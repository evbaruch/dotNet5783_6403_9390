using DalApi;
using DO;

namespace Dal;


public class DalOrder : IOrder
{
    public int Create(Order order)
    {      
        order.ID = DataSource.Config.get_ID_Order;
        DataSource.listOrder.Add(order);
        return order.ID;
    }
    public Order Read(Order order)
    {
            int I = DataSource.searchOrder(order.ID);
            if (I != -1) // if the ID exist return the details else throw an Error
            {

            return (Order)DataSource.listOrder[I];

            }
            else
            {
                throw new IDWhoException("Delete range Error ");
            }
    }
    public void Update(Order order)
    {
        int I = DataSource.searchOrder(order.ID);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            DataSource.listOrder[I] = order;
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }
    public void Delete(Order order)
    {
        int I = DataSource.searchOrder(order.ID);
        if (I != -1) // if the ID exist delete the details else throw an Error
        {
            DataSource.listOrder.Remove(order);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }
    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? func)
    {   
        List<Order?> finalResult = new List<Order?>();
        if (func != null)
        {
            foreach (var item in DataSource.listOrder)
            {
                if (func(item))
                {
                    finalResult.Add(item);
                }
            }
            return finalResult;
        }
        else
        {
            finalResult = DataSource.listOrder;
            return finalResult;
        }
    }

    public IEnumerable<Order?> Read(Func<Order?, bool>? func)
    {
         =>;
    }
}
