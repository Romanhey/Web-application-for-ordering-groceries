using System.Text.Json.Serialization;
using WebDev.DAL.Entities;

public class Product
{

    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string ProductDescription { get; set; }
    public int CategoryId { get; set; } // Foreign Key
    public ProductCategory Category { get; set; } // Навигационное свойство

    public byte[] ImageData{ get; set; }
    public string ImageSrc { get; set; }

    [JsonIgnore]
    public ICollection<OrderProduct> OrderProducts { get; set; }
}
