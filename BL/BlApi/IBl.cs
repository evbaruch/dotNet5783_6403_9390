using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IBl
    {
        ICart cart { get; }

        IOrder order { get; }

        IProduct product { get; }
    }
}
