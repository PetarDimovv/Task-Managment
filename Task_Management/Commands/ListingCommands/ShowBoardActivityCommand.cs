
using System.Collections.Generic;

using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowBoardActivityCommand : BaseCommand 
    {
        public ShowBoardActivityCommand(IList<string> commandParameters, IRepository repository)
                : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {CommandParameters.Count}");
            }


            var boardName = CommandParameters[0];
            IBoard board = Repository.GetBoard(boardName);

            return board.PrintActivity();
        }

    }

}

