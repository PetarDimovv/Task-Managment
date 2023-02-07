using System;
using System.Collections.Generic;
using System.Text;
//using Task_Management.Commands.Contracts;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;

namespace Task_Management.Commands.CreateCommands
{
    public class CreateMemberCommand : BaseCommand //Created by Pavel - 8/4 - around 10PM
    {
        public CreateMemberCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1," +
                    $" Received: {CommandParameters.Count}\r\n" +
                    $"To create a new member you need to type in:\r\n" +
                    $"create member / title ");
            }

            //Parameters:
            // [0] = Member's name

            var memberName = CommandParameters[0];
            if (Repository.MemberExists(memberName))
            {
                throw new InvalidUserInputException("A member with this name already exists");
            }
            var member = Repository.CreateMember(memberName);
            member.AddToHistory($"Member with name: {member.Name} has been created");
            return $"Member with name: {member.Name} was created";
        }
    }
}
