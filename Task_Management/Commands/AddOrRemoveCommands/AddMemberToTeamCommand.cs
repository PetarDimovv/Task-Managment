using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;

namespace Task_Management.Commands.AddCommands
{
    public class AddMemberToTeamCommand : BaseCommand
    {
        public AddMemberToTeamCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2," +
                    $" Received: {CommandParameters.Count}");
            }

            //Parameters:
            // [0] - Member's name
            // [1] - Team's name

            string memberName = CommandParameters[0];
            string teamName = CommandParameters[1];
            IMember member = base.Repository.GetMember(memberName);
            ITeam team = base.Repository.GetTeam(teamName);

            if (this.Repository.IsATeamMember(member,team))
            {
                throw new InvalidOperationException($"Member {memberName} is already in {teamName}");
            }

            team.AddMember(member);
            member.AddToHistory($"Member {memberName} has been added to {teamName}");
            team.AddToHistory($"Member {memberName} has been added to {teamName}");
            
            return $"Member {memberName} has been added to {teamName}";
        }
    }
}
