﻿using Microsoft.EntityFrameworkCore;
using WebDev.DTO;
using WebDev.Entities;
using WebDev.Models;

namespace WebDev.services
{
    public class ProductService
    {
        private readonly ApplicationDBContext _context;

        public ProductService(ApplicationDBContext context)
        {
            _context = context;
        }

        // Валидация имени продукта
        public void ValidateProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException("Product name cannot be empty.");
            if (productName.Length > 100)
                throw new ArgumentException("Product name cannot exceed 100 characters.");
        }

        // Валидация категории продукта
        public void ValidateCategory(ProductCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");

            if (string.IsNullOrWhiteSpace(category.CategoryName))
                throw new ArgumentException("Category name cannot be empty.");

            if (category.CategoryName.Length > 100)
                throw new ArgumentException("Category name cannot exceed 100 characters.");

            if (!string.IsNullOrWhiteSpace(category.CategoryDescription) && category.CategoryDescription.Length > 500)
                throw new ArgumentException("Category description cannot exceed 500 characters.");

            // Проверка существования категории в базе данных
            bool categoryExists = _context.ProductCategories.Any(c => c.CategoryId == category.CategoryId);
            if (!categoryExists)
                throw new ArgumentException($"Category with ID {category.CategoryId} does not exist.");
        }

        // Валидация цены продукта
        public void ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentException("Price must be greater than 0.");
            if (price > 100000)
                throw new ArgumentException("Price must be realistic (e.g., less than 100,000).");
        }

        // Валидация описания продукта
        public void ValidateProductDescription(string productDescription)
        {
            if (!string.IsNullOrWhiteSpace(productDescription) && productDescription.Length > 500)
                throw new ArgumentException("Product description cannot exceed 500 characters.");
        }

        // Получение всех продуктов
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category) // Загружаем категории
                .ToListAsync();
        }

        // Получение продукта по ID
        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category) // Загрузка категории
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                throw new Exception($"Product with ID {id} not found");

            return product;
        }
        // Удаление продукта
        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        // Обновление информации о продукте
        public async Task UpdateProduct(int id, ProductDTO updatedProduct)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new Exception($"Product with ID {id} not found");

            // Обновляем свойства продукта
            product.ProductName = updatedProduct.ProductName;
            product.ProductDescription = updatedProduct.ProductDescription;
            product.Price = updatedProduct.Price;
            product.Category = await _context.ProductCategories.FindAsync(updatedProduct.CategoryId);

            await _context.SaveChangesAsync();
        }

        public async Task AddProduct(ProductDTO newProduct)
        {
            ValidateProductName(newProduct.ProductName);
            ValidateCategory(await _context.ProductCategories.FindAsync(newProduct.CategoryId));
            ValidatePrice(newProduct.Price);
            ValidateProductDescription(newProduct.ProductDescription);

            _context.Products.Add(new Product
            {
                Category = await _context.ProductCategories.FindAsync(newProduct.CategoryId),
                ProductName = newProduct.ProductName,
                ProductDescription = newProduct.ProductDescription,
                Price = newProduct.Price

            });
            await _context.SaveChangesAsync();
        }
    }
}
