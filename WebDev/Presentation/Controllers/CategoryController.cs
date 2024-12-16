using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using WebDev.BLL.DTO;
using WebDev.BLL.Services;

namespace WebDev.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController
        (
           CategoryService service
        ) : ControllerBase
    {
        private readonly CategoryService _categoryService = service;

        [HttpPut("category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO model)
        {
            await _categoryService.AddCategory(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            return Ok(await _categoryService.GetCategory(id));
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO model)
        {
            await _categoryService.UpdateCategory(id, model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }
    }
}