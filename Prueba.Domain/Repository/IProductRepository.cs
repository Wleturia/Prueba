using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Domain.Repository
{
    public interface IProductRepository
    {
        public void Save(Entity.Product product);
        public void Edit(Entity.Product product);
        public Entity.Product GetById(int productId);
        public Task<ICollection<Entity.Product>> GetAll();
    }
}
