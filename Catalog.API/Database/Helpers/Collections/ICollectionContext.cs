using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Helpers.Collections
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}