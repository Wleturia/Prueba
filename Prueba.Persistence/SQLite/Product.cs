using LazyCache;
using Prueba.Persistence.External;
using Prueba.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Prueba.Persistence.SQLite
{
    public class Product : Domain.Repository.IProductRepository
    {
        readonly SQLiteConnection db;
        public Product(SQLiteConnection db)
        {
            this.db = db;
        }

        public void Edit(Domain.Entity.Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Domain.Entity.Product>> GetAll()
        {
            var result = new List<Domain.Entity.Product>();
            var query = "SELECT * FROM products";
            using var command = new SQLiteCommand(query, db);
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
                return result;

            while (reader.Read())
            {
                var builder = new Domain.Builder.ProductBuilder();
                builder.SetCode(reader["code"].ToString());
                builder.SetId(Convert.ToInt32(reader["id"].ToString()));
                builder.SetName(reader["name"].ToString());
                // Consultar caché
                // Consultar servicio externo
                result.Add(await PopulateProduct(builder.GetProduct()));
                //result.Add();
            }
            return result;
        }

        public Domain.Entity.Product GetById(int productId)
        {
            var query = $"SELECT * FROM products p WHERE p.id = {productId}";
            using var command = new SQLiteCommand(query, db);
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
                throw new Exception($"The id {productId} has no coincidences");

            var result = new Domain.Entity.Product();
            while (reader.Read())
            {
                result.Id = Convert.ToInt32(reader["id"].ToString());
                result.Name = reader["Name"].ToString();
                result.Code = reader["Code"].ToString();
            }
            return result;
        }

        public void Save(Domain.Entity.Product product)
        {
            var query = $"INSERT INTO products VALUES (null, '{product.Code}', '{product.Name}')";
            using var command = new SQLiteCommand(query, db);
            var rows = command.ExecuteNonQuery();
            if (rows != 1)
                throw new Exception($"The insertion has affected {rows} rows");
        }

        private async Task<Domain.Entity.Product> PopulateProduct(Domain.Entity.Product product)
        {
            IAppCache cache = new CachingService();
            var productCache = new ProductCache(cache);

            var dataCache = productCache.CacheProduct(product);
            var dataExternal = await ProductExternal.RetrieveExternal(product);

            product.Rating = dataCache.Rating;
            product.Reviews = dataCache.Reviews;
            product.Photo = dataExternal.Foto;
            return product;
        }
    }
}
