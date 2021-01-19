using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repository.Inter
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> getAll();
        Task<Product> getById(string id);
        Task<IEnumerable<Product>> getByName(string name);
        Task<IEnumerable<Product>> getByCategoryName(string categoryName);

        Task create(Product product);
        Task<bool> update(Product product);
        Task<bool> delete(string id);
    }
}
