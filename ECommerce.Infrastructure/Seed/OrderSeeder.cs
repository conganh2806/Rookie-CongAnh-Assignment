using ECommerce.Domain.Entities;
using ECommerce.Domain.Enum;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public static class OrderSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (await context.Orders.AnyAsync()) return;

        var user = await context.Users
                        .FirstOrDefaultAsync(u => u.Email == "user@game.com");
        var product = await context.Products.OrderBy(p => p.Name).FirstOrDefaultAsync();

        if (user != null && product != null)
        {
            var order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                TotalAmount = product.Price,
                PaymentType = PaymentType.VNPay, 
                PaymentStatus = PaymentStatus.Pending, 
                Status = OrderStatus.Pending,
            };
            
            context.Orders.Add(order);

            var orderDetail = new OrderDetail
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = 1,
                Price = product.Price,
            };

            context.OrderDetails.Add(orderDetail);

            await context.SaveChangesAsync();
        }
    }
}
