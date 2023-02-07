using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ShowMemberActivityCommand : BaseCommand
    {
        public ShowMemberActivityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != 1)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 1, Received: {CommandParameters.Count}");
            }


            var memberName = CommandParameters[0];
            IMember member = Repository.GetMember(memberName);

            return member.PrintActivity();
        }

    }
}
