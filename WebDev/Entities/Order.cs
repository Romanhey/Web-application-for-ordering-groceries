using System;
using WebDev.Entities;

namespace WebDev.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryAddress { get; set; }

        // Используем промежуточную таблицу для связи многие-ко-многим
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
