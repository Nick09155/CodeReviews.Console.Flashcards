using System.Data.SqlClient;
using Dapper;
using Flashcards.Nick09155.Models;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Nick09155.Services;

public class StackService
{
    private readonly DatabaseService _databaseService;
    private string connectionString { get; }
    
    public StackService(IConfiguration config)
    {
        _databaseService = new DatabaseService(config);
        connectionString = _databaseService.GetConnectionString();
    }


    internal void ViewStacks()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM STACKS";
            var results = connection.Query<Stack>(sql).ToList();
            Console.Clear();
            Console.WriteLine("Stacks:");
            Helpers.DisplayTable(results);
            Helpers.PressAnyKeyToContinue();
        }
    }

    internal void AddStack()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            Console.WriteLine("What would you like to name the new stack?");
            var nameInput = Console.ReadLine();
            
            Console.WriteLine("Please provide a description for the new stack:");
            var descriptionInput = Console.ReadLine();
            
            var sql = "INSERT INTO STACKS (name, description) VALUES (@Name, @Description)";
            var exec = connection.Execute(sql, new { Name = nameInput, Description = descriptionInput });
            if (exec > 0)
            {
                Console.WriteLine("\nStack added");
                Helpers.PressAnyKeyToContinue();
            }
        }
    }

    internal void RemoveStack()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM STACKS";
            var results = connection.Query<Stack>(sql).ToList();
            Console.Clear();
            Console.WriteLine("Which stack would you like to remove? Choose a number.");
            Helpers.DisplayTable(results);

            var userInput = Console.ReadLine();
            var selectedStack = results.Where(x => x.StackId.ToString() == userInput);
            var stackName = selectedStack.FirstOrDefault().Name;
            var deleteSql = "DELETE FROM STACKS WHERE stackId = @StackId";
            var exec = connection.Execute(deleteSql, new { StackId = Convert.ToInt32(userInput) });
            if (exec > 0)
            {
                Console.WriteLine($"Stack: {stackName} removed.");
                Helpers.PressAnyKeyToContinue();
            }
        }
    }
}