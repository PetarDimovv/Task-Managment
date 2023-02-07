using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfFeedBack;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeFeedbackStatusCommand : BaseCommand
    {
        public ChangeFeedbackStatusCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute()
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change feedback status\" command / the ID of the feedback / the new status of the feedback");
            }


            int id = int.Parse(CommandParameters[0]);
            StatusFeedback newStatus = Enum.Parse<StatusFeedback>(CommandParameters[1], ignoreCase: true);

            IFeedback feedback = Repository.FeedbackList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered feedback with this ID");
            var status = feedback.Status;
            feedback.ChangeFeedbackStatus(newStatus);
            feedback.AddToHistory($"The status of item with ID {id} switched from {status} to {newStatus}");
            return $"The status of item with ID {id} switched from {status} to {newStatus}";
        }
    }
}
