using System;
using System.Collections.Generic;

namespace Prueba.Domain.Usecase
{
    public interface IProductUsecase
    {
        public Exception Save(DTO.ProductSave product);
        public Exception Edit(DTO.ProductEdit product);
        public (VO.ProductFull, Exception) Get(int productId);
        public (ICollection<VO.Product>, Exception) List();
    }
}
