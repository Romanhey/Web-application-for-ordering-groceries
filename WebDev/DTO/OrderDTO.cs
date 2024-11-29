using WebDev.Models;
using static WebDev.Models.Order;

namespace WebDev.DTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> ProductIds { get; set; }


    }




}
