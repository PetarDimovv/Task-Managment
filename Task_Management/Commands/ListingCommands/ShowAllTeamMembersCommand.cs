using System;
using System.Collections.Generic;
using System.Text;
//using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowAllTeamMembersCommand : BaseCommand
    {
        public ShowAllTeamMembersCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (this.CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {this.CommandParameters.Count}");
            }

            var teamName = base.CommandParameters[0];
            //get team from repository

            ITeam team = this.Repository.GetTeam(teamName);

            if (team.Members.Count > 0)
            {
                var counter = 1;
                var sb = new StringBuilder();
                sb.AppendLine($"Listed members in team: {teamName}");

                foreach (var member in team.Members)
                {
                    sb.AppendLine($"{counter}. {member.Name}");
                    counter++;
                }

                return sb.ToString();
            }
            else
            {
                return "There are no registered people.";
            }



        }
    }
}
