using System;
using System.Text.Json;
using System.Threading.Tasks;
using Basket.API.Database.Entities;
using Basket.API.Database.Helpers.Collections.Interfaces;
using Basket.API.Database.Repositories.Interfaces;

namespace Basket.API.Database.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BasketCart> GetBasket(string userName)
        {
            var basket = await _context
                .Redis
                .StringGetAsync(userName);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<BasketCart>(basket);
        }

        public async Task<BasketCart> UpdateBasket(BasketCart basket)
        {
            var updated = await _context
                .Redis
                .StringSetAsync(basket.UserName, JsonSerializer.Serialize(basket));
            return updated == false ? null : await GetBasket(basket.UserName);
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            return await _context
                .Redis
                .KeyDeleteAsync(userName);
        }
    }
}