using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Valutavalto;

namespace Penzvalto
{
    internal class Program
    {
        static Valuta valuta = null;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Mennyit szeretne átváltani?");
            double szam = int.Parse(Console.ReadLine());
            await valutaValto();
            var euro = szam / valuta.Rates["HUF"];
            var dollar = (szam / valuta.Rates["HUF"])*valuta.Rates["USD"];
            Console.WriteLine($"{szam} HUF = {euro} EUR");
            Console.WriteLine($"{szam} HUF = {dollar} USD");


            Console.ReadKey();

        }
        private static async Task valutaValto()
        {
            string url = $"https://infojegyzet.hu/webszerkesztes/php/valuta/api/v1/arfolyam/";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string jsonString = await response.Content.ReadAsStringAsync();
            valuta = Valuta.FromJson(jsonString);

        }
    }
}
