namespace Prueba.Domain.Entity
{
    public class ProductVariation
    {
        public ProductVariation(int id, string sku, string name)
        {
            Id = id;
            SKU = sku;
            Name = name;
        }

        public ProductVariation() { }

        public int Id { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
    }
}
