using System.Data.SqlClient;
using Dapper;
using Flashcards.Nick09155.DTO;
using Flashcards.Nick09155.Models;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Nick09155.Services;

public class FlashcardService
{
    private readonly DatabaseService _databaseService;
    private string connectionString { get; }

    public FlashcardService(IConfiguration config)
    {
        _databaseService = new DatabaseService(config);
        connectionString = _databaseService.GetConnectionString();
    }
    
    internal void ViewAllFlashcards()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var sql = @"SELECT f.*, s.name as StackName 
            FROM FLASHCARDS f 
            LEFT JOIN STACKS s ON f.stackId = s.stackId";;
            var results = connection.Query<FlashcardDto>(sql).ToList();
            Helpers.DisplayFlashcards(results);
            Helpers.PressAnyKeyToContinue();
        }
    }

    internal void ViewFlashcardsByStack()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM STACKS";
            var results = connection.Query<Stack>(sql).ToList();
            Console.WriteLine("Which stack would you like to view flashcards for?");
            Helpers.DisplayTable(results);
            var userInput = Console.ReadLine();
            var flashcards = connection.Query<FlashcardDto>($"SELECT * FROM FLASHCARDS WHERE stackId = {userInput}").ToList();
            Helpers.DisplayFlashcards(flashcards);
            Helpers.PressAnyKeyToContinue();
        }
    }
    
    internal void RemoveFlashcard()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var sql = "SELECT * FROM FLASHCARDS";
            var results = connection.Query<FlashcardDto>(sql).ToList();
            Console.WriteLine("Which flashcard would you like to remove?");
            Helpers.DisplayFlashcards(results);
            var userInput = Console.ReadLine();
            var selectedFlashcard = results[int.Parse(userInput) - 1];
            var deleteSql = $"DELETE FROM FLASHCARDS WHERE Question = {selectedFlashcard.Question}";
            var exec = connection.Execute(deleteSql);
            if (exec > 0)
            {
                Console.WriteLine("Flashcard removed.");
                Helpers.PressAnyKeyToContinue();
            }
        }
    }
}