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

        public Task<IEnumerable<Product>> GetProducts()
        {
            return _repository.GetProducts();
        }
    }
}