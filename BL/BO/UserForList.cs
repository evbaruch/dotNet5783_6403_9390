using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class UserForList
{
    public string? UserName { set; get; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public bool? IsAdmin { set; get; }
    public int? NumOfOrder { set; get; }

    public override string ToString() => $@"
    Name:      {UserName} 
    Address:   {Address} 
    Email:     {Email}
    IsAdmin:   {IsAdmin}
    Past orders:   \n {NumOfOrder}
    ";
}
