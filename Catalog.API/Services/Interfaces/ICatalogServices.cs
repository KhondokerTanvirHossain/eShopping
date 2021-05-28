using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;

namespace Catalog.API.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(string id);
        public Task<IEnumerable<Product>> GetProductsByName(string name);
        public Task<IEnumerable<Product>> GetProductsByCategory(string category);

        public Task<Product> Create(Product product);
        public Task<bool> Update(Product product);
        public Task<bool> Delete(string id);
        
    }
}