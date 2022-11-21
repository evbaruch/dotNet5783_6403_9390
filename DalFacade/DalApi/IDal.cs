using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

internal interface IDal
{
    public Order Order { get;}

    public OrderItem Item { get;}

    public Product Product { get;}
}
