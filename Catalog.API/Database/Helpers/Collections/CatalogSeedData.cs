using System.Collections.Generic;
using Catalog.API.Database.Entities;
using MongoDB.Driver;

namespace Catalog.API.Database.Helpers.Collections
{
    public class CatalogContextSeedData
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            var isProductExist = products.Find(p => true).Any();
            if (!isProductExist)
            {
                products.InsertManyAsync(GetPreConfiguredProduct());
            }
        }

        private static IEnumerable<Product> GetPreConfiguredProduct()
        {
            return new List<Product>()
            {
                new()
                {
                    Name = "Iphone X",
                    Description = "Have lot of things",
                    Summary = "Good phone",
                    ImageFile = "IPhoneX.png",
                    Price = 10000,
                    Category = "Phone"
                },
                new()
                {
                    Name = "Iphone 11",
                    Description = "Have lot of things",
                    Summary = "Good phone",
                    ImageFile = "IPhone11.png",
                    Price = 20000,
                    Category = "Phone"
                }
            };
        }
    }
}