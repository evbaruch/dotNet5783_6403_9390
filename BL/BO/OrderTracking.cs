
using static BO.Enums;
namespace BO;

public class OrderTracking
{
   public int ID { get; set; }
   public productsCategory Status { get; set; }

    public override string ToString() => $@"
    ID:             {ID}
    Status:         {Status} 
    ";
}
