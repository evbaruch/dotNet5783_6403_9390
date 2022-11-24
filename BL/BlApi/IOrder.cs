using BO;
namespace BlApi;
/// <summary>
/// 
/// </summary>
internal interface IOrder
{
    /// <summary>
    /// Order list request (admin screen)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order> OrderListRequest();
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
}
