using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;
using Catalog.API.Database.Helpers.Collections.Interfaces;
using Catalog.API.Database.Repositories.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _iCatalogContext;

        public ProductRepository(ICatalogContext iCatalogContext)
        {
            _iCatalogContext = iCatalogContext ?? throw new ArgumentNullException(nameof(iCatalogContext));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _iCatalogContext
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _iCatalogContext
                .Products
                .Find(p => p.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq( p => p.Name, name);
            return await _iCatalogContext
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, category);
            return await _iCatalogContext
                .Products
                .Find(filter)
                .ToListAsync();
        }

        public async Task Create(Product product)
        {
            await _iCatalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> Update(Product product)
        {
            var result = await _iCatalogContext
                .Products
                .ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return result.IsAcknowledged && result.IsModifiedCountAvailable;
        }

        public async Task<bool> Delete(string id)
        {
            var result = await _iCatalogContext
                .Products
                .DeleteOneAsync(Builders<Product>.Filter.Eq(p => p.Id, id));
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}