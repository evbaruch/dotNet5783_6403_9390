using BO;
using Dal;
using DalApi;
using IOrder = BlApi.IOrder;

namespace BlImplementation;

internal class Order : IOrder
{
    private IDal Dal = new DalList();

    public BO.Order OrderDetailsRequest(int orderID)
    {
       
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Order> OrderListRequest()
    {
        IEnumerable<DO. Order> orders = Dal.order.ReadAll().ToList();
        IEnumerable<DO.OrderItem> orderItems = Dal.orderItem.ReadAll().ToList();
        IEnumerable<BO.OrderForList> OrderForList = new List<BO.OrderForList>();
        foreach (var item in orders) // go over the products and get all the data from DO 
        {
            BO.OrderForList temp = new() 
                {
                ID =  item.ID,
                CustomerName = item.CustomerName,
                //AmountOfItems =  ,
                TotalPrice = item.Price
                };
            if (Dal.order.Read(new() { ID = (int)item.ID }).ShipDate == DateTime.MinValue )
            {
                temp.Status = (BO.Enums.OrderStatus.ordered);
            }
            else if(Dal.order.Read(new() { ID = (int)item.ID }).ShipDate != DateTime.MinValue && Dal.order.Read(new() { ID = (int)item.ID }).DeliveryDate == DateTime.MinValue)
            {
                temp.Status = (BO.Enums.OrderStatus.shiped);
            }
            else
            {
                temp.Status = (BO.Enums.OrderStatus.delivered);
            }
            OrderForList.Append(temp);
        }
        return OrderForList;
    }

    public BO.Order OrderShippingUpdate(int orderID)
    {
        throw new NotImplementedException();
    }

    public OrderTracking OrderTracking(int id)
    {
        throw new NotImplementedException();
    }

    public void OrderUpdate(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateDeliveryOrder(int id)
    {
        throw new NotImplementedException();
    }
}
