using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prueba.Delivery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Product : ControllerBase
    {
        private readonly Domain.Usecase.IProductUsecase _productUC;

        public Product(Domain.Usecase.IProductUsecase product)
        {
            _productUC = product;
        }

        [HttpGet]
        public IEnumerable<Domain.VO.Product> List()
        {
            var (res, err) = _productUC.List();
            if (err != null)
                throw new ApplicationException(err.Message);
            return res;
        }

        [HttpPost]
        public StatusCodeResult Save([FromBody] Domain.DTO.ProductSave product)
        {
            var err = _productUC.Save(product);
            if (err != null)
                throw new ApplicationException(err.Message);
            return StatusCode(200);
        }

        [HttpGet("{id}")]
        public Domain.VO.ProductFull Get(string id)
        {
            bool isParsable = int.TryParse(id, out int number);

            if (!isParsable)
                throw new ApplicationException("Not a valid id");

            var (res, err) = _productUC.Get(number);

            if (err != null)
                throw new ApplicationException(err.Message);

            return res;
        }
    }
}
