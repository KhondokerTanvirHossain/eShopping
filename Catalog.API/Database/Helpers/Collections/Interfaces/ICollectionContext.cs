using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Helpers.Collections.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}