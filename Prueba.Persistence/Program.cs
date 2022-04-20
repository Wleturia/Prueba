using Prueba.Persistence.External;
using System;
using System.Threading.Tasks;

namespace Prueba.Persistence
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = await ProductExternal.RetrieveExternal(new Domain.Entity.Product { Id = 1 });
            Console.WriteLine(a.Id);
            Console.WriteLine(a.Foto);
        }

    }
}
