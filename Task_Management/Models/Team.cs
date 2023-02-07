using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    internal class Team : ITeam 
    {
        private string name;
        private IList<IMember> members = new List<IMember>(); //This is a list of all members in the team
        private IList<IBoard> boards = new List<IBoard>(); //This is a list of all boards in the team
        private readonly IList<ActivityHistory> activityHistory = new List<ActivityHistory>();

        public Team(string name) 
        {
            Validations.ValidateNameLength(name);
            this.Name = name;
            this.Members = members;
            this.Boards = boards;
        }

        public string Name
        {
            get { return this.name; }
            protected set
            {
                this.name = value;
            }
        }

        public IList<IBoard> Boards
        {
            get { return this.boards; }
            protected set
            {
                this.boards = value;
            }
        }

        public IList<IMember> Members
        {

            get { return members; }
            protected set
            {
                this.members = value;
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

        public void AddMember(IMember member)
        {
            this.members.Add(member);
        }
        public void RemoveMember(IMember member)
        {
            this.members.Remove(member);
        }

        public void AddBoard(IBoard board)
        {
            this.boards.Add(board);
        }

        public void AddToHistory(string content)
        {
            this.activityHistory.Add(new ActivityHistory(content));
        }

        public string PrintActivity()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Team [{this.Name}] activity history:");
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
