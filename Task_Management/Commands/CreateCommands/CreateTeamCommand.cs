using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;


namespace Task_Management.Commands
{
    public class CreateTeamCommand : BaseCommand
    {
        public CreateTeamCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (this.CommandParameters.Count < 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1," +
                    $" Received: {this.CommandParameters.Count}\r\n" +
                    $" To create a new team you need to type in:\r\n" +
                    $"create team / title");
            }


            string title = base.CommandParameters[0];

            if (Repository.TeamExists(title))
            {
                throw new InvalidUserInputException("A team with this name already exists.");
            }

            var newTeam = this.Repository.CreateTeam(title);
            newTeam.AddToHistory($"Team with name: {newTeam.Name} has been created.");
            return $"Team with title: {title} was created.";
        }
    }
}
