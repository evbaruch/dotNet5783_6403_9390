using DalApi;
using DO;

namespace Dal;


public class DalOrder : IOrder
{
    //public int Create(T entity);
    //public T Read(int ID);
    //public void Update(T entity);
    //public void Delete(T entity);
    //public IEnumerable<T> ReadAll();


    public DalOrder()
    {
        DataSource.s_Initialize();
    }
    public int Create(Order order)
    {
        //
        order.ID = DataSource.Config.get_ID_Order;
        DataSource.listOrder.Add(order);
        return order.ID;
    }
    public Order Read(Order order)
    {
        int I = DataSource.searchOrder(order.ID);
        if (I != -1) // if the ID exist replas the details else throw an Error
        {

            return DataSource.listOrder[I];

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

    public IEnumerable<Order> ReadAll()
    {
        return DataSource.listOrder;
    }

    //public Order ReadID(int ID)
    //{
    //    if (DataSource.searchOrder(ID) == -1) // if the ID exist return the details else throw an Error
    //    {
    //        throw new IndexOutOfRangeException("Read range Error");
    //    }
    //    return DataSource.listOrder[DataSource.searchOrder(ID)];
    //}
    public int Order_Length()
    {
        return DataSource.listOrder.Count;
    }

}
