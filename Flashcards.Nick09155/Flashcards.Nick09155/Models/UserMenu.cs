namespace Flashcards.Nick09155.Models;

public class UserMenu
{
    public UserMenu()
    {
        Study study = new Study();
    }
    
    public void GetUserInput()
    {
        // Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Start a Study Session");
            Console.WriteLine("Type 2 to Insert Record.");
            Console.WriteLine("Type 3 to Delete Record.");
            Console.WriteLine("Type 4 to Update Record.");
            Console.WriteLine("------------------------------------------\n");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    Console.WriteLine("Goodbye");
                    closeApp = true;
                    break;
                case "1":
                    // study.GetRecords();
                    Console.ReadKey();
                    break;
                // case "2":
                //     codingOperations.Insert();
                //     break;
                // case "3":
                //     codingOperations.Delete();
                //     break;
                // case "4":
                //     codingOperations.Update();
                //     break;
                default:
                    Console.WriteLine("Invalid Command, try again.");
                    break;
            }
        }
    }
}