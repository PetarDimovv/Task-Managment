using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Size;
using Task_Management.Models.StatusOfFeedBack;

namespace Task_Management.Commands.ListingCommands
{
    public class ListFeedbacksCommand : BaseCommand
    {
        public ListFeedbacksCommand(IList<string> commandParameters, IRepository repository)
          : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validations.ValidateListNonEmpty(this.Repository.FeedbackList, "feedbacks");

            //gives the user a list of all feedbacks
            if (this.CommandParameters == null || !this.CommandParameters.Any())
            {
                var sb = new StringBuilder();
                var feedbacks = this.Repository.FeedbackList;

                sb.AppendLine("List of all feedbacks:");
                sb.AppendLine(ListFeedbacks(feedbacks));

                return sb.ToString();
            }

            //sorting the feedbacks list

            if (this.CommandParameters.Count == 2)
            {
                //Parameters:
                // [0] = Sort by 
                // [1] = By what state - title,rating

                string commandType = base.CommandParameters[0].ToLower();
                string byWhatState = base.CommandParameters[1].ToLower();

                if (commandType == "sort by")
                {
                    if (byWhatState == "title")
                    {
                        var feedbacks = this.Repository.FeedbackList.OrderBy(m => m.Title);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all feedbacks sorted by title:");
                        sb.AppendLine(ListFeedbacks(feedbacks));

                        return sb.ToString();

                    }
                    else if (byWhatState == "rating")
                    {
                        var feedbacks = this.Repository.FeedbackList.OrderBy(m => m.Rating);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all feedbacks sorted by rating:");
                        sb.AppendLine(ListFeedbacks(feedbacks));

                        return sb.ToString();

                    }
                }
            }

            //Filter list by feedback's status

            if (this.CommandParameters.Count == 3)
            {
                //Parameters:
                // [0] = Filter by
                // [1] = Status
                // [2] = State's "name"

                string commandType = base.CommandParameters[0].ToLower();
                string byWhatState = base.CommandParameters[1].ToLower();
                string stateName = base.CommandParameters[2];

                if (commandType == "filter by")
                {
                    if (byWhatState == "status")
                    {
                        var feedbacks = this.Repository.FeedbackList.Where(s => s.Status == Enum.Parse<StatusFeedback>(CommandParameters[2], ignoreCase: true));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all feedbacks filtered by status {stateName}");
                        sb.AppendLine(ListFeedbacks(feedbacks));
                    }
                }
            }

            throw new InvalidUserInputException($"Invalid user input for this command.\r\n" +
                   $"To get a list of feedbacks you must provide the \"list feedbacks\" command or: \r\n" +
                   $"      list feedbacks / sort by or filter by / feedback feature / *feature state you want to filter feedbacks by");
        }

        public string ListFeedbacks(IEnumerable<IFeedback> feedbacks)
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (Feedback feedback in feedbacks)
            {
                sb.AppendLine($"{counter}. {feedback.ShowInfo()}");
                counter++;
            }
            sb.AppendLine("===========================================");
            return sb.ToString();
        }

    }
}
