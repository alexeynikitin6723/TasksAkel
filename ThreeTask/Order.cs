using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
  public class Order
  {
    public int Code { get; set; }
    public int ProductCode { get; set; }
    public int ClientCode { get; set; }
    public string OrderNumber { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
  }
}
