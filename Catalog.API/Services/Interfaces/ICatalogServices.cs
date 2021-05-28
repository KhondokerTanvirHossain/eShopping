using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.API.Database.Entities;

namespace Catalog.API.Services.Interfaces
{
    public interface ICatalogService
    {
        public Task<IEnumerable<Product>> GetProducts();
    }
}