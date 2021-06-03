using StackExchange.Redis;

namespace Basket.API.Database.Helpers.Collections.Interfaces
{
    public interface IBasketContext
    {
        IDatabase Redis { get; }
    }
}