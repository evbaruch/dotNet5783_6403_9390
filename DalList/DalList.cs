using DalApi;
using DO;
using System.Security.Principal;

namespace Dal;
sealed internal class DalList : IDal
{
    public IOrder order => new DalOrder();
    public IOrderItem orderItem => new DalOrderItem();
    public IProduct product => new DalProduct();


    public static IDal Instance { get; } = new DalList();
}
