using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalOrder : IOrder
{
    const string orderPath = "Order";
    static XElement config = XmlTools.LoadConfig();

    public int Create(Order order)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        order.OrderID = int.Parse(config.Element("OrderID")!.Value) + 1;
        XmlTools.SaveConfigXElement("OrderID", order.OrderID);
        listOrder.Add(order);
        XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
        return order.OrderID;
    }

    public Order Read(Order order)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        Order? isNULL = ReadObject(
            a => a?.OrderID == order.OrderID
                            );
        if (isNULL?.OrderID != -1) // if the ID exist return the details else throw an Error
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
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        Order? isNULL = ReadObject(
             a => a?.OrderID == order.OrderID
                             );
        int I = listOrder.IndexOf(isNULL);//DataSource.searchOrder(order.ID);
        if (I != -1) // if the ID exist update the details else throw an Error
        {
            listOrder[I] = order;
            XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
        }
        else
        {
            throw new IDWhoException("object doesn't exist - Update");
        }
    }

    public void Delete(Order order)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        Order? isNULL = ReadObject(
           a => a?.OrderID == order.OrderID
                           );
        if (isNULL?.OrderID != -1) // if the ID exist delete the details else throw an Error
        {
            listOrder.Remove(isNULL);
            XmlTools.SaveListToXMLSerializer(listOrder, orderPath);
        }
        else
        {
            throw new IndexOutOfRangeException("Delete range Error");
        }
    }

    public IEnumerable<Order?> ReadAll(Func<Order?, bool>? func = null)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);
        List<Order?> finalResult = new List<Order?>();
        if (func != null)
        {
            finalResult = (from Order in listOrder
                                where func(Order)
                                select Order).ToList();

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
            finalResult = listOrder;
            return finalResult;
        }
    }

    public Order ReadObject(Func<Order?, bool>? func)
    {
        List<DO.Order?> listOrder = XmlTools.LoadListFromXMLSerializer<DO.Order>(orderPath);

        if (func != null)
        {
            var order = listOrder.Find(x => func(x));
            if (order != null)
            {
                return (Order)order;
            }
        }

        return new()
        {
            OrderID = -1,
            CstomerAddress = null,
            CustomerEmail = null,
            CustomerName = null,
            DeliveryDate = null,
            OrderDate = null,
            ShipDate = null
        };
    }
}
