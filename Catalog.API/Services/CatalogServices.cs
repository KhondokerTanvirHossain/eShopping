using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;
using Catalog.API.Database.Repositories.Interfaces;
using Catalog.API.Services.Interfaces;

namespace Catalog.API.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IProductRepository _repository;

        public CatalogService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _repository.GetProducts();
        }
        
        public async Task<Product> GetProduct(string id)
        {
            return await _repository.GetProduct(id);
        }
        
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _repository.GetProductsByName(name);
        }
        
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            return await _repository.GetProductsByCategory(category);
        }
        
        public async Task<Product> Create(Product product)
        {
             await _repository.Create(product);
             return product;
        }

        public async Task<bool> Update(Product product)
        {
            return await _repository.Update(product);
        }
        
        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
        
    }
}