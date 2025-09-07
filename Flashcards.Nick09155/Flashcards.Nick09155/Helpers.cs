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
        foreach (var item in list)
        {
            table.AddRow($"{item.StackId}", $"[green]{item.Name}[/]",$"[yellow]{item.Description}[/]");
        }
            
        AnsiConsole.Write(table);
    }
    
    internal static void DisplayFlashcards(List<DTO.FlashcardDto> list)
    {
        var table = new Table();
        table.AddColumn("#");
        table.AddColumn(new TableColumn("Stack").Centered());
        table.AddColumn(new TableColumn("Question").Centered());
        int id = 1;
        foreach (var item in list)
        {
            table.AddRow($"{id}", $"[yellow]{item.StackName}[/]", $"[blue]{item.Question}[/]");
            id++;
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