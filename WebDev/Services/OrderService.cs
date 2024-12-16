using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using WebDev.DTO;
using WebDev.Entities;
using WebDev.Models;
using WebDev.Services;

namespace WebDev.services
{
    public class OrderService:BaseService
    {
        public OrderService(ApplicationDbContext context):base(context){}


        public async Task AddOrder(OrderDTO model)
        {
            var user = await _context.Users.FindAsync(model.CustomerId)
                ?? throw new Exception($"User with ID {model.CustomerId} not found");

            var products = await _context.Products
                .Where(p => model.ProductIds.Select(e=>e.ProductId).Contains(p.ProductId))
                .ToListAsync();

            if (products.Count != model.ProductIds.Count)
                throw new Exception("Some products were not found");

            

            var order = new Order
            {
                CustomerId = model.CustomerId,
                DeliveryAddress = user.Address,
                OrderDate = DateTime.UtcNow,
                Status = model.Status,
                TotalPrice = products.Sum(p => p.Price*model.ProductIds.First(e=>e.ProductId==p.ProductId).Quantity),
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderProducts = products.Select(p => new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = p.ProductId,
                Quantity = model.ProductIds.First(e=>e.ProductId==p.ProductId).Quantity,
                ProductName = products.First(e=>e.ProductId==p.ProductId).ProductName
            }).ToList();

            await _context.OrderProducts.AddRangeAsync(orderProducts);
            await _context.SaveChangesAsync();
        }
        public async Task<Order> GetOrder(int id)
        {
            // Загружаем заказ
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id)
                ?? throw new Exception("Order not found");

            var productIds = await _context.OrderProducts
                .Where(op => op.OrderId == id)
                .Select(op => op.ProductId)
                .ToListAsync();

            // Загружаем сами продукты по их идентификаторам
            var products = await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            // Присваиваем полученные продукты заказу через промежуточную таблицу
            order.OrderProducts = products.Select(p => new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = p.ProductId,
                Quantity = productIds.Count(id => id == p.ProductId),
                ProductName = products.First(e=>e.ProductId==p.ProductId).ProductName
            }).ToList();

            return order;
        }



        public async Task UpdateOrder(int id, OrderDTO model)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id)
                ?? throw new Exception("Order not found");

            order.Status = model.Status;

            var products = await _context.Products
                .Where(p => model.ProductIds.Select(e=>e.ProductId).Contains(p.ProductId))
                .ToListAsync();

            if (products.Count != model.ProductIds.Count)
                throw new Exception("Some products were not found");


            order.TotalPrice = products.Sum(p => p.Price*model.ProductIds.First(e=>e.ProductId==p.ProductId).Quantity);

            var oldOrderProducts = await _context.OrderProducts
                    .Where(op => op.OrderId == order.OrderId)
                    .ToListAsync();

            _context.OrderProducts.RemoveRange(oldOrderProducts);

            var orderProducts = products.Select(p => new OrderProduct
            {
                OrderId = order.OrderId,
                ProductId = p.ProductId,
                Quantity = model.ProductIds.First(e=>e.ProductId==p.ProductId).Quantity,
                ProductName = products.First(e=>e.ProductId==p.ProductId).ProductName
            }).ToList();

            await _context.OrderProducts.AddRangeAsync(orderProducts);

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            var orderProducts = await _context.OrderProducts
                .Where(op => op.OrderId == id)
                .ToListAsync();

            _context.OrderProducts.RemoveRange(orderProducts);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }



        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o=>o.OrderProducts)
                            .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }

    }
}
