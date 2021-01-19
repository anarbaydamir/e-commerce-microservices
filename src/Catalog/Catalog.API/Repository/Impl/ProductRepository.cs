using Catalog.API.Dal.Inter;
using Catalog.API.Entities;
using Catalog.API.Repository.Inter;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext context;

        public ProductRepository(ICatalogContext context)
        {
            this.context = context;
        }

        public async Task create(Product product)
        {
            await context.Products.InsertOneAsync(product);
        }

        public async Task<bool> delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.id, id);

            var deleteResult = await context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged 
                && deleteResult.DeletedCount > 1;
        }

        public async Task<IEnumerable<Product>> getAll()
        {
            return await context.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> getByCategoryName(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.category, categoryName);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> getById(string id)
        {
            return await context.Products.Find(p => p.id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> getByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.name, name);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> update(Product product)
        {
            var updateResult = await context.Products
                                            .ReplaceOneAsync(filter: p => p.id == product.id, replacement: product);
            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }
    }
}
