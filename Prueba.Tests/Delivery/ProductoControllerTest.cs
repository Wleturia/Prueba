using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Prueba.Delivery;
using Prueba.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Prueba.Tests.Delivery
{
    [TestClass]
    public class ProductoControllerTest
    {
        [TestMethod]
        public async Task ShouldReturnIEnumerableOfProductVO()
        {
            var _factory = new WebApplicationFactory<Startup>()
                           .WithWebHostBuilder(builder => builder.UseSetting("https_port", "5001")
                               .UseEnvironment("Testing"));
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/product");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var json = await response.Content.ReadAsStringAsync();

            var types = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Domain.VO.Product>>(json);

            Assert.IsInstanceOfType(types, typeof(IEnumerable<Domain.VO.Product>));
        }

        [TestMethod]
        public async Task ShouldReturnInstanceOfProductFullVO()
        {
            var _factory = new WebApplicationFactory<Startup>()
                           .WithWebHostBuilder(builder =>
                           builder
                           .UseSetting("https_port", "5001")
                           .UseEnvironment("Testing")
                           .ConfigureServices(services =>
                                {
                                    var productUsecase = new MockProductUsecase();
                                    services.AddSingleton<Domain.Usecase.IProductUsecase>(productUsecase);
                                    services.AddControllers();
                                })
                               );

            var client = _factory.CreateClient();
            var response = await client.GetAsync("Product/1");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var json = await response.Content.ReadAsStringAsync();

            var types = JsonConvert.DeserializeObject<Domain.VO.ProductFull>(json);

            Assert.IsInstanceOfType(types, typeof(Domain.VO.ProductFull));
            Assert.AreEqual("TESTINTERNO", types.Code);
        }
    }

    public class MockProductUsecase : Domain.Usecase.IProductUsecase
    {
        public Exception Edit(ProductEdit product)
        {
            throw new NotImplementedException();
        }

        public (Domain.VO.ProductFull, Exception) Get(int productId)
        {
            return (new Domain.VO.ProductFull() { Id = productId, Code = "TESTINTERNO" }, null);
        }

        public (ICollection<Domain.VO.Product>, Exception) List()
        {
            throw new NotImplementedException();
        }

        public Exception Save(ProductSave product)
        {
            throw new NotImplementedException();
        }
    }
}
