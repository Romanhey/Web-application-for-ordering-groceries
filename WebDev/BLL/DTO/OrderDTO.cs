using WebDev.DAL.Entities;
using static WebDev.DAL.Entities.Order;

namespace WebDev.BLL.DTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductIdDTO> ProductIds { get; set; }


    }
    public class ProductIdDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }




}
