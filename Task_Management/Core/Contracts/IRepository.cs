using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfBug;
using Task_Management.Models.StatusOfFeedBack;
using Task_Management.Models.Size;

namespace Task_Management.Core.Contracts
{
    public interface IRepository
    {
        IList<IMember> MemberList { get; }
        IList<ITeam> TeamsList { get; }
        IList<IBug> BugList { get; }
        IList<IFeedback> FeedbackList { get; }
        IList<IStory> StoryList { get; }
        IMember CreateMember(string name);
        IBoard CreateBoard(string name);
        ITeam CreateTeam(string title);
        IBug CreateBug(string title, string description, string stepsToReproduce, Priority priority, Severity severity);
        IStory CreateStory(string title, string description, Priority priority, Models.Enums.SizeStory size, StatusStory status);
        IFeedback CreateFeedback(string title, string description, int rating, StatusFeedback status);
        IComment CreateComment(string content, string author);
        IList<ITask> GetAllTasksList();
        IList<IAssignableTask> GetAllTasksWithAssigneeList();
        IMember GetMember(string name);
        ITeam GetTeam(string name);
        IBoard GetBoard(string name);
        IStory GetStory(string name);
        IBug GetBug(string name);
        IFeedback GetFeedback(string name);
        ITask FindTaskByID(int id);
        bool MemberExists(string name);
        bool BoardExists(string name);
        bool TeamExists(string name);
        bool TaskExists(string name);
        bool IsATeamMember(IMember memberToLookFor, ITeam team);
        bool IsATeamBoard(IBoard boardToLookFor, ITeam team);



    }
}
