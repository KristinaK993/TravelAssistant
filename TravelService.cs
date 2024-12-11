using Spectre.Console;

namespace TravelAssistant
{
    public class TravelService
    {
        private List<City> _cities;

        public TravelService(List<City> cities)
        {
            _cities = cities;
        }

        public City GetCityByName(string cityName)
        {
            return _cities.FirstOrDefault(c => c.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));
        }

        public void DisplayConnections(City city)
        {
            AnsiConsole.MarkupLine($"[bold yellow]Möjliga destinationer från {city.Name}:[/]");
            var table = new Table();
            table.AddColumn("Destination");
            table.AddColumn("Avstånd (km)");
            table.AddColumn("Tid (min)");
            table.AddColumn("Kostnad");
            table.AddColumn("Transportmetod");

            foreach (var connection in city.Connections)
            {
                table.AddRow(connection.Destination, connection.Distance.ToString(), connection.Time.ToString(), connection.Cost.ToString(), connection.TravelMethod);
            }
            AnsiConsole.Write(table);
        }
        public Connection GetConnection(City city, string destination)
        {
            return city.Connections.FirstOrDefault(c => c.Destination.Equals(destination, StringComparison.OrdinalIgnoreCase));
        }
    }
}
