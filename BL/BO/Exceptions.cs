using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class IncorrectDataException : Exception
{
    public IncorrectDataException() : base() { }

    public IncorrectDataException(string messege) : base() { }

    public IncorrectDataException(string messege, Exception innnerException) : base(messege, innnerException) { }
}
public class DataNotFoundException : Exception
{
    public DataNotFoundException() : base() { }

    public DataNotFoundException(string messege) : base() { }

    public DataNotFoundException(string messege, Exception innnerException) : base(messege, innnerException) { }
}
