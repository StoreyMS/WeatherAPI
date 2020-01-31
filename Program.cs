using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace APIWeatherProject
{
    class Program
    {

        static void Main(string[] args)
        {
#if DEBUG
            string key = File.ReadAllText("appsettings.debug.json");
#else
        string key = File.ReadAllText("appsettings.release.json");
#endif

            var httpclient = new HttpClient();

            Console.WriteLine("Enter Zip Code");
            int zip = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your Country Code (Ex. US, CA etc...)");
            string countrycode = Console.ReadLine();

            JObject jObject = JObject.Parse(key);
            JToken token = jObject["ApiKey"];
            string apikey = token.ToString();

            string url = $"https://api.openweathermap.org/data/2.5/weather?zip={zip},{countrycode}&appid={apikey}";

            Task<string> response = httpclient.GetStringAsync(url);
            string newresponse = response.Result;
            JObject jObject1 = JObject.Parse(newresponse);
            var temp = jObject1["main"]["temp"].ToString();
            var tempconversion = double.Parse(temp) * 9 / 5 - 459.67;
            tempconversion = Math.Round(tempconversion, 1);
            Console.WriteLine($"Temp is {tempconversion}");


        }











    }



}