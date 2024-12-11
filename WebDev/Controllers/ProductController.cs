using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using WebDev.DTO;
using WebDev.services;

namespace WebDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController
        (
        ProductService service  
        ) : ControllerBase 
    {
        private readonly ProductService _productService = service;

        [HttpPut("product")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductDTO model)
        {
            await _productService.AddProduct(model);

            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]

        public async Task <IActionResult> GetProduct(int id)
        {
            return Ok (await _productService.GetProduct(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return Ok("Product deleted");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO model)
        {
            await _productService.UpdateProduct(id, model);
            return Ok();
        }


    }
}
