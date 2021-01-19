using Catalog.API.Dal.Inter;
using Catalog.API.Entities;
using Catalog.API.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Dal.Impl
{
    public class CatalogContext:ICatalogContext
    {
        public CatalogContext(ICatalogDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);
            var database = client.GetDatabase(dbSettings.DatabaseName);

            this.Products = database.GetCollection<Product>(dbSettings.CollectionName);
            CatalogContextSeed.seedData(this.Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
