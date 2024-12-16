namespace WebDev.BLL.DTO
{
    public class ProductDTO
    {
        public IFormFile formFile { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
    }
}
