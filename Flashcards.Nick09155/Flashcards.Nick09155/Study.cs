using System.Data.SqlClient;

namespace Flashcards.Nick09155;

public class Study
{
    public Study()
    {
        CreateTable();
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
}