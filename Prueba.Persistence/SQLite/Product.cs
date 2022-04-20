using LazyCache;
using Prueba.Persistence.External;
using Prueba.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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

        public ICollection<Domain.Entity.Product> GetAll()
        {
            var result = new List<Domain.Entity.Product>();
            var query = "SELECT * FROM products";
            using var command = new SQLiteCommand(query, db);
            using var reader = command.ExecuteReader();
            if (!reader.HasRows)
                return result;

            var preResult = new List<Domain.Entity.Product>();
            while (reader.Read())
            {
                var builder = new Domain.Builder.ProductBuilder();
                builder.SetCode(reader["code"].ToString());
                builder.SetId(Convert.ToInt32(reader["id"].ToString()));
                builder.SetName(reader["name"].ToString());
                preResult.Add(builder.GetProduct());
            }

            List<Task<Domain.Entity.Product>> TaskList = new List<Task<Domain.Entity.Product>>();
            Parallel.ForEach(preResult.Cast<Domain.Entity.Product>(), (currentElement) =>
            {
                var task = PopulateProduct(currentElement);
                if (task.Status == TaskStatus.Created)
                    task.Start();
                TaskList.Add(task);
            });

            Task.WaitAll(TaskList.ToArray());
            foreach (var task in TaskList)
            {
                result.Add(task.Result);
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
            // Consultar caché
            // Consultar servicio externo
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
