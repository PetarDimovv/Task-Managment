using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowAllMembersCommand : BaseCommand
    {
        public ShowAllMembersCommand(IRepository repository) : base(repository)
        {
        }

        public override string Execute()
        {
            if (Repository.MemberList.Count > 0)
            {
                var counter = 1;
                var sb = new StringBuilder();
                sb.AppendLine("Listed members:");

                foreach (var member in Repository.MemberList)
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
