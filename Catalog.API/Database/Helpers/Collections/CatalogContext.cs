using Catalog.API.Database.Entities;
using Catalog.API.Database.Helpers.Collections.Interfaces;
using Catalog.API.Database.Helpers.Settings;
using MongoDB.Driver;

namespace Catalog.API.Database.Helpers.Collections
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectingString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            CatalogContextSeedData.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}