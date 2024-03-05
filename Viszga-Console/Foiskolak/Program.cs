using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foiskolak;
using System.Threading.Tasks;
using System.Net.Http;

namespace Foiskolak
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<Foiskola> Foiskolak = new List<Foiskola>();
            Foiskolak = await foiskolakAdatok();
            foreach(Foiskola foiskolak in Foiskolak)
            {
                Console.WriteLine($"{foiskolak.Name} - {foiskolak.Country}");
            }
            Console.ReadKey();
           
        }

        private static async Task<List<Foiskola>> foiskolakAdatok()
        {
            List<Foiskola> foiskolak = new List<Foiskola>();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://universities.hipolabs.com/search?country=hungary");
            if(response.IsSuccessStatusCode)
            {
                
                string jsonString = await response.Content.ReadAsStringAsync();
                foiskolak = Foiskola.FromJson(jsonString).ToList();
                
            }
            return foiskolak;
        }
    }
}
