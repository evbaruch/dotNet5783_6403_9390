using DO;
namespace DalApi;

internal class IDal
{
    IOrder order { get;}
    IOrder OrderItem { get; }
    IOrder Product { get; }
}
