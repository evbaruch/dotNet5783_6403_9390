
using static BO.Enums;
namespace BO;

public class OrderTracking
{
   public int ID { get; set; }
   public OrderStatus? Status { get; set; }
   public List<Tuple<DateTime,Enums.OrderStatus>?> OrderStatuses { get; set; }

    public override string ToString() => $@"
    ID:             {ID}
    Status:         {Status} 
    ";
}
