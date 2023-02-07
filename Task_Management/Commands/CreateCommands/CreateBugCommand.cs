using System;
using System.Collections.Generic;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;

namespace Task_Management.Commands.CreateCommands
{
    public class CreateBugCommand : BaseCommand
    {
        public CreateBugCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count < 6)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 6," +
                    $" Received: {CommandParameters.Count} \r\n" +
                    $"To create a new bug you need to type in: \r\n" +
                    $"create bug / board name / title / description / steps to reproduce it / priority / severity");
            }

            //Parameters:
            // [0] = BoardName
            // [1] = Title
            // [2] = Description
            // [3] = Steps to reproduce
            // [4] = Priority
            // [5] = Severity
            string boardName = CommandParameters[0];
            string title = CommandParameters[1];
            string description = CommandParameters[2];
            string stepsToReproduce = CommandParameters[3];
            Priority priority = Enum.Parse<Priority>(CommandParameters[4], ignoreCase: true);
            Severity severity = Enum.Parse<Severity>(CommandParameters[5], ignoreCase: true);

            if (Repository.TaskExists(title))
            {
                throw new InvalidUserInputException("A bug with this name already exists.");
            }
            var newBug = Repository.CreateBug(title, description, stepsToReproduce, priority, severity);
            newBug.AddToHistory($"A new bug [Title: {newBug.Title} | ID:{newBug.Id}] in board '{boardName}' was created.");

            IBoard board = this.Repository.GetBoard(boardName);
            board.AddTaskToBoard(newBug);
            board.AddToHistory($"A new bug [Title: {newBug.Title} | ID: {newBug.Id}] in board '{boardName}' was created.");

            return $"A new bug [Title: {newBug.Title} | ID:{newBug.Id}] in board '{boardName}' was created.";

        }
    }
}
