using Prueba.Domain.DTO;
using Prueba.Domain.VO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.App
{
    public class Product : Domain.Usecase.IProductUsecase
    {
        private readonly Domain.Repository.IProductRepository repo;
        public Product(Domain.Repository.IProductRepository repo)
        {
            this.repo = repo;
        }

        public Exception Edit(ProductEdit product)
        {
            return null;
        }

        public (ProductFull, Exception) Get(int productId)
        {
            try
            {
                var entity = repo.GetById(productId);
                return (Domain.Mapper.Product.ToProductFullVO(entity), null);
            }
            catch (Exception err)
            {
                return (null, err);
            }
        }

        public (ICollection<Domain.VO.Product>, Exception) List()
        {
            try
            {
                var entities = repo.GetAll();
                return (Domain.Mapper.Product.ToProductVOCollection(entities), null);
            }
            catch (Exception err)
            {
                return (null, err);
            }

        }

        public Exception Save(ProductSave product)
        {
            try
            {
                var builder = new Domain.Builder.ProductBuilder();
                builder.SetName(product.Name);
                builder.SetCode(product.Code);
                repo.Save(builder.GetProduct());
                return null;
            }
            catch (Exception err)
            {
                return err;
            }
        }
    }
}
