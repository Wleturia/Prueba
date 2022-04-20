using LazyCache;
using Prueba.Persistence.Cache.Product;
using System;

namespace Prueba.Persistence.Services
{
    public class ProductCache
    {
        readonly IAppCache cache;

        public ProductCache(IAppCache cache)
        {
            this.cache = cache;
        }

        public ProductCacheDTO CacheProduct(Domain.Entity.Product product)
        {
            Func<ProductCacheDTO> productCache = () => RetrieveData();
            ProductCacheDTO cachedResults = cache.GetOrAdd<ProductCacheDTO>(product.Id.ToString(), productCache);
            return cachedResults;
        }

        private ProductCacheDTO RetrieveData()
        {
            // Este es un cálculo pesado
            System.Threading.Thread.Sleep(4000);
            return new ProductCacheDTO { Rating = 1, Reviews = 5 };
        }
    }
}
