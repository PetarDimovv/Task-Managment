using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Member : IMember
    {
        private IList<IAssignableTask> memberTasks = new List<IAssignableTask>();
        private readonly IList<ActivityHistory> activityHistory = new List<ActivityHistory>();

        private string name;
        public Member(string name)
        {
            this.Name = name;
        }
        public string Name
        {
            get { return this.name; }
            private set
            {
                Validations.ValidateNameLength(value);
                this.name = value;
            }
        }
            
        public IList<IAssignableTask> MemberTasks
        { 
            get 
            {
                var copy = new List<IAssignableTask>(this.memberTasks);
                return copy;
            }
        }
        public IList<ActivityHistory> ActivityHistory
        { 
            get 
            {
                var copy = new List<ActivityHistory>(this.activityHistory);
                return copy;
            }
            
        }
        public void AddToHistory(string content)
        {
            this.activityHistory.Add(new ActivityHistory(content));
        }

        public void AddTask(IAssignableTask task)
        {
            this.memberTasks.Add(task);
        }
        public void RemoveTask(IAssignableTask task)
        {
            this.memberTasks.Remove(task);
        }

        public string PrintActivity()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Member [{this.Name}] activity history:");
            var counter = 1;
            foreach (var activity in this.ActivityHistory)
            {
                sb.AppendLine($"{counter}. {activity.Content}");
                counter++;
            }

            return sb.ToString();
        }

    }
}
