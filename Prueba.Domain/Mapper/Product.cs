using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Domain.Mapper
{
    public class Product
    {

        public static VO.Product ToProductVO(Entity.Product entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entity.Product, VO.Product>());
            var mapper = config.CreateMapper();
            return mapper.Map<VO.Product>(entity);
        }

        public static VO.ProductFull ToProductFullVO(Entity.Product entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entity.Product, VO.ProductFull>());
            var mapper = config.CreateMapper();
            return mapper.Map<VO.ProductFull>(entity);
        }


        public static ICollection<VO.Product> ToProductVOCollection(ICollection<Entity.Product> entities)
        {
            return entities.Select(x => ToProductVO(x)).ToList();
        }

        public static ICollection<VO.ProductFull> ToProductFullVOCollection(ICollection<Entity.Product> entities)
        {
            return entities.Select(x => ToProductFullVO(x)).ToList();
        }

    }
}
