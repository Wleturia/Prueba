using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Domain.Mapper
{
    public class ProductVariation
    {
        public static VO.ProductVariation ToProductVariationVO(Entity.Product entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entity.ProductVariation, VO.ProductVariation>());
            var mapper = config.CreateMapper();
            return mapper.Map<VO.ProductVariation>(entity);
        }

        public static ICollection<VO.ProductVariation> ToProductVariationVOCollection(ICollection<Entity.Product> entities)
        {
            return entities.Select(x => ToProductVariationVO(x)).ToList();
        }

    }
}
