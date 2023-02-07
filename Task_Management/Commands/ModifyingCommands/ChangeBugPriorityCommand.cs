using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.StatusOfBug;
using Task_Management.Models;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.Contracts;
using System.Linq;
using Task_Management.Models.Enums.Bug;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeBugPriorityCommand : BaseCommand
    {
        public ChangeBugPriorityCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change bug priority\" command / the ID of the bug / the new priority of the bug");
            }

            //Parameters:
            // [0] - Bug's Id
            // [1] - New Priority

            int id = int.Parse(CommandParameters[0]);
            Priority priority = Enum.Parse<Priority>(CommandParameters[1], ignoreCase: true);

            IBug bug = Repository.BugList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered bug with this ID");
            var prevPriority = bug.Priority;
            bug.ChangeBugPriority(priority);
            bug.AddToHistory($"The priority of item with ID {id} switched from {prevPriority} to {priority}");

            this.Repository.GetMember(bug.Assignee.Name).AddToHistory
                ($"The priority of item with ID {id} switched from {prevPriority} to {priority}");

            return $"The priority of item with ID {id} switched from {prevPriority} to {priority}";
        }
    }
}
