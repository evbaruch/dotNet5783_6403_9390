using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal;

sealed internal class DalXml : IDal
{
    private DalXml() { } // constructor stage 6

    public static IDal Instance { get; } = new DalXml(); // stage 6

    public IOrder order { get; } = new DalOrder();

    public IProduct product { get; } = new DalProduct();

    public IOrderItem orderItem { get; } = new DalOrderItem();
}
