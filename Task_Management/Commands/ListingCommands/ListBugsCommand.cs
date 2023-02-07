using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using System.Linq;
using Task_Management.Models.StatusOfBug;
using System.Reflection;

namespace Task_Management.Commands.ListingCommands
{
    public class ListBugsCommand : BaseCommand
    {
        public ListBugsCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validations.ValidateListNonEmpty(this.Repository.BugList, "bugs");

            //gives the user a list of all bugs
            if (this.CommandParameters == null || !this.CommandParameters.Any())
            {
                var bugs = this.Repository.BugList;
                var sb = new StringBuilder();
                sb.AppendLine("List of all bugs:");
                sb.AppendLine(ListBugs(bugs));

                return sb.ToString();
            }

            //sorting the bug list

            if (this.CommandParameters.Count == 2)
            {
                //Parameters:
                // [0] = Sort by 
                // [1] = By what state - title,priority,severity

                string commandType = base.CommandParameters[0].ToLower();
                string byWhatState = base.CommandParameters[1].ToLower();

                if (commandType == "sort by")
                {
                    if (byWhatState == "title")
                    {
                        var bugs = this.Repository.BugList.OrderBy(m => m.Title);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all bugs sorted by title:");
                        sb.AppendLine(ListBugs(bugs));

                        return sb.ToString();

                    }
                    else if (byWhatState == "priority")
                    {
                        var bugs = this.Repository.BugList.OrderBy(m => m.Priority);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all bugs sorted by priority:");
                        sb.AppendLine(ListBugs(bugs));

                        return sb.ToString();

                    }
                    else if (byWhatState == "severity")
                    {
                        var bugs = this.Repository.BugList.OrderBy(m => m.Severity);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all bugs sorted by severity:");
                        sb.AppendLine(ListBugs(bugs));

                        return sb.ToString();
                    }
                }
            }

            //Filter list by one of bug's states and it's name

            if (this.CommandParameters.Count == 3)
            {
                //Parameters:
                // [0] = Filter by
                // [1] = By what state - status,assignee
                // [2] = State's "name"

                string commandType = base.CommandParameters[0].ToLower();
                string byWhatState = base.CommandParameters[1].ToLower();
                string stateName = base.CommandParameters[2];

                if (commandType == "filter by")
                {
                    if (byWhatState == "status")
                    {
                        var bugs = this.Repository.BugList.Where(s => s.Status == Enum.Parse<StatusBug>(CommandParameters[2], ignoreCase: true));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all bugs filtered by status {stateName}");
                        sb.AppendLine(ListBugs(bugs));
                    }
                    else if (byWhatState == "assignee")
                    {
                        var bugs = this.Repository.BugList.Where
                            (s => s.Assignee == this.Repository.GetMember(stateName));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all bugs filtered by assignee {stateName}");
                        sb.AppendLine(ListBugs(bugs));
                    }
                    else if (byWhatState == "status and assignee")
                    {
                        string[] stateNames = stateName.Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();

                        var bugs = this.Repository.BugList.Where(s => s.Assignee == this.Repository.GetMember(stateNames[0]) && s.Status == Enum.Parse<StatusBug>(CommandParameters[1], ignoreCase: true));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all bugs filtered by status and assignee:");
                        sb.AppendLine(ListBugs(bugs));
                    }

                }
            }

            throw new InvalidUserInputException($"Invalid user input for this command.\r\n" +
                   $"To get a list bugs you must provide the \"list bugs\" command or: \r\n" +
                   $"      list bugs / sort by / title,priority or severity \r\n" +
                   $"      list bugs / filter by / status or assignee / status or assignee's name \r\n" +
                   $"      list bugs / filter by / status and assignee / status's name,assignee's name \r\n");
        }

        public string ListBugs(IEnumerable<IBug> bugs)
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (Bug bug in bugs)
{
                sb.AppendLine($"{counter}. {bug.ShowInfo()}");
                counter++;
            }
            sb.AppendLine();
            return sb.ToString();
        }
    }

}
