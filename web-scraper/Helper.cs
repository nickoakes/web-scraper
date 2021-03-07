using System;
using System.Linq;
using System.Net.Http;

namespace web_scraper
{
    public static class Helper
    {
        public static string ProcessDate(string dateHTML)
        {
            if (!string.IsNullOrEmpty(dateHTML))
            {
                return dateHTML
                   .Split("10pt;\">")[1]
                   .Split("</div>")[0]
                   .Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ProcessSpecies(string speciesHTML)
        {
            if (!string.IsNullOrEmpty(speciesHTML))
            {
                return speciesHTML
                   .Split("/species-guide/")[1]
                   .Split("\">")[1]
                   .Split("</a>")[0]
                   .Trim()
                   .Replace("&#39;", "'");
            }
            else
            {
                return string.Empty;
            }
        }

        public static void DrawLine()
        {
            Console.WriteLine("----------------------------------");
        }

        public static string Capitalize(string word)
        {
            if (!string.IsNullOrEmpty(word))
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
            else
            {
                return string.Empty;
            }
        }

        public static string[] ProcessHTML(string html, string county)
        {
            if(!string.IsNullOrEmpty(html) && !string.IsNullOrEmpty(county))
            {
                string[] sightings = html.Split("sighting-header");

                string[] countySightings = sightings
                                           .Where(x => x.Contains(county))
                                           .ToArray();

                return countySightings;
            }
            else
            {
                return new string[] { };
            }
        }

        public static void HandleServerError(HttpResponseMessage result)
        {
            Console.WriteLine("Birdguides appears to be experiencing server issues");

            Console.WriteLine("Error details:");

            Console.WriteLine($"Status code: { result.StatusCode }");
        }
    }
}
