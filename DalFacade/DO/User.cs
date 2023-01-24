using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DO;

public struct User
{
    public string? UserName { set; get; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { set; get; }
    public bool? IsAdmin { set; get; }
    public List<int>? listOfOrder { set; get; }
    public List<OrderItem> CurrentOrder { set; get; }
}
