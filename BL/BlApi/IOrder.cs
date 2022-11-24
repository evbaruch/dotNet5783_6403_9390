using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// 
/// </summary>
internal interface IOrder
{
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
    public void OrderUpdate(int id);
}
