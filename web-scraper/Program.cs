using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace web_scraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a county:");

                string county = Console.ReadLine();

                string capitalizedCounty = Helper.Capitalize(county);

                using var client = new HttpClient();

                HttpResponseMessage result = await client.GetAsync("https://www.birdguides.com/sightings/");

                if (result.IsSuccessStatusCode)
                {
                    string html = await result.Content.ReadAsStringAsync();

                    string[] countySightings = Helper.ProcessHTML(html, capitalizedCounty);

                    if (countySightings.Any())
                    {

                        Console.WriteLine($"Today's sightings for { capitalizedCounty }");

                        Helper.DrawLine();

                        foreach (string sighting in countySightings)
                        {
                            string[] columns = sighting
                                               .Split("col-sx-12");

                            string date = Helper.ProcessDate(columns[2]);

                            string species = Helper.ProcessSpecies(columns[3]);

                            Console.WriteLine($"{ date } - { species }");

                            Helper.DrawLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No results found for { county }");
                    }

                }
                else
                {
                    Helper.HandleServerError(result);
                }
            }
        }

        
    }
}
