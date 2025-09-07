using Flashcards.Nick09155.Services;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Nick09155.Models;

public class UserMenu
{
    private StudySessionService _studySessionService;
    private StackService stackService;
    private FlashcardService flashcardService;
    public UserMenu(IConfiguration config)
    {
        _studySessionService = new StudySessionService(config);
        stackService = new StackService(config);
        flashcardService = new FlashcardService(config);
    }
    
    public void GetUserInput()
    {
        bool closeApp = false;
        bool stackSelected = false;
        bool flashcardSelected = false;
        while (closeApp == false)
        {
            if (stackSelected)
            {
                GetStackInput();
            }

            if (flashcardSelected)
            {
                GetFlashcardInput();
            }
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Start a Study Session");
            Console.WriteLine("Type 2 to View Study Sessions");
            Console.WriteLine("Type 3 to Go To Stack Menu");
            Console.WriteLine("Type 4 to Go To Flashcard Menu");
            Console.WriteLine("------------------------------------------\n");

            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    Console.WriteLine("Goodbye");
                    closeApp = true;
                    break;
                case "1":
                    _studySessionService.StartStudySession();
                    break;
                case "2":
                    _studySessionService.ViewStudySessions();
                    break;
                case "3":
                    stackSelected = true;
                    Console.Clear();
                    break;
                case "4":
                    flashcardSelected = true;
                    Console.Clear();
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
                default:
                    Console.WriteLine("Invalid Command, try again.");
                    break;
            }
        }
    }
    public void GetFlashcardInput()
    {
        bool flashcardSelected = true;
        while (flashcardSelected)
        {
            Console.WriteLine("\n\nSTACK MENU");
            Console.WriteLine("\nType 1 select which stack to view flashcards for");
            Console.WriteLine("Type 2 to view all flashcards");
            Console.WriteLine("Type 3 to delete a flashcard");
            Console.WriteLine("Type 0 to go back to Main Menu.");
            Console.WriteLine("------------------------------------------\n");
            
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "0":
                    flashcardSelected = false;
                    break;
                case "1":
                    flashcardService.ViewFlashcardsByStack();
                    break;
                case "2":
                    flashcardService.ViewAllFlashcards();
                    break;
                case "3":
                    flashcardService.RemoveFlashcard();
                    break;
                default:
                    Console.WriteLine("Invalid Command, try again.");
                    break;
            }
        }
    }
}