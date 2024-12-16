using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebDev.DAL.Entities
{
    public class ProductCategory
    {
        [Key] public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }
    }

}
