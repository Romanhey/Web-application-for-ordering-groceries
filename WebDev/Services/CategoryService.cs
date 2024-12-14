using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDev.DTO;
using WebDev.Models;
using WebDev.Services;

namespace WebDev.services
{
    public class CategoryService:BaseService
    {
        public CategoryService(ApplicationDBContext context):base(context){}
        public async Task AddCategory(CategoryDTO model)
        {
            // Проверка: описание категории не должно быть пустым или слишком длинным
            if (string.IsNullOrWhiteSpace(model.CategoryDescription))
            {
                throw new ArgumentException("Category description cannot be empty.");
            }

            if (model.CategoryDescription.Length > 200)
            {
                throw new ArgumentException("Category description cannot exceed 200 characters.");
            }

            // Проверка: не существует ли уже категории с таким названием
            var isCategoryExists = await _context.ProductCategories
                .AnyAsync(c => c.CategoryName == model.CategoryName);

            if (isCategoryExists)
            {
                throw new InvalidOperationException("A category with the same name already exists.");
            }

            // Создаем новую категорию
            var newCategory = new ProductCategory
            {
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription,
            };

            // Добавляем категорию в базу данных
            _context.ProductCategories.Add(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductCategory> GetCategory(int id)  
        {

            return await _context.ProductCategories.FirstOrDefaultAsync(c => c.CategoryId == id) ?? throw new Exception("Not found");
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync(c=>c.CategoryId == id);
            if (category == null) { throw new InvalidOperationException("Category doesn't exist"); }
            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, CategoryDTO model)
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                throw new InvalidOperationException("Category doesn't exist");
            }
            if (string.IsNullOrWhiteSpace(model.CategoryDescription))
            {
                throw new ArgumentException("Category description cannot be empty.");
            }

            if (model.CategoryDescription.Length > 200)
            {
                throw new ArgumentException("Category description cannot exceed 200 characters.");
            }

            // Проверка: не существует ли уже категории с таким названием
            var isCategoryExists = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.CategoryName == model.CategoryName);

            if (isCategoryExists is not null)
            {
                throw new InvalidOperationException("A category with the same name already exists.");
            }
            
            category.CategoryName = model.CategoryName;
            category.CategoryDescription = model.CategoryDescription;

            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategory>> GetAllCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }
    }
}
