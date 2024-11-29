using System.Text.Json.Serialization;
using WebDev.Entities;
using WebDev.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string ProductDescription { get; set; }
    public int CategoryId { get; set; } // Foreign Key
    public ProductCategory Category { get; set; } // Навигационное свойство

    [JsonIgnore]
    public ICollection<OrderProduct> OrderProducts { get; set; }
}
