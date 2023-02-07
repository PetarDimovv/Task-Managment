using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands
{
    public class ShowAllTeamsCommand : BaseCommand //Created by Pavel - 8/4 - around 10PM
    {
        public ShowAllTeamsCommand(IRepository repository) : base(repository)
        {
        }

        public override string Execute()
        {
            if (this.Repository.TeamsList.Count != 0)
            {
                var counter = 1;
                var sb = new StringBuilder();
                sb.AppendLine("Listed teams:");
                

                foreach (var teamName in base.Repository.TeamsList)
                {
                    sb.AppendLine($"{counter}. {teamName.Name}");
                    counter++;
                }

                return sb.ToString();
            }
            else
            {
                return $"There are no teams yet.";
            }

           


            //Not sure how to show the teams acth ;//
        }
    }
}

