using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Data;
using static System.Net.WebRequestMethods;

namespace WeatherMap_API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string key = config.GetSection("APIKeys").GetSection("WeatherAPI").Value;

            HttpClient weatherMap = new HttpClient();
            string lat = "21.3099";
            string lon = "157.8581";

            using HttpResponseMessage response = await weatherMap.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}&units=imperial");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseBody);

            var json = JObject.Parse(responseBody);
            JObject main = (JObject)json.GetValue("main");
            var t = main.GetValue("temp");
            var p = main.GetValue("pressure");
            var h = main.GetValue("humidity");
            var ti = main.GetValue("temp_min");
            var tm = main.GetValue("temp_max");

            Console.WriteLine(" da Aina forecast:");
            Console.WriteLine($" Brah the temp stay: {t} fahrenheit ");
            Console.WriteLine($" Do you feel da mana in da pressah {p} RAJA DAT ");
            Console.WriteLine($" Buggah is hot!! Check out dat {h} humidity CHEEE ");
            Console.WriteLine($" Best time to nai nai or surf cos da temp stay {ti} {tm} ");
            Console.WriteLine();
        }
    }

}