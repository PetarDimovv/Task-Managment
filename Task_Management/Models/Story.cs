using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Size;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.Enums;

namespace Task_Management.Models
{
    public class Story : Task, IStory
    {
        private const string ErrorMessage = "Story's {0} is already at {1}";

        private IMember assignee;

        public Story(int id, string title, string description, Priority priority, SizeStory size, StatusStory status)
        : base(id, title, description)
        {
            this.Priority = priority;
            this.Size = size;
            this.Status = status;
        }

        public Priority Priority { get; private set; }
        public SizeStory Size { get; private set; }
        public StatusStory Status { get; private set; }

        public IMember Assignee
        {
            get { return this.assignee; }
            set
            {
                this.assignee = value;
            }
        }

        public void ChangeStoryPriority(Priority priority)
        {
            if (this.Priority == priority)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "priority",this.Priority));
            }

            this.Priority = priority;
        }
        public void ChangeStoryStatus(StatusStory status)
        {
            if (this.Status == status)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "status", this.Status));
            }

            this.Status = status;
        }
        public void ChangeStorySize(SizeStory size)
        {
            if (this.Size == size)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "size", this.Size));
            }

            this.Size = size;
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
            return $"Story: [{this.Id}] \"{this.Title}\" | Description: {this.Description} | Assignee: {this.Assignee.Name}\r\n " +
                   $"Priority: {this.Priority} | Size: {this.Size} | Status: {this.Status}";

        }
        public override string DispleyMostImportantInfo()
        {
            return $"Story: [{this.Id}] \"{this.Title}\" | Assignee: {this.Assignee} | Status: {this.Status}";
        }
    }
}
