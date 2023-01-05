using BlApi;
using BO;
using DalApi;
using DO;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using IOrder = BlApi.IOrder;

namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> OrderListRequest()
    {
        IEnumerable<DO.Order?> orders = dal.order.ReadAll().ToList();
        IEnumerable<DO.OrderItem?> orderItems = dal.orderItem.ReadAll().ToList();
        List<BO.OrderForList> OrderForList = new List<BO.OrderForList>();

        OrderForList = (from item in orders
                        let order = dal.order.Read(new() { ID = (int)(item?.ID ?? -1) })
                        let status = order.ShipDate == null ?
                                     BO.Enums.OrderStatus.ordered :
                                     order.DeliveryDate == null ?
                                     BO.Enums.OrderStatus.shiped :
                                     BO.Enums.OrderStatus.delivered
                        select new OrderForList
                        {
                            AmountOfItems = orderItems.Sum(x => x?.OrderID == item?.ID ? x?.Amount : 0),
                            TotalPrice = orderItems.Where(x => x?.OrderID == item?.ID).Sum(a => a?.Price*a?.Amount),
                            CustomerName = item?.CustomerName,
                            ID = (int)(item?.ID ?? -1),
                            Status = status
                        }).ToList();
        OrderForList = OrderForList.OrderBy(x => x.AmountOfItems).ToList();
        

        //foreach (var item in orders) // go over the products and get all the data from DO 
        //{
        //    BO.OrderForList temp = new()
        //    {
        //        ID =  (int)(item?.ID),
        //        CustomerName = item?.CustomerName,
        //    };
        //    temp.AmountOfItems = 0;
        //    temp.TotalPrice = 0;
        //    foreach (var inItem in orderItems) // go over the orderItem and find all the order Item and modify accordingly the amount and total price
        //    {
        //        if (inItem?.OrderID == item?.ID)
        //        {
        //            temp.AmountOfItems++;
        //            temp.TotalPrice += inItem?.Price;
        //        }
        //    }
        //    if (dal.order.Read(new() { ID = (int)item?.ID }).ShipDate == null) // if the value of the shiping is not define it's only ordered
        //    {
        //        temp.Status = (BO.Enums.OrderStatus.ordered);
        //    }
        //    else if (dal.order.Read(new() { ID = (int)(item?.ID) }).ShipDate != null && dal.order.Read(new() { ID = (int)item?.ID }).DeliveryDate == null)
        //    { // if the value of the shiping is define but the time of the delivery is't it's only shiped
        //        temp.Status = (BO.Enums.OrderStatus.shiped);
        //    }
        //    else // else (both define)
        //    {
        //        temp.Status = (BO.Enums.OrderStatus.delivered);
        //    }
        //    OrderForList.Add(temp);
        //}
        return OrderForList;
    }

    public BO.Order OrderDetailsRequest(int orderID) 
    {
        try
        {
            if(orderID > 0) // green line (;
            {
                DO.Order orderItem = dal.order.Read(new() { ID = orderID });
                IEnumerable <DO.OrderItem?> orderItems = dal.orderItem.ReadAll().ToList();
                BO.Order order = new()
                {
                    ID = orderID,
                    CustomerName = orderItem.CustomerName,
                    CustomerEmail = orderItem.CustomerEmail,
                    CustomerAddress = orderItem.CstomerAddress,
                    OrderDate = orderItem.OrderDate,
                    ShipDate = orderItem.ShipDate,
                    DeliveryrDate = orderItem.DeliveryDate,
                };
                order.TotalPrice = 0;
                order.Items = new List<BO.OrderItem>();
                for (int i = 0,j=0; i < orderItems.Count(); i++) // go over all the Order item and collect the details
                {
                    if (orderItem.ID == orderItems.ElementAt(i)?.OrderID)
                    {
                        order.Items.Add(new() { });
                        order.Items[j].ID = (int)(orderItems.ElementAt(i)?.OrderID);
                        order.Items[j].ProductID = (int)(orderItems.ElementAt(i)?.ProductID);
                        order.Items[j].Name = dal.product.Read(new() { ID = order.Items[j].ProductID }).Name;
                        order.Items[j].Price = orderItems.ElementAt(i)?.Price;
                        order.Items[j].Amount = orderItems.ElementAt(i)?.Amount;
                        order.Items[j].TotalPrice = orderItems.ElementAt(i)?.Price * orderItems.ElementAt(i)?.Amount;
                        order.TotalPrice += orderItems.ElementAt(i)?.Price * orderItems.ElementAt(i)?.Amount;
                        j++;
                    }
                }
                if (order.ShipDate == null) // if the value of the shiping is not define it's only ordered
                {
                    order.Status = (BO.Enums.OrderStatus.ordered);
                }
                else if (order.ShipDate != null && order.DeliveryrDate == null) 
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
            DO.Order updateOrder = dal.order.Read(new() { ID = orderID });
            if (orderID > 0 && updateOrder.ShipDate == null) // only if the order hadn't been shiped update the shiping date to now
            {
                updateOrder.ShipDate = DateTime.Now;
                BO.Order order = OrderDetailsRequest(orderID);
                order.ShipDate = DateTime.Now;
                dal.order.Update(updateOrder);
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
            DO.Order updateOrder = dal.order.Read(new() { ID = orderID });
            if (orderID > 0 && updateOrder.ShipDate != null && updateOrder.DeliveryDate == null) // only if the order hadn't been deliverd update the delivery date to now
            {
                updateOrder.DeliveryDate = DateTime.Now;
                BO.Order order = OrderDetailsRequest(orderID);
                order.DeliveryrDate = DateTime.Now;
                dal.order.Update(updateOrder);
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
            DO.Order trackedOrder = dal.order.Read(new() { ID = orderID });
            if(orderID > 0)
            {
                Tuple<DateTime, BO.Enums.OrderStatus> a;
                BO.OrderTracking tracking = new() {ID = orderID };
                if (trackedOrder.ShipDate == null) // if the value of the shiping is not define it's only ordered
                {
                    tracking.Status = (BO.Enums.OrderStatus.ordered);
                    a = new Tuple<DateTime, BO.Enums.OrderStatus> ((DateTime)trackedOrder.OrderDate, (BO.Enums.OrderStatus)tracking.Status);
                }
                else if (trackedOrder.ShipDate != null && trackedOrder.DeliveryDate == null)
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
            BO.Order order = OrderDetailsRequest(orderID);
            List <DO.OrderItem?>orderToUpdate = dal.orderItem.ReadAll().ToList();
            if (productID > 0 && orderID > 0)
            {
                
                BO.Order orderUpdate = OrderDetailsRequest(orderID);
                var itemToUpdate = (from item in orderUpdate.Items
                                    where item.ProductID == productID
                                    select item).FirstOrDefault();
                if (itemToUpdate != null)
                {
                    var idFind = (from o in orderToUpdate
                                  where o?.OrderID == orderID && o?.ProductID == itemToUpdate.ProductID
                                  select o).FirstOrDefault();
                    if (itemToUpdate.Amount + plus_minus == 0)
                    {
                        orderUpdate.TotalPrice += itemToUpdate.Price * plus_minus;
                        orderToUpdate.Remove(idFind);

                        dal.orderItem.Delete(new() { ID = (int)idFind?.ID, ProductID = itemToUpdate.ProductID, Amount = itemToUpdate.Amount, Price = itemToUpdate.Price, OrderID = orderID });
                        if(orderUpdate.TotalPrice <= 0)
                        {
                             dal.order.Delete(new() { ID = orderID });
                        }
                    }
                    else
                    {
                        orderUpdate.TotalPrice += itemToUpdate.Price * plus_minus;
                        itemToUpdate.Amount += plus_minus;

                        dal.orderItem.Update(new() { ID = (int)idFind?.ID, ProductID = itemToUpdate.ProductID, Amount = itemToUpdate.Amount, Price = itemToUpdate.Price, OrderID = orderID });
                    }
                }

                // in my opinion the linq here is not efficient but it's the only way we manage to find
                //foreach (var item in orderUpdate.Items) // go over the order item and modify the amount 
                //{
                //    if (item.ProductID == productID)
                //    {
                //        if (item.Amount + plus_minus <= 0)
                //        {
                //            orderUpdate.TotalPrice += item.Price*item.Amount;
                //            orderToUpdate.Remove(new() {ID = (int)item.ID});
                //        }
                //        else
                //        {
                //            orderUpdate.TotalPrice += item.Price*plus_minus;
                //            item.Amount += plus_minus;
                //        }
                //        foreach (var idFind in orderToUpdate)
                //        {
                //            if(idFind?.OrderID == orderID && idFind?.ProductID == item.ProductID)
                //            {
                //                item.ID = (int)(idFind?.ID);
                //            }
                //        }
                //        dal.orderItem.Update(new() {ID = (int)item.ID, ProductID = item.ProductID , Amount = item.Amount, Price = item.Price , OrderID = orderID });
                //    }
                //}
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
