using System;
using System.Collections.Generic;
using System.Linq;
using Task_Management.Commands;
using Task_Management.Commands._Contracts;
using Task_Management.Commands.AddCommands;
using Task_Management.Commands.CreateCommands;
using Task_Management.Commands.ListingCommands;
using Task_Management.Commands.ModifyingCommands;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;

namespace Task_Management.Core
{
    public class CommandFactory : ICommandFactory
    {
        private readonly char SplitSymbol = '/';

        private readonly IRepository repository;

        public CommandFactory(IRepository repository)
        {
            this.repository = repository;
        }

        public ICommand CreateCommand(string commandLine)
        {
            string[] arguments = commandLine.Split(SplitSymbol, StringSplitOptions.RemoveEmptyEntries);

            string commandName = arguments[0].Trim();
            List<string> commandParameters = arguments.Skip(1).Select(a => a.Trim()).ToList();

            ICommand command;
            switch (commandName.ToLower())
            {
                case "create member":
                    command = new CreateMemberCommand(commandParameters, this.repository);
                    break;
                case "show all members":
                    command = new ShowAllMembersCommand(this.repository);
                    break;
                case "show member activity":
                    command = new ShowMemberActivityCommand(commandParameters, this.repository);
                    break;
                case "create team":
                    command = new CreateTeamCommand(commandParameters, this.repository);
                    break;
                case "show all teams":
                    command = new ShowAllTeamsCommand(this.repository);
                    break;
                case "show team activity":
                    command = new ShowTeam_sActivityCommand(commandParameters, this.repository);
                    break;
                case "add member to team":
                    command = new AddMemberToTeamCommand(commandParameters, this.repository);
                    break;
                case "show all team members":
                    command = new ShowAllTeamMembersCommand(commandParameters, this.repository);
                    break;
                case "create board":
                    command = new CreateBoardCommand(commandParameters, this.repository);
                    break;
                case "show all team boards":
                    command = new ShowAllTeamBoardsCommand(commandParameters, this.repository);
                    break;
                case "show board activity":
                    command = new ShowBoardActivityCommand(commandParameters, this.repository);
                    break;
                case "create bug":
                    command = new CreateBugCommand(commandParameters, this.repository);
                    break;
                case "create story":
                    command = new CreateStoryCommand(commandParameters, this.repository);
                    break;
                case "create feedback":
                    command = new CreateFeedbackCommand(commandParameters, this.repository);
                    break;
                case "change bug priority":
                    command = new ChangeBugPriorityCommand(commandParameters, this.repository);
                    break;
                case "change bug severity":
                    command = new ChangeBugSeverityCommand(commandParameters, this.repository);
                    break;
                case "change bug status":
                    command = new ChangeBugStatusCommand(commandParameters, this.repository);
                    break;
                case "change story priority":
                    command = new ChangeStoryPriorityCommand(commandParameters, this.repository);
                    break;
                case "change story size":
                    command = new ChangeStorySizeCommand(commandParameters, this.repository);
                    break;
                case "change story status":
                    command = new ChangeStoryStatusCommand(commandParameters, this.repository);
                    break;
                case "change feedback rating":
                    command = new ChangeFeedbackRatingCommand(commandParameters, this.repository);
                    break;
                case "change feedback status":
                    command = new ChangeFeedbackStatusCommand(commandParameters, this.repository);
                    break;
                case "assign task":
                    command = new AddAssigneeToTaskCommand(commandParameters, this.repository);
                    break;
                case "unassign task":
                    command = new RemoveAssigneeFromTaskCommand(commandParameters, this.repository);
                    break;
                case "add comment to task":
                    command = new AddCommentToTaskCommand(commandParameters, this.repository);
                    break;
                case "show task activity":
                    command = new ShowTaskActivityCommand(commandParameters, this.repository);
                    break;
                case "list tasks":
                    command = new ListTasksCommand(commandParameters, this.repository);
                    break;
                case "list tasks with assignee":
                    command = new ListTasksWithAssigneeCommand(commandParameters, this.repository);
                    break;
                case "list bugs":
                    command = new ListBugsCommand(commandParameters, this.repository);
                    break;
                case "list stories":
                    command = new ListStoriesCommand(commandParameters, this.repository);
                    break;
                case "list feedbacks":
                    command = new ListFeedbacksCommand(commandParameters, this.repository);
                    break;
                case "show all commands":
                    command = new ShowAllCommands(this.repository);
                    break;
                default:
                    throw new InvalidUserInputException("You have entered an invalid command.\r\n" +
                        "If you need help with commands type the command \"show all commands\".");
            }
            return command;
        }

    }
}
