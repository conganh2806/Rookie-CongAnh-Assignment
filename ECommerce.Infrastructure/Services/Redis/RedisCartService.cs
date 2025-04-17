using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.Services
{
    public class RedisCartService : IRedisCartService
    {
        private readonly IDatabase _db;
        private readonly string _cartKeyPrefix = "cart:";

        public RedisCartService(IConnectionMultiplexer redis)
        { 
            _db = redis.GetDatabase();
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

            return hashEntries.Select(entry => new CartItemDto
            {
                ProductId = entry.Name,
                Quantity = (int)entry.Value
            }).ToList();
        }

        public async Task<bool> RemoveItemAsync(string userId, string productId)
        {
            var cartKey = GetCartKey(userId);
            var result = await _db.HashDeleteAsync(cartKey, productId);
            return result;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            var cartKey = GetCartKey(userId);
            var result = await _db.KeyDeleteAsync(cartKey);
            return result;
        }

        private string GetCartKey(string userId)
        {
            return $"{_cartKeyPrefix}{userId}";
        }
    }
}
