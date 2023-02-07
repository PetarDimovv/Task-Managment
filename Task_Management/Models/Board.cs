using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class Board : IBoard
    {
        private readonly IList<ITask> boardTasks = new List<ITask>();
        private readonly IList<ActivityHistory> activityHistory = new List<ActivityHistory>();

        private string name;
        public Board(string name)
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
        public IList<ITask> BoardTasks
        {
            get
            {
                var copy = new List<ITask>(this.boardTasks);
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

        public void AddTaskToBoard(ITask task)
        {
            this.boardTasks.Add(task);
        }
        public void AddToHistory(string content)
        {
            this.activityHistory.Add(new ActivityHistory(content));
        }
        public string PrintActivity()
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
    }
}
