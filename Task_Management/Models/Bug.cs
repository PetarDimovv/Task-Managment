using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfBug;

namespace Task_Management.Models
{
    public class Bug : Task, IBug
    {
        private const char StepsSplitSymbol = ';';
        private const string ErrorMessage = "Bug's {0} is already at {1}";

        private IMember assignee;
        private StatusBug status;

        private readonly IList<string> listOfStepsToReproduceBug = new List<string>();

        public Bug(int id, string title, string description, string stepsToReproduce, Priority priority, Severity severity)
            : base(id, title, description)

        {
            this.listOfStepsToReproduceBug = stepsToReproduce.Split(StepsSplitSymbol).ToList();
            this.Priority = priority;
            this.Severity = severity;
            this.status = StatusBug.Active;
        }

        public Severity Severity { get; private set; }
        public Priority Priority { get; private set; }
        public StatusBug Status
        {
            get => this.status;
            private set
            {
                this.status = value;
            }
        }
        public IMember Assignee
        {
            get { return this.assignee; }
            set
            {
                this.assignee = value;
            }
        }
        public IList<string> ListOfStepsToReproduceBug
        {
            get
            {
                var copy = new List<string>(this.listOfStepsToReproduceBug);
                return copy;
            }
        }
        public void ChangeBugStatus(StatusBug status)
        {
            if (this.Status == status)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage , "status" , this.Status));
            }

            this.Status = status;
        }
        public void ChangeBugPriority(Priority priority)
        {
            if (this.Priority == priority)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "priority", this.Priority));
            }

            this.Priority = priority;
        }
        public void ChangeBugSeverity(Severity severity)
        {
            if (this.Severity == severity)
            {
                throw new InvalidOperationException(String.Format(ErrorMessage, "severity", this.Severity));
            }

            this.Severity = severity;
        }

        public override string PrintActivity()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Bug [{this.Title} | ID {this.Id}] activity history:");
            sb.AppendLine(base.PrintActivity());

            return sb.ToString();
        }


        public override string ShowInfo()
        {
            return $"Bug: [{this.Id}] \"{this.Title}\" | Description: {this.Description} | Assignee: {this.Assignee.Name}\r\n" +
                   $"Priority: {this.Priority} | Severity: {this.Severity} | Status: {this.Status}\r\n" +
                   $"      Steps to reproduce bug:\r\n" +
                   $"      {string.Join("; ",listOfStepsToReproduceBug)}";

        }

        public override string DispleyMostImportantInfo()
        {
            return $"Bug: [{this.Id}] \"{this.Title}\"| Assignee: {this.Assignee} | Status: {this.Status}";
        }
    }
}
