using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Services
{
    public class RedisCartService : IRedisCartService
    {
        private readonly IDatabase _db;
        private readonly IProductService _productService;
        private readonly string _cartKeyPrefix = "cart:";

        public RedisCartService(IConnectionMultiplexer redis,
                                IProductService productService)
        { 
            _db = redis.GetDatabase();
            _productService = productService;
        }

        public async Task<CartDto> AddOrUpdateItemAsync(string userId, CartItemDto item)
        {
            var cartKey = GetCartKey(userId);
            
            var existingQuantity = await _db.HashGetAsync(cartKey, item.ProductId);
            int newQuantity = existingQuantity.HasValue ? (int)existingQuantity + item.Quantity 
                                                        : item.Quantity;

            await _db.HashSetAsync(cartKey, item.ProductId, newQuantity);
            
            var cartItems = await GetAllItemsInCartAsync(userId);

            return new CartDto
            {
                UserId = userId,
                Items = cartItems
            };
        }

        public async Task<CartDto> GetCartAsync(string userId)
        {
            var cartItems = await GetAllItemsInCartAsync(userId);
            
            return new CartDto
            {
                UserId = userId,
                Items = cartItems
            };
        }

        private async Task<List<CartItemDto>> GetAllItemsInCartAsync(string userId)
        {
            var cartKey = GetCartKey(userId);
            var hashEntries = await _db.HashGetAllAsync(cartKey);

            var cartItems = new List<CartItemDto>();

            foreach (var entry in hashEntries)
            {
                var productId = entry.Name.ToString();

                if (!int.TryParse(entry.Value.ToString(), out var quantity))
                    continue;

                var product = await _productService.GetByIdAsync(productId);
                if (product is null)
                    continue;

                cartItems.Add(new CartItemDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }

            return cartItems;
        }

        public async Task<bool> RemoveItemAsync(string userId, string productId)
        {
            var cartKey = GetCartKey(userId);
            return await _db.HashDeleteAsync(cartKey, productId);
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartKey = GetCartKey(userId);
            return await _db.KeyDeleteAsync(cartKey);
        }

        private string GetCartKey(string userId)
        {
            return $"{_cartKeyPrefix}{userId}";
        }
    }
}
