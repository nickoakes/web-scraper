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

                string capitalizedCounty = Capitalize(county);

                using var client = new HttpClient();

                HttpResponseMessage result = await client.GetAsync("https://www.birdguides.com/sightings/");

                if (result.IsSuccessStatusCode)
                {
                    string html = await result.Content.ReadAsStringAsync();

                    string[] countySightings = ProcessHTML(html, capitalizedCounty);

                    if (countySightings.Any())
                    {

                        Console.WriteLine($"Today's sightings for { capitalizedCounty }");

                        DrawLine();

                        foreach (string sighting in countySightings)
                        {
                            string[] columns = sighting
                                               .Split("col-sx-12");

                            string date = ProcessDate(columns[2]);

                            string species = ProcessSpecies(columns[3]);

                            Console.WriteLine($"{ date } - { species }");

                            DrawLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No results found for { county }");
                    }

                }
                else
                {
                    HandleServerError(result);
                }
            }
        }

        static string ProcessDate(string dateHTML)
        {
            return dateHTML
                   .Split("10pt;\">")[1]
                   .Split("</div>")[0]
                   .Trim();
        }

        static string ProcessSpecies(string speciesHTML)
        {
            return speciesHTML
                   .Split("/species-guide/")[1]
                   .Split("\">")[1]
                   .Split("</a>")[0]
                   .Trim()
                   .Replace("&#39;", "'");
        }

        static void DrawLine()
        {
            Console.WriteLine("----------------------------------");
        }

        static string Capitalize(string word)
        {
            string[] multiwordCounty = word.Split(" ");

            string capitalizedCounty = string.Empty;

            foreach (string item in multiwordCounty)
            {
                string capitalizedWord = char.ToUpper(item[0]) + item.Substring(1);

                capitalizedCounty += $"{ capitalizedWord } ";
            }

            return capitalizedCounty.Trim();
        }

        static string[] ProcessHTML(string html, string county)
        {
            string[] sightings = html.Split("sighting-header");

            string[] countySightings = sightings
                                       .Where(x => x.Contains(county))
                                       .ToArray();

            return countySightings;
        }

        static void HandleServerError(HttpResponseMessage result)
        {
            Console.WriteLine("Birdguides appears to be experiencing server issues");

            Console.WriteLine("Error details:");

            Console.WriteLine($"Status code: { result.StatusCode }");
        }
    }
}
