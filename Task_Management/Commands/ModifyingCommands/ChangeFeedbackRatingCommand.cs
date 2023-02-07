using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfFeedBack;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeFeedbackRatingCommand : BaseCommand
    {
        public ChangeFeedbackRatingCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }
        public override string Execute()
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change feedback rating\" command / the ID of the feedback / the new rating of the feedback");
            }


            int id = int.Parse(CommandParameters[0]);
            int rating = int.Parse(CommandParameters[1]);

            IFeedback feedback = Repository.FeedbackList.Where(b => b.Id == id).FirstOrDefault()
                ?? throw new InvalidUserInputException("There is not registered feedback with this ID");
            int oldRating = feedback.Rating;
            feedback.ChangeFeedbackRating(rating);
            feedback.AddToHistory($"The rating of item with ID {id} switched from {oldRating} to {rating}");
            return $"The rating of item with ID {id} switched from {oldRating} to {rating}";
        }
    }
}



