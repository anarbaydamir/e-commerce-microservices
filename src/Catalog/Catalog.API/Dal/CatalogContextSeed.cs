using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Dal
{
    public class CatalogContextSeed
    {
        public static void seedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
                productCollection.InsertManyAsync(getPreconfiguredProducts());
        }

        private static IEnumerable<Product> getPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    name="IPhone",
                    summary="best phone",
                    description="Billie Jean is not my lover",
                    price=950.00M,
                    imageFile="product-1.png",
                    category="Smart Phone"
                },
                new Product
                {
                    name="Samsung",
                    summary="best phone",
                    description="Billie Jean is not my lover",
                    price=950.00M,
                    imageFile="product-1.png",
                    category="Smart Phone"
                },
                new Product
                {
                    name="Xiaomi",
                    summary="best phone",
                    description="Billie Jean is not my lover",
                    price=950.00M,
                    imageFile="product-1.png",
                    category="Smart Phone"
                }
            };
        }
    }
}
