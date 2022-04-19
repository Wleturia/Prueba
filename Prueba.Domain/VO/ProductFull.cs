using System.Collections.Generic;

namespace Prueba.Domain.VO
{
    public class ProductFull
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public int Reviews { get; set; }

        public ICollection<ProductVariation> Variations { get; set; }
    }
}
