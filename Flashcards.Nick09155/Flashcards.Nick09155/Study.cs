using System.Data.SqlClient;
using Dapper;
using Flashcards.Nick09155.DTO;
using Flashcards.Nick09155.Models;

namespace Flashcards.Nick09155;

public class Study
{
    private string ConnectionString { get; }
    public Study()
    {
        ConnectionString = "Server=localhost;Database=master;User Id=sa;Password=Apples123;";
        CreateTable();
        CreateFlashcards();
    }
    private void CreateTable()
    {
        var connectionString = ConnectionString;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = @"
DROP TABLE IF EXISTS FLASHCARDS;
DROP TABLE IF EXISTS STUDYSESSION;
DROP TABLE IF EXISTS STACKS;
CREATE TABLE STACKS (
    stackID INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    description VARCHAR(MAX)
);


CREATE TABLE FLASHCARDS (
    flashcardID INT IDENTITY(1,1) PRIMARY KEY,
    question VARCHAR(MAX) NOT NULL,
    answer VARCHAR(MAX) NOT NULL,
    stackID INT NOT NULL,
    FOREIGN KEY (stackID) REFERENCES STACKS(stackID) ON DELETE CASCADE
);

CREATE TABLE STUDYSESSION (
    date DATE NOT NULL,
    score INT NOT NULL,
    stackID INT NOT NULL,
    FOREIGN KEY (stackID) REFERENCES STACKS(stackID) ON DELETE CASCADE
);
";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }


    private void CreateFlashcards()
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            var insertCmd = connection.CreateCommand();
            insertCmd.CommandText += @"
INSERT INTO STACKS (name, description) VALUES ('Math', 'Basic math concepts');
INSERT INTO STACKS (name, description) VALUES ('Science', 'General science facts');

INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is 2+2?', '4', 1);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is H2O?', 'Water', 2);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is 5 x 6?', '30', 1);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is the square root of 16?', '4', 1);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is 12 divided by 3?', '4', 1);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is the value of pi (approx)?', '3.14', 1);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is 7 + 8?', '15', 1);

INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What planet is known as the Red Planet?', 'Mars', 2);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What gas do plants breathe in?', 'Carbon dioxide', 2);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is the boiling point of water (Celsius)?', '100', 2);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What organ pumps blood?', 'Heart', 2);
INSERT INTO FLASHCARDS (question, answer, stackID) VALUES ('What is the chemical symbol for gold?', 'Au', 2);
";
            insertCmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    internal void StartStudySession()
    {
        Console.WriteLine("Starting Study Session...");
        using (var connection = new SqlConnection(ConnectionString))
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
            
            var flashcards = connection.Query<FlashcardDTO>("SELECT * FROM FLASHCARDS WHERE stackID = @StackId", new { StackId = userInput }).ToList();

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
            // Valide Input
            var chosenStack = stacks.Where(s => s.StackId.ToString() == userInput).FirstOrDefault();
            
            string today = DateTime.Now.ToString("MM-dd-yy");
            // var insertCmd = connection.CreateCommand();
            var sql = $"INSERT INTO STUDYSESSION (date, score, stackID) VALUES ('{today}', '{score}', '{userInput}')";
            connection.Execute(sql);
            // insertCmd.Execute();
        }
    }

    internal void ViewStudySessions()
    {
        using (var connection = new SqlConnection(ConnectionString))
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