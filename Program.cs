using System;
using System.Linq;
using TravelAssistant;
using Spectre.Console;
using Figgle;

class Program
{
    static void Main(string[] args)
    {
        // 1. Ladda in städer
        var cities = DataLoader.LoadCities();
        if (cities.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]Inga städer kunde läsas in. Kontrollera att cities.json är korrekt.[/]");
            return;
        }

        // 2. Visa en cool titel
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Travel Assistant").Color(ConsoleColor.Yellow));
        AnsiConsole.MarkupLine("[bold yellow]Välkommen till reseassistenten![/]");

        // 3. Välj startstad från lista
        var availableCities = cities.Select(c => c.Name).ToList();

        var startCityName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Välj vilken stad du vill resa ifrån från listan nedan:[/]")
                .PageSize(10)
                .AddChoices(availableCities)
        );

        var startCity = cities.FirstOrDefault(c => c.Name.Equals(startCityName, StringComparison.OrdinalIgnoreCase));

        if (startCity == null)
        {
            AnsiConsole.MarkupLine("[red]Staden hittades inte i systemet.[/]");
            return;
        }

        // 4. Visa möjliga destinationer från den valda startstaden
        AnsiConsole.MarkupLine($"[bold yellow]Du har valt {startCity.Name} som din startstad.[/]");
        AnsiConsole.MarkupLine("[bold yellow]Möjliga destinationer:[/]");

        var destinationTable = new Spectre.Console.Table();
        destinationTable.AddColumn("Destination");
        destinationTable.AddColumn("Avstånd (km)");
        destinationTable.AddColumn("Tid (min)");
        destinationTable.AddColumn("Kostnad (kr)");
        destinationTable.AddColumn("Transportmetod");

        foreach (var connection in startCity.Connections)
        {
            destinationTable.AddRow(
                connection.Destination,
                connection.Distance.ToString(),
                connection.Time.ToString(),
                connection.Cost.ToString(),
                connection.TravelMethod
            );
        }

        AnsiConsole.Write(destinationTable);

        // 5. Välj vilken stad man vill åka till
        var destinationCityName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold green]Välj vilken stad du vill resa till:[/]")
                .PageSize(10)
                .AddChoices(startCity.Connections.Select(c => c.Destination).ToList())
        );

        var selectedConnection = startCity.Connections.FirstOrDefault(c => c.Destination.Equals(destinationCityName, StringComparison.OrdinalIgnoreCase));

        if (selectedConnection == null)
        {
            AnsiConsole.MarkupLine("[red]Ingen direkt anslutning till denna stad finns tillgänglig.[/]");
            return;
        }

        // 6. Visa reseinformation
        AnsiConsole.MarkupLine($"[bold yellow]Du har valt att resa från {startCity.Name} till {destinationCityName}.[/]");

        var infoTable = new Spectre.Console.Table();
        infoTable.AddColumn("Parameter");
        infoTable.AddColumn("Värde");
        infoTable.AddRow("Avstånd (km)", selectedConnection.Distance.ToString());
        infoTable.AddRow("Tid (min)", selectedConnection.Time.ToString());
        infoTable.AddRow("Kostnad", selectedConnection.Cost.ToString());
        infoTable.AddRow("Transportmetod", selectedConnection.TravelMethod);

        AnsiConsole.Write(infoTable);

        AnsiConsole.Write(
         new Panel("[bold yellow]Trevlig resa och tack för att du använde ReseAssistenten![/]")
             .Border(BoxBorder.Double)
             );


     Console.ReadLine();
    }
}

