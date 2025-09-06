using Flashcards.Nick09155.Services;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Nick09155.Models;

public class UserMenu
{
    private Study study;
    private StackService stackService;
    public UserMenu(IConfiguration config)
    {
        study = new Study(config);
        stackService = new StackService(config);
    }
    
    public void GetUserInput()
    {
        // Console.Clear();
        bool closeApp = false;
        bool stackSelected = false;
        while (closeApp == false)
        {
            if (stackSelected)
            {
                GetStackInput();
            }
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Start a Study Session");
            Console.WriteLine("Type 2 to View Study Sessions");
            Console.WriteLine("Type 3 to Go To Stack Menu");
            Console.WriteLine("Type 4 to Create Stack");
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
                case "3":
                    stackSelected = true;
                    Console.Clear();
                    // stackService.ViewStacks();
                    break;
                case "4":
                    stackService.AddStack();
                    break;
                default:
                    Console.WriteLine("Invalid Command, try again.");
                    break;
            }
        }
    }

    public void GetStackInput()
    {
        bool stackSelected = true;
        while (stackSelected)
        {
            Console.WriteLine("\n\nSTACK MENU");
            Console.WriteLine("\nType 1 view available stacks");
            Console.WriteLine("Type 2 to create a new stack");
            Console.WriteLine("Type 3 to delete a stack");
            // Console.WriteLine("Type 4 to Create Stack");
            Console.WriteLine("Type 0 to go back to Main Menu.");
            
            Console.WriteLine("------------------------------------------\n");
            
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    stackSelected = false;
                    break;
                case "1":
                    stackService.ViewStacks();
                    break;
                case "2":
                    stackService.AddStack();
                    break;
                case "3":
                    stackService.RemoveStack();
                    break;
                // case "4":
                //     stackService.AddStack();
                //     break;
                default:
                    Console.WriteLine("Invalid Command, try again.");
                    break;
            }
        }
    }
}