using Prueba.Domain.Entity;

namespace Prueba.Domain.Builder
{
    public class ProductBuilder
    {
        Product _product = new Product();

        public ProductBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this._product = new Product();
        }

        public void SetId(int id)
        {
            _product.Id = id;
        }

        public void SetName(string nombre)
        {
            _product.Name = nombre;
        }

        public void SetCode(string code)
        {
            _product.Code = code;
        }

        public void SetRating(float rating)
        {
            _product.Rating = rating;
        }
        public void SetReviews(int reviews)
        {
            _product.Reviews = reviews;
        }

        public void AddVariation(ProductVariation variation)
        {
            _product.AddVariation(variation);
        }

        public Product GetProduct()
        {
            Product product = this._product;
            Reset();
            return product;
        }
    }
}
