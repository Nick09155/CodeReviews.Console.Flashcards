namespace Flashcards.Nick09155.Models;

public class UserMenu
{
    private Study study;
    public UserMenu()
    {
        study = new Study();
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
            Console.WriteLine("Type 2 to View Study Sessions");
            Console.WriteLine("------------------------------------------\n");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    Console.WriteLine("Goodbye");
                    closeApp = true;
                    break;
                case "1":
                    study.StartStudySession();
                    // Console.ReadKey();
                    break;
                case "2":
                    study.ViewStudySessions();
                    break;
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