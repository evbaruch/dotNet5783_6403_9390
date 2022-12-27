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
        Order? isNULL = ReadObject(
            a => a?.ID == order.ID
                            );
        if (isNULL?.ID != -1) // if the ID exist return the details else throw an Error
        {

            return (Order)isNULL;

        }
        else
        {
            throw new IDWhoException("Delete range Error ");
        }
    }
    public void Update(Order order)
    {
        Order? isNULL = ReadObject(
            a => a?.ID == order.ID
                            );
        int I = DataSource.listOrder.IndexOf(isNULL);//DataSource.searchOrder(order.ID);
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
       Order? isNULL=  ReadObject(
            a => a?.ID == order.ID
                            );
        if ( isNULL?.ID != -1) // if the ID exist delete the details else throw an Error
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
            finalResult = (List<Order?>)(from Order in DataSource.listOrder 
                                        where func(Order)
                                        select Order);
            //foreach (var item in DataSource.listOrder)
            //{
            //    if (func(item))
            //    {
            //        finalResult.Add(item);
            //    }
            //}
            return finalResult;
        }
        else
        {
            finalResult = DataSource.listOrder;
            return finalResult;
        }
    }

    public Order ReadObject (Func<Order?, bool>? func)
    {

        if (func != null)
        {
            var order = DataSource.listOrder.Find(x => func(x));
            if (order != null)
            {
                return (Order)order;
            }
        }
        //foreach (var item in DataSource.listOrder)
        //{
        //    if(func(item))
        //    {
        //        return (Order)item;
        //    }
        //}
        return new()
        {
            ID = -1,
            CstomerAddress = null,
            CustomerEmail = null,
            CustomerName = null,
            DeliveryDate = null,
            OrderDate = null,
            ShipDate = null
        };
    }
}
