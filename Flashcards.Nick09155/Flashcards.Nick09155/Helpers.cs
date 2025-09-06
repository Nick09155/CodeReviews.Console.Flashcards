using Spectre.Console;

namespace Flashcards.Nick09155;

public class Helpers
{
    internal static void DisplayTable(List<Models.Stack> list)
    {
        var table = new Table();
        table.AddColumn("#");
        table.AddColumn(new TableColumn("Name").Centered());
        table.AddColumn(new TableColumn("Description").Centered());
        // table.AddColumn(new TableColumn("Duration").Centered());
        foreach (var item in list)
        {
            table.AddRow($"{item.StackId}", $"[green]{item.Name}[/]",$"[yellow]{item.Description}[/]");
        }
            
        AnsiConsole.Write(table);
    }
    
    internal static void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}