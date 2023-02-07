using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ShowTeam_sActivityCommand : BaseCommand
    {
        public ShowTeam_sActivityCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        { 
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 1, Received: { this.CommandParameters.Count }");
            }
            var teamName = base.CommandParameters[0];
            ITeam team = this.Repository.GetTeam(teamName);
            return team.PrintActivity();
        }
    }
}
