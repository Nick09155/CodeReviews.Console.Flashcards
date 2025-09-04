using System.Data.SqlClient;

namespace Flashcards.Nick09155;

public class Study
{
    public Study()
    {
        CreateTable();
        CreateFlashcards();
    }
    private void CreateTable()
    {
        Console.WriteLine("Creating table...");
        var connectionString = "Server=localhost;Database=master;User Id=sa;Password=Apples123;";
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
    FOREIGN KEY (stackID) REFERENCES STACKS(stackID)
);
";
            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
    }


    private void CreateFlashcards()
    {
        Console.WriteLine("Creating flashcards...");
        var connectionString = "Server=localhost;Database=master;User Id=sa;Password=Apples123;";
        using (var connection = new SqlConnection(connectionString))
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

    private void StartStudySession()
    {
        
    }
}