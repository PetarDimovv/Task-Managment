using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Contracts;
using System.Linq;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeBugSeverityCommand : BaseCommand
    {
        public ChangeBugSeverityCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change bug severity\" command / the ID of the bug / the new priority of the bug");
            }

            //Parameters:
            // [0] - Bug's Id
            // [1] - New Severity

            int id = int.Parse(CommandParameters[0]);
            Severity severity = Enum.Parse<Severity>(CommandParameters[1], ignoreCase: true);

            IBug bug = Repository.BugList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered bug with this ID");
            var prevSeverity = bug.Severity;
            bug.ChangeBugSeverity(severity);
            bug.AddToHistory($"The severity of item with ID {id} switched from {prevSeverity} to {severity}");

            this.Repository.GetMember(bug.Assignee.Name).AddToHistory
                ($"The severity of item with ID {id} switched from {prevSeverity} to {severity}");


            return $"The severity of item with ID {id} switched from {prevSeverity} to {severity}";
        }
    }

}
