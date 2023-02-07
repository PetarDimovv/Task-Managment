using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfFeedBack;
using Task_Management.Models.Size;

namespace Task_Management.Commands.CreateCommands
{
    public class CreateFeedbackCommand : BaseCommand
    {
        public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository)
                : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (CommandParameters.Count < 5)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 5," +
                    $" Received: {CommandParameters.Count}\r\n" +
                    $"To create a new feedback you need to type in: \r\n" +
                    $"create feedback / board name / title / description /  rating / status");
            }

            //Parameters:
            // [0] = BoardName
            // [1] = Title
            // [2] = Description
            // [3] = Rating
            // [4] = Status
            string boardName = CommandParameters[0];
            string title = CommandParameters[1];
            string description = CommandParameters[2];
            int rating = int.Parse(CommandParameters[3]);
            StatusFeedback status = Enum.Parse<StatusFeedback>(CommandParameters[4],ignoreCase: true);

            if (Repository.TaskExists(title))
            {
                throw new InvalidUserInputException("A feedback with this name already exists.");
            }

            var newFeedback = Repository.CreateFeedback(title, description, rating, status);
            newFeedback.AddToHistory($"A new feedback [Title: {newFeedback.Title} | ID:{newFeedback.Id}] was created.");

            IBoard board = this.Repository.GetBoard(boardName);
            board.AddTaskToBoard(newFeedback);
            board.AddToHistory($"A new feedback [Title: {newFeedback.Title} | ID: {newFeedback.Id}] in board '{boardName}' was created.");


            return $"A new feedback [Title: {newFeedback.Title} | ID:{newFeedback.Id}] was created.";
        }
    }
}
