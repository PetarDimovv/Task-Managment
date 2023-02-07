using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public abstract class Task : ITask
    {
        protected IList<IComment> comments = new List<IComment>();
        protected readonly IList<ActivityHistory> activityHistory = new List<ActivityHistory>();

        protected Task(int id, string title, string description)
        {
            Validations.ValidateTitleLength(title);
            Validations.ValidateDescriptionLength(description);

            this.Id = id;
            this.Title = title;
            this.Description = description;
        }

        public string Title { get; }

        public string Description { get; }

        public int Id { get; }

        public IList<IComment> Comments
        {
            get
            {
                var copy = new List<IComment>(this.comments);
                return copy;
            }
        }
        public IList<ActivityHistory> ActivityHistory
        {
            get
            {
                var copy= new List<ActivityHistory>(this.activityHistory);
                return copy;
            }
        }
        public void AddComment(Comment comment)
        {
            this.comments.Add(comment);
        }
        public void AddToHistory(string content)
        {
            this.activityHistory.Add(new ActivityHistory(content));
        }
        public virtual string PrintActivity()
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (var activity in this.ActivityHistory)
            {
                sb.AppendLine($"{counter}. {activity.Content}");
                counter++;
            }
            return sb.ToString();
        }
        
        public string ShowAllComments()
        {
            if (!this.Comments.Any())
            {
                return "No comments have been added to this task";
            }

            var sb = new StringBuilder();
            sb.AppendLine("List of comments:");
            //var counter = 1;
            foreach (var comment in this.comments)
            {
                sb.AppendLine(this.comments.ToString());
            }
            return sb.ToString();
        }

        public abstract string ShowInfo();
        public abstract string DispleyMostImportantInfo();
        

    }
}
