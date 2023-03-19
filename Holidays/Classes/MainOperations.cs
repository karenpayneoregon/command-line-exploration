using System.Text.Json;
using Nager.Date;
using Nager.Date.Extensions;
using static System.DateTime;

namespace Holidays.Classes;

internal class MainOperations
{
    public static async Task Run(string countryCode)
    {
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        using var httpClient = new HttpClient();
        
        /*
         * Here we get holidays for the current year, optionally setup another parameter to the program which
         * is not required for the year, if null, use the current year.
         */
        var response = await httpClient.GetAsync($"https://date.nager.at/api/v3/publicholidays/{
            Now.Year}/{countryCode}");

        if (response.IsSuccessStatusCode)
        {
            await using var jsonStream = await response.Content.ReadAsStreamAsync();

            // Distinct is used as there were duplicate entries
            var publicHolidays = 
                JsonSerializer.Deserialize<PublicHoliday[]>(jsonStream, jsonSerializerOptions)
                    !.Distinct(PublicHoliday.DateComparer);

            Console.WriteLine();
            AnsiConsole.MarkupLine($"[yellow]Holidays[/] for {countryCode} for {Now.Year}");

            var table = new Table()
                .RoundedBorder()
                .AddColumn("[b]Name[/]")
                .AddColumn("[b]Date[/]")
                .AddColumn("[b]Weekend[/]")
                .Alignment(Justify.Left)
                .BorderColor(Color.CadetBlue);

            foreach (var holiday in publicHolidays!)
            {

                Enum.TryParse(countryCode, true, out CountryCode code);
                if (holiday.Date > Now)
                {
                    if (holiday.Date.IsWeekend(code))
                    {
                        table.AddRow($"[cyan]{holiday.Name}[/]", $"[white]{holiday.Date:MM/dd/yyyy}[/]", "[yellow]*[/]");
                    }
                    else
                    {
                        table.AddRow($"[cyan]{holiday.Name}[/]", $"[white]{holiday.Date:MM/dd/yyyy}[/]");
                    }
                }
                else
                {
                    if (holiday.Date.IsWeekend(code))
                    {
                        table.AddRow($"[cyan]{holiday.Name}[/]", $"[white]{holiday.Date:MM/dd/yyyy}[/]", "[yellow]*[/]");
                    }
                    else
                    {
                        table.AddRow($"{holiday.Name}", $"{holiday.Date:MM/dd/yyyy}");
                    }
                }
            }


            AnsiConsole.Write(table);

        }
        else
        {
            AnsiConsole.MarkupLine($"Country code [white on red]{countryCode}[/] found no holidays, may be an invalid code.");
        }

    }
}