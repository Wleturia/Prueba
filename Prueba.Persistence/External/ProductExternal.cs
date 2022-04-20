using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prueba.Persistence.External
{
    public class ProductExternal
    {
        public static async Task<DTO.ExternalDTO> RetrieveExternal(Domain.Entity.Product product)
        {
            HttpClient client = new HttpClient();
            string url = $"https://retoolapi.dev/UB2VDp/product_photos/{product.Id}";
            Console.WriteLine(url);
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DTO.ExternalDTO>(responseBody);
        }
    }
}