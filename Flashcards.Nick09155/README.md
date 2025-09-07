# [Flashcards](https://thecsharpacademy.com/project/14/flashcards)


## Requirements
- :white_check_mark: This is an application where the users will create Stacks of Flashcards.
- :white_check_mark: You'll need two different tables for stacks and flashcards. The tables should be linked by a foreign key.
- :white_check_mark: Stacks should have an unique name.
- :white_check_mark: Every flashcard needs to be part of a stack. If a stack is deleted, the same should happen with the flashcard.
- :white_check_mark: You should use DTOs to show the flashcards to the user without the Id of the stack it belongs to.
- :white_check_mark: When showing a stack to the user, the flashcard Ids should always start with 1 without gaps between them. If you have 10 cards and number 5 is deleted, the table should show Ids from 1 to 9.
- :white_check_mark: After creating the flashcards functionalities, create a "Study Session" area, where the users will study the stacks. All study sessions should be stored, with date and score.
- :white_check_mark: The study and stack tables should be linked. If a stack is deleted, it's study sessions should be deleted.
- :white_check_mark: The project should contain a call to the study table so the users can see all their study sessions. This table receives insert calls upon each study session, but there shouldn't be update and delete calls to it.

# Tech Stack

- .NET 9
- Spectre Console
- SQL Server
- Dapper

# Features

- Start a study session with a selected Flashcard Stack.
- View all past study sessions for a selected Flashcard Stack.
- Create, view and delete Stacks.
- Create, view and delete Flashcards within a selected Flashcard Stack.