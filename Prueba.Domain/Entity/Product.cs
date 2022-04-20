using System.Collections.Generic;

namespace Prueba.Domain.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public float Rating { get; set; }
        public int Reviews { get; set; }

        public void AddVariation(ProductVariation variation)
        {
            _productVariations.Add(variation);
        }

        public ICollection<ProductVariation> ListVariations()
        {
            return _productVariations;
        }

        private ICollection<ProductVariation> _productVariations = new List<ProductVariation>();
    }
}
