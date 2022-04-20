using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Domain.Usecase
{
    public interface IProductUsecase
    {
        public Exception Save(DTO.ProductSave product);
        public Exception Edit(DTO.ProductEdit product);
        public (VO.ProductFull, Exception) Get(int productId);
        public Task<(ICollection<VO.Product>, Exception)> List();
    }
}
