using System;
using System.Text;
using System.Collections.Generic;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfFeedBack;
using Task_Management.Models.Enums;
using Task_Management.Models.StatusOfBug;

namespace Task_Management.Models
{
    internal class Feedback : Task, IFeedback
    {
        private const string ErrorMessage = "Feedback's {0} is already at {1}";
        private int rating;

        public Feedback(int id, string title, string description, int rating, StatusFeedback status)
            : base(id, title, description)
        {
            this.rating = rating;
            this.Status = status;
        }

        public int Rating { get; private set; }
        public StatusFeedback Status { get; private set; }
        public void ChangeFeedbackStatus(StatusFeedback status)
        {
            if (this.Status == status)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage , "status" , this.Status ));
            }

            this.Status = status;
        }
        public void ChangeFeedbackRating(int rating)
        {
            if (this.Rating == rating)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "rating", this.Rating));
            }

            this.Rating = rating;
        }
        public override string PrintActivity()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Story [{this.Title} | ID {this.Id}] activity history:");
            sb.AppendLine(base.PrintActivity());

            return sb.ToString();
        }
        public override string ShowInfo()
        {
            return $"Feedback: [{this.Id}] \"{this.Title}\" | Description: {this.Description} \r\n" +
                   $"Rating: {this.Rating}| Status: {this.Status}";
        }
        public override string DispleyMostImportantInfo()
        {
            return $"Feedback: [{this.Id}] \"{this.Title}\" | Status: {this.Status}";
        }
    }
}
