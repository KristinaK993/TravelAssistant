using Newtonsoft.Json;

namespace TravelAssistant
{
    public class DataLoader
    {
        public static List<City> LoadCities()
        {
            string cityData = File.ReadAllText("cities.json");

            var citiesWrapper = JsonConvert.DeserializeObject<Dictionary<string, List<City>>>(cityData);

            return citiesWrapper["cities"];
        }
    }
}
