using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class IncorrectDataException : Exception
{
    public IncorrectDataException(string messege) : base() { }
}
public class DataNotFoundException : Exception
{
    public DataNotFoundException(string messege) : base() { }
}
