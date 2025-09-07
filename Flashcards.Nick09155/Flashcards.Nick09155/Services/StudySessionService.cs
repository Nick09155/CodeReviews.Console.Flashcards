using System.Data.SqlClient;
using Dapper;
using Flashcards.Nick09155.DTO;
using Flashcards.Nick09155.Models;
using Flashcards.Nick09155.Services;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Nick09155;

public class StudySessionService
{
    private readonly DatabaseService _databaseService;
    private string connectionString { get; }
    public StudySessionService(IConfiguration config)
    {
        _databaseService = new DatabaseService(config);
        connectionString = _databaseService.GetConnectionString();
    }

    internal void StartStudySession()
    {
        Console.WriteLine("Starting Study Session...");
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var stacks = connection.Query<Stack>("SELECT * FROM STACKS").ToList();
            
            int score = connection.Query<int>("SELECT Score FROM STUDYSESSION").SingleOrDefault();
            Console.WriteLine("Your current score is: " + score);
            Console.WriteLine("Pick a Stack: ");
            foreach (var stack in stacks)
            {
                Console.WriteLine($"{stack.StackId}: {stack.Name} ({stack.Description})");
            }
            string userInput = Console.ReadLine();
            
            var flashcards = connection.Query<FlashcardDto>("SELECT * FROM FLASHCARDS WHERE stackID = @StackId", new { StackId = userInput }).ToList();

            foreach (var flashcard in flashcards)
            {
                Console.WriteLine($"{flashcard.Question}");
                var userAnswer = Console.ReadLine();
                if (userAnswer == flashcard.Answer)
                {
                    score++;
                    Console.WriteLine("Correct!, your score is now: " + score);
                }
                else
                {
                    Console.WriteLine("Incorrect answer. The correct answer is: " + flashcard.Answer);
                }
            }
                Console.WriteLine($"Study session finished. Your score is: {score}.");
            
            string today = DateTime.Now.ToString("MM-dd-yy");
            var sql = $"INSERT INTO STUDYSESSION (date, score, stackID) VALUES ('{today}', '{score}', '{userInput}')";
            connection.Execute(sql);
        }
    }

    internal void ViewStudySessions()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var sql = "SELECT * FROM STUDYSESSION";
            var results = connection.Query<StudySession>(sql).ToList();
            Console.WriteLine("Sessions:");
            Console.WriteLine("---------------------------");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.Date}: {result.Score}");
            }
            Console.WriteLine("---------------------------");
            Console.ReadKey();
        }
    }
}