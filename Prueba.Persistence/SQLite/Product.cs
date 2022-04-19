using System;
using System.Collections.Generic;
using System.Data.SQLite;

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

            while (reader.Read())
            {
                var builder = new Domain.Builder.ProductBuilder();
                builder.SetCode(reader["Code"].ToString());
                builder.SetId(Convert.ToInt32(reader["id"].ToString()));
                builder.SetName(reader["Name"].ToString());
                // Consultar caché
                // Consultar servicio externo
                result.Add(builder.GetProduct());
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
                // Consultar caché
                // Consultar servicio externo
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
    }
}
