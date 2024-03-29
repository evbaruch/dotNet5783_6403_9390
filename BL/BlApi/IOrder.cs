﻿using BO;
namespace BlApi;
/// <summary>
/// Operation on the Order
/// </summary>
public interface IOrder
{
    /// <summary>
    /// Order list request (admin screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> OrderListRequest();
    /// <summary>
    /// Order details request (for manager screen and buyer screen)
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public Order OrderDetailsRequest(int orderID);
    /// <summary>
    /// Order Shipping Update (Manager Order Management Screen)
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    public Order OrderShippingUpdate(int orderID);
    /// <summary>
    /// request to update the state of the order delivery and alter it
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateDeliveryOrder(int id);
    /// <summary>
    /// request for the tracking details
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking OrderTracking(int id);
    /// <summary>
    /// request to update the order (admin)
    /// </summary>
    /// <param name="id"></param>
    public Order OrderUpdate(int OrderID, int productID, int plus_minus);

    public int? PriorityOrder(Func<DO.Order?, bool>? filter = null);

    public bool IsUserNameUnique(string userName);

    public BO.OrderForList OrderForList(int orderID);

}
