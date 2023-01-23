using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BO.Enums;

namespace BO;
public class User
{
    public string? UserName { set; get; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public string? Password { set; get; }
    public bool? IsAdmin { set; get; }
    public Cart? currentCart { set; get; }
    public List<OrderForList>? listOfOrder { set; get; }

    public override string ToString() => $@"
    Name:      {UserName} 
    Address:   {Address} 
    Email:     {Email}
    IsAdmin:   {IsAdmin}
    Past orders:   \n {listOfOrder}
    ";
}
