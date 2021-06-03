using System;
using Basket.API.Database.Helpers.Collections.Interfaces;
using StackExchange.Redis;

namespace Basket.API.Database.Helpers.Collections
{
    public class BasketContext : IBasketContext
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public BasketContext(ConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            Redis = redisConnection.GetDatabase();
        }

        public IDatabase Redis { get; }
    }
}