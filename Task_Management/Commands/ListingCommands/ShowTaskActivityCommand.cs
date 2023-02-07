using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowTaskActivityCommand : BaseCommand
    {
        public ShowTaskActivityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {CommandParameters.Count}");
            }

            //Parameters:
            // [0] - Task's Id

            int id = int.Parse(CommandParameters[0]);
            ITask task = Repository.FindTaskByID(id);

            return task.PrintActivity();
        }
    }
}
