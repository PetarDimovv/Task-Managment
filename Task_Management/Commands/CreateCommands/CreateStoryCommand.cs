using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.Size;

namespace Task_Management.Commands.CreateCommands
{
    public class CreateStoryCommand : BaseCommand
    {
        public CreateStoryCommand(IList<string> commandParameters, IRepository repository)
                : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (CommandParameters.Count < 6)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 6," +
                    $" Received: {CommandParameters.Count}\r\n" +
                    $"To create a new story you need to type in: \r\n" +
                    $"create story / board name / title / description / priority / size / status");
            }

            //Parameters:
            // [0] = BoardName
            // [1] = Title
            // [2] = Description
            // [3] = Priority
            // [4] = Size
            // [5] = Status
            string boardName = CommandParameters[0];    
            string title = CommandParameters[1];
            string description = CommandParameters[2];
            Priority priority = Enum.Parse<Priority>(CommandParameters[3], ignoreCase: true);
            SizeStory size = Enum.Parse<SizeStory>(CommandParameters[4], ignoreCase: true);
            StatusStory status = Enum.Parse<StatusStory>(CommandParameters[5], ignoreCase: true);

            if (Repository.TaskExists(title))
            {
                throw new InvalidUserInputException("A story with this name already exists.");
            }

            var createStory = Repository.CreateStory(title,description,priority,size, status);
            createStory.AddToHistory($"A new story [Title: {createStory.Title} | ID:{createStory.Id}] was created.");

            IBoard board = this.Repository.GetBoard(boardName);
            board.AddTaskToBoard(createStory);
            board.AddToHistory($"A new feedback [Title: {createStory.Title} | ID: {createStory.Id}] in board '{boardName}' was created.");

            return $"A new story [Title: {createStory.Title} | ID:{createStory.Id}] was created.";

        }
    }
}

