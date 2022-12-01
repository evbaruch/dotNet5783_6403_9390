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
        List<BO.OrderForList> OrderForList = new List<BO.OrderForList>();
        foreach (var item in orders) // go over the products and get all the data from DO 
        {
            BO.OrderForList temp = new()
            {
                ID =  item.ID,
                CustomerName = item.CustomerName,
            };
            temp.AmountOfItems = 0;
            temp.TotalPrice = 0;
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
            OrderForList.Add(temp);
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
                order.TotalPrice = 0;
                order.Items = new List<BO.OrderItem>();
                for (int i = 0,j=0; i < orderItems.Count(); i++) // go over all the Order item and collect the details
                {
                    if (orderItem.ID == orderItems.ElementAt(i).OrderID)
                    {
                        order.Items.Add(new() { });
                        order.Items[j].ID = orderItems.ElementAt(i).OrderID;
                        order.Items[j].ProductID = orderItems.ElementAt(i).ProductID;
                        order.Items[j].Name = Dal.product.Read(new() { ID = order.Items[j].ProductID }).Name;
                        order.Items[j].Price = orderItems.ElementAt(i).Price;
                        order.Items[j].Amount = orderItems.ElementAt(i).Amount;
                        order.TotalPrice += orderItems.ElementAt(i).Price * orderItems.ElementAt(i).Amount;
                        j++;
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
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Order->OrderDetails = order don't exist - BlIm"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (Exception excption)
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
                throw new DataNotFoundException(" ",new Exception("BlImplementation->Order->OrderShippingUpdate = order don't exist or been shiped - BlIm"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
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
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Order->UpdateDeliveryOrde = order don't exist or been delivered - BlIm"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (DataNotFoundException excption) 
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
                    tracking.Status = (BO.Enums.OrderStatus.ordered);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus> ((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                else if (trackedOrder.ShipDate != DateTime.MinValue && trackedOrder.DeliveryDate == DateTime.MinValue)
                { // if the value of the shiping is define but the time of the delivery is't it's only shiped
                    tracking.Status = (BO.Enums.OrderStatus.shiped);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus>((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                else // else (both define)
                {
                    tracking.Status = (BO.Enums.OrderStatus.delivered);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus>((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                tracking.OrderStatuses = new() { };
                tracking.OrderStatuses.Add(a);
                return tracking;
            }
            else
            {
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Order->OrderTracking = order don't exist - BlIm"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (DataNotFoundException excption)
        {
            throw excption;
        }
    }

    public BO.Order OrderUpdate(int orderID, int productID ,int plus_minus)
    {
        try
        {
            List <DO.OrderItem >orderToUpdate = Dal.orderItem.ReadAll().ToList();
            if (productID > 0 && orderID > 0)
            {
                BO.Order orderUpdate = OrderDetailsRequest(orderID);
                foreach (var item in orderUpdate.Items) // go over the order item and modify the amount 
                {
                    if (item.ProductID == productID)
                    {
                        if (item.Amount + plus_minus <= 0)
                        {
                            orderUpdate.TotalPrice += item.Price*item.Amount;
                            orderToUpdate.Remove(new() {ID = (int)item.ID});
                        }
                        else
                        {
                            orderUpdate.TotalPrice += item.Price*plus_minus;
                            item.Amount += plus_minus;
                            // לא מצליח לעדכן את השכבת נתונים
                        }
                        foreach (var idFind in orderToUpdate)
                        {
                            if(idFind.OrderID == orderID && idFind.ProductID == item.ProductID)
                            {
                                item.ID = idFind.ID;
                            }
                        }
                        Dal.orderItem.Update(new() {ID = (int)item.ID, ProductID = item.ProductID , Amount = item.Amount, Price = item.Price , OrderID = orderID });
                    }
                }
                return orderUpdate;
            }
            else
            {
                throw new DataNotFoundException(" ", new Exception("BlImplementation->Order->OrderUpdate = order don't exist - BlIm"));
            }
        }
        catch (DO.IDWhoException)
        {
            throw new DataNotFoundException(" ", new Exception("IDWhoException was throw"));
        }
        catch (DO.ISawYouAlreadyException)
        {
            throw new IncorrectDataException(" ", new Exception("ISawYouAlreadyException was throw"));
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    
}
