using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using System.Data;
using IOrder = BlApi.IOrder;

namespace BlImplementation;

internal class Order : IOrder
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.OrderForList> OrderListRequest()
    {
        IEnumerable<DO.Order> orders = Dal.order.ReadAll().ToList();
        IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.ReadAll().ToList();
        IEnumerable<BO.OrderForList> OrderForList = new List<BO.OrderForList>();
        foreach (var item in orders) // go over the products and get all the data from DO 
        {
            BO.OrderForList temp = new()
            {
                ID =  item.ID,
                CustomerName = item.CustomerName,
            };
            foreach (var inItem in orderItems) // go over the orderItem and find all the order Item and modify accordingly the amount and total price
            {
                if (inItem.OrderID == item.ID)
                {
                    temp.AmountOfItems++;
                    temp.TotalPrice += inItem.Price;
                }
            }
            if (Dal.order.Read(new() { ID = (int)item.ID }).ShipDate == DateTime.MinValue) // if the value of the shiping is not define it's only ordered
            {
                temp.Status = (BO.Enums.OrderStatus.ordered);
            }
            else if (Dal.order.Read(new() { ID = (int)item.ID }).ShipDate != DateTime.MinValue && Dal.order.Read(new() { ID = (int)item.ID }).DeliveryDate == DateTime.MinValue)
            { // if the value of the shiping is define but the time of the delivery is't it's only shiped
                temp.Status = (BO.Enums.OrderStatus.shiped);
            }
            else // else (both define)
            {
                temp.Status = (BO.Enums.OrderStatus.delivered);
            }
            OrderForList.Append(temp);
        }
        return OrderForList;
    }

    public BO.Order OrderDetailsRequest(int orderID)
    {
        try
        {
            if(orderID > 0) // green line (;
            {
                DO.Order orderItem = Dal.order.Read(new() { ID = orderID });
                IEnumerable <DO.OrderItem> orderItems = Dal.orderItem.ReadAll().ToList();
                BO.Order order = new()
                {
                    ID = orderID,
                    CustomerName = orderItem.CustomerName,
                    CustomerEmail = orderItem.CustomerEmail,
                    CustomerAdress = orderItem.CstomerAddress,
                    OrderDate = orderItem.OrderDate,
                    ShipDate = orderItem.ShipDate,
                    DeliveryrDate = orderItem.DeliveryDate,
                };
                for (int i = 0; i < orderItems.Count(); i++) // go over all the Order item and collect the details
                {
                    if (orderItem.ID == orderItems.ElementAt(i).ID)
                    {
                        order.Items.Add(new()
                        {
                            ID = orderItems.ElementAt(i).ID,
                            Name = Dal.product.Read(new() { ID = orderID }).Name,
                            ProductID = orderItems.ElementAt(i).ProductID,
                            Price = orderItems.ElementAt(i).Price,
                            Amount = orderItems.ElementAt(i).Amount,
                        }
                        ) ;
                        order.TotalPrice += orderItems.ElementAt(i).Price;
                    }
                }
                if (order.ShipDate == DateTime.MinValue) // if the value of the shiping is not define it's only ordered
                {
                    order.Status = (BO.Enums.OrderStatus.ordered);
                }
                else if (order.ShipDate != DateTime.MinValue && order.DeliveryrDate == DateTime.MinValue) 
                {// if the value of the shiping is define but the time of the delivery is't it's only shiped
                    order.Status = (BO.Enums.OrderStatus.shiped);
                }
                else // else (both define)
                {
                    order.Status = (BO.Enums.OrderStatus.delivered);
                }
                return order;
            }
            else
            {
                throw new DataNotFoundException("BlImplementation->Order->OrderDetails = order don't exist - BlIm");
            }
        }
        catch(Exception excption)
        {
            throw excption;
        }
    }

    public BO.Order OrderShippingUpdate(int orderID)
    {
        try
        {
            DO.Order updateOrder = Dal.order.Read(new() { ID = orderID });
            if (orderID > 0 && updateOrder.ShipDate == DateTime.MinValue) // only if the order hadn't been shiped update the shiping date to now
            {
                updateOrder.ShipDate = DateTime.Now;
                BO.Order order = OrderDetailsRequest(orderID);
                order.ShipDate = DateTime.Now;
                Dal.order.Update(updateOrder);
                return order;
            }
            else
            {
                throw new DataNotFoundException("BlImplementation->Order->OrderShippingUpdate = order don't exist or been shiped - BlIm");
            }
        }
        catch (Exception excption)
        {
            throw excption;
        }
    }

    public BO.Order UpdateDeliveryOrder(int orderID)
    {
        try
        {
            DO.Order updateOrder = Dal.order.Read(new() { ID = orderID });
            if (orderID > 0 && updateOrder.ShipDate != DateTime.MinValue && updateOrder.DeliveryDate == DateTime.MinValue) // only if the order hadn't been deliverd update the delivery date to now
            {
                updateOrder.DeliveryDate = DateTime.Now;
                BO.Order order = OrderDetailsRequest(orderID);
                order.DeliveryrDate = DateTime.Now;
                Dal.order.Update(updateOrder);
                return order;
            }
            else
            {
                throw new DataNotFoundException("BlImplementation->Order->UpdateDeliveryOrde = order don't exist or been delivered - BlIm");
            }
        }
        catch(Exception excption) 
        {
            throw excption;
        }
    }

    public OrderTracking OrderTracking(int orderID)
    {
        try
        {
            DO.Order trackedOrder = Dal.order.Read(new() { ID = orderID });
            if(orderID > 0)
            {
                Tuple<DateTime, BO.Enums.OrderStatus> a;
                BO.OrderTracking tracking = new() {ID = orderID };
                if (trackedOrder.ShipDate == DateTime.MinValue) // if the value of the shiping is not define it's only ordered
                {
                    tracking.Status = ((BO.Enums.productsCategory?)BO.Enums.OrderStatus.ordered);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus> ((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                else if (trackedOrder.ShipDate != DateTime.MinValue && trackedOrder.DeliveryDate == DateTime.MinValue)
                { // if the value of the shiping is define but the time of the delivery is't it's only shiped
                    tracking.Status = ((BO.Enums.productsCategory?)BO.Enums.OrderStatus.shiped);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus>((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                else // else (both define)
                {
                    tracking.Status = ((BO.Enums.productsCategory?)BO.Enums.OrderStatus.delivered);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus>((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }

                tracking.OrderStatuses.Add(a);
                return tracking;
            }
            else
            {
                throw new DataNotFoundException("BlImplementation->Order->OrderTracking = order don't exist - BlIm");
            }
        }
        catch(Exception excption)
        {
            throw excption;
        }
    }

    public BO.Order OrderUpdate(int orderID, int productID ,int plus_minus)
    {
        try
        {
            DO.Order orderToUpdate = Dal.order.Read(new() { ID = orderID });
            if (productID > 0 && orderID > 0)
            {
                BO.Order orderUpdate = OrderDetailsRequest(orderID);
                foreach (var item in orderUpdate.Items) // go over the order item and modify the amount 
                {
                    if (item.ID == orderID)
                    {
                        item.Amount += plus_minus;
                    }
                }
                return orderUpdate;
            }
            else
            {
                throw new DataNotFoundException("BlImplementation->Order->OrderUpdate = order don't exist - BlIm");
            }
        }
        catch(Exception exception)
        {
            throw exception;
        }
    }

    
}
