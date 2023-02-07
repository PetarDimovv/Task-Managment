using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfBug;
using Task_Management.Models.StatusOfFeedBack;
using Task_Management.Models.Size;
using Task_Management.Models.Enums;

namespace Task_Management.Core.Contracts
{
    public class Repository : IRepository
    {
        #region Fields
        private int uniqueID;

        protected readonly IList<ITeam> teamsList = new List<ITeam>();
        protected readonly IList<IMember> membersList = new List<IMember>();
        protected readonly IList<IBoard> boardList = new List<IBoard>();

        protected readonly IList<IBug> bugList = new List<IBug>();
        protected readonly IList<IFeedback> feedbackList = new List<IFeedback>();
        protected readonly IList<IStory> storyList = new List<IStory>();
        #endregion

        #region Ctor
        public Repository()
        {
            this.uniqueID = 1;

        }
        #endregion

        #region Properties
        public IList<ITeam> TeamsList
        {
            get
            {
                var teamCopy = new List<ITeam>(this.teamsList);
                return teamCopy;
            }
        }
        public IList<IBoard> BoardList
        {
            get
            {
                var boardCopy = new List<IBoard>(this.boardList);
                return boardCopy;
            }
        }
        public IList<IMember> MemberList
        {
            get
            {
                var membersCopy = new List<IMember>(this.membersList);
                return membersCopy;
            }
        }
        public IList<IBug> BugList
        {
            get
            {
                var bugCopy = new List<IBug>(this.bugList);
                return bugCopy;
            }
        }
        public IList<IStory> StoryList
        {
            get
            {
                var storyCopy = new List<IStory>(this.storyList);
                return storyCopy;
            }
        }
        public IList<IFeedback> FeedbackList
        {
            get
            {
                var feedbackCopy = new List<IFeedback>(this.feedbackList);
                return feedbackCopy;
            }
        }


        #endregion

        #region Methods

        public IList<ITask> GetAllTasksList()
        {
            var allTasksList = new List<ITask>(bugList);
            allTasksList.AddRange(storyList);
            allTasksList.AddRange(FeedbackList);
            return allTasksList;
        }
        public IList<IAssignableTask> GetAllTasksWithAssigneeList()
        {
            var allTasksWithAssigneeList = new List<IAssignableTask>(bugList);
            allTasksWithAssigneeList.AddRange(storyList);
            return allTasksWithAssigneeList;
        }
        public ITeam CreateTeam(string title)
        {
            ITeam team = new Team(title);
            this.teamsList.Add(team);
            return team;
        }
        public IMember CreateMember(string name)
        {
            IMember member = new Member(name);
            this.membersList.Add(member);
            return member;
        }

        public IBoard CreateBoard(string name)
        {
            IBoard board = new Board(name);
            this.boardList.Add(board);
            return board;
        }

        public IStory CreateStory(string title, string description, Priority priority, SizeStory size, StatusStory status)
        {
            IStory story = new Story(uniqueID, title, description, priority, size, status);
            this.storyList.Add(story);
            this.uniqueID++;
            return story;
        }

        public IFeedback CreateFeedback(string title, string description, int rating, StatusFeedback status)
        {
            IFeedback feedback = new Feedback(uniqueID, title, description, rating, status);
            this.feedbackList.Add(feedback);
            this.uniqueID++;
            return feedback;
        }

        public IBug CreateBug(string title, string description, string stepsToReproduce, Priority priority, Severity severity)
        {
            var bug = new Bug(uniqueID, title, description, stepsToReproduce, priority, severity);
            this.bugList.Add(bug);
            uniqueID++;
            return bug;
        }
        public IComment CreateComment(string content, string author)
        {
            var comment = new Comment(content, author);
            return comment;
        }
        public ITask FindTaskByID(int id)
        {
            foreach (Bug bug in this.bugList)
            {
                if (bug.Id == id)
                {
                    return bug;
                }
            }
            foreach (IStory story in this.storyList)
            {
                if (story.Id == id)
                {
                    return story;
                }
            }
            foreach (IFeedback feedback in this.feedbackList)
            {
                if (feedback.Id == id)
                {
                    return feedback;
                }
            }

            throw new EntityNotFoundException($"Task with id: {id} was not found!");
        }
        public IMember GetMember(string name)
        {
            foreach (var member in MemberList)
            {
                if (member.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return member;
                }
            }
            throw new InvalidUserInputException($"There is no member with name {name}!");
        }

        public ITeam GetTeam(string name)
        {
            foreach (ITeam team in this.TeamsList)
            {
                if (team.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return team;
                }
            }
            //TODO
            throw new Exception($"There is no team with this name: {name}!");
        }
        public IBoard GetBoard(string name)
        {
            foreach (IBoard board in this.BoardList)
            {
                if (board.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return board;
                }
            }
            throw new Exception($"There is no board with this name: {name}!");
        }
        public IBug GetBug(string name)
        {
            foreach (IBug bug in this.BugList)
            {
                if (bug.Title.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return bug;
                }
            }
            throw new Exception($"There is no bug with this name: {name}!");
        }
        public IStory GetStory(string name)
        {
            foreach (IStory story in this.StoryList)
            {
                if (story.Title.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return story;
                }
            }
            throw new Exception($"There is no story with this name: {name}!");
        }
        public IFeedback GetFeedback(string name)
        {
            foreach (IFeedback feedback in this.FeedbackList)
            {
                if (feedback.Title.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return feedback;
                }
            }
            throw new Exception($"There is no story with this name: {name}!");
        }
        public bool MemberExists(string name)
        {
            bool result = false;
            foreach (IMember member in this.MemberList)
            {
                if (member.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public bool TeamExists(string name)
        {
            bool result = false;
            foreach (ITeam team in this.TeamsList)
            {
                if (team.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public bool BoardExists(string name)
        {
            bool result = false;
            foreach (IBoard board in this.BoardList)
            {
                if (board.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool TaskExists(string name)
        {
            bool result = false;
            foreach (ITask task in GetAllTasksList())
            {
                if (task.Title.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }

            }
            return result;
        }
        public bool IsATeamMember(IMember memberToLookFor, ITeam team)
        {
            bool result = false;
            foreach (var member in team.Members)
            {
                if (ReferenceEquals(member, memberToLookFor))
                {
                    return result = true;
                }
            }
            return result;
        }
        public bool IsATeamBoard(IBoard boardToLookFor, ITeam team)
        {
            bool result = false;
            foreach (var board in team.Boards)
            {
                if (ReferenceEquals(board, boardToLookFor))
                {
                    return result = true;
                }
            }
            return result;
        }




        #endregion
    }
}
