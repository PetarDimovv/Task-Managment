using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfBug;
using Task_Management.Models.StatusOfFeedBack;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeBugStatusCommand : BaseCommand
    {
        public ChangeBugStatusCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change bug status\" command / the ID of the bug / the new status of the bug");
            }

            //Parameters:
            // [0] - Bug's ID
            // [1] - New Status

            int id = int.Parse(CommandParameters[0]);
            StatusBug status = Enum.Parse<StatusBug>(CommandParameters[1],ignoreCase: true);

            IBug bug = Repository.BugList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered bug with this ID");
            var prevStatus = bug.Status;
            bug.ChangeBugStatus(status);
            bug.AddToHistory($"The status of item with ID {id} switched from {prevStatus} to {status}");
            this.Repository.GetMember(bug.Assignee.Name).AddToHistory
                ($"The status of item with ID {id} switched from {prevStatus} to {status}");
            
            return $"The status of item with ID {id} switched from {prevStatus} to {status}";
        }
    }
}
