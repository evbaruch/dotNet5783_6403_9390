using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public class IDWhoException : Exception
{
    public IDWhoException(string messege) : base() { }
}
public class ISawYouAlreadyException : Exception
{ 
    public ISawYouAlreadyException(string messege) : base() { }
}