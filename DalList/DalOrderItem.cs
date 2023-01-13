using DalApi;
using DO;
using System;
using System.Net.Http.Headers;

namespace Dal;


public class DalOrderItem : IOrderItem
{
    public int Create(OrderItem orderItem)
    {
        orderItem.OrderItemID = DataSource.Config.get_ID_OrderItem;
        DataSource.listOrderItem.Add(orderItem);
        return orderItem.OrderItemID;
    }
    public OrderItem Read(OrderItem orderItem)
    {
        OrderItem? isNULL = ReadObject(
            a => a?.OrderItemID == orderItem.OrderItemID
                            );
        int I = DataSource.listOrderItem.IndexOf(isNULL);
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
        OrderItem? isNULL = ReadObject(
            a => a?.OrderItemID == orderItem.OrderItemID
                            );
        int I = DataSource.listOrderItem.IndexOf(isNULL);
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
        OrderItem? isNULL = ReadObject(
            a => a?.OrderItemID == orderItem.OrderItemID
                            );
        if (isNULL?.OrderItemID != -1) // if the ID exist delete the details else throw an Error
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

        if (func != null)
        {
            var finalResult = from item in DataSource.listOrderItem
                              where func(item)
                              select item;

            return finalResult;

        }
        else
        {
            List<OrderItem?> finalResult = new List<OrderItem?>();
            finalResult = DataSource.listOrderItem;
            return finalResult;
        }
    }

    public OrderItem ReadObject(Func<OrderItem?, bool>? func)
    {
        if (func != null)
        {
            var orderItem = DataSource.listOrderItem.Find(x => func(x));
            if (orderItem != null)
            {
                return (OrderItem)orderItem;
            }
        }

        return new()
        {
            OrderItemID = -1,
            ProductID = -1,
            OrderID = -1,
            Price = null,
            Amount = null,
        };
    }
}