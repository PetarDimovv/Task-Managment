using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfBug;
using Task_Management.Models.Size;

namespace Task_Management.Commands.ListingCommands
{
    public class ListStoriesCommand : BaseCommand
    {
        public ListStoriesCommand(IList<string> commandParameters, IRepository repository)
          : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validations.ValidateListNonEmpty(this.Repository.StoryList, "stories");
            //gives the user a list of all stories

            if (this.CommandParameters == null || !this.CommandParameters.Any())
            {
                var stories = this.Repository.StoryList;
                var sb = new StringBuilder();
                sb.AppendLine("List of all stories:");
                sb.AppendLine(ListStories(stories));

                return sb.ToString();
            }

            //sorting the stories list

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
                        var stories = this.Repository.StoryList.OrderBy(m => m.Title);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all stories sorted by title:");
                        sb.AppendLine(ListStories(stories));

                        return sb.ToString();

                    }
                    else if (byWhatState == "priority")
                    {
                        var stories = this.Repository.StoryList.OrderBy(m => m.Priority);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all stories sorted by priority:");
                        sb.AppendLine(ListStories(stories));

                        return sb.ToString();

                    }
                    else if (byWhatState == "size")
                    {
                        var stories = this.Repository.StoryList.OrderBy(m => m.Size);

                        var sb = new StringBuilder();
                        sb.AppendLine("List of all stories sorted by size:");
                        sb.AppendLine(ListStories(stories));

                        return sb.ToString();
                    }
                }
            }

            //Filter list by one of story's states and it's name

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
                        var stories = this.Repository.StoryList.Where(s => s.Status == Enum.Parse<StatusStory>(CommandParameters[2], ignoreCase: true));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all stories filtered by status {stateName}");
                        sb.AppendLine(ListStories(stories));

                        return sb.ToString();
                    }
                    else if (byWhatState == "assignee")
                    {
                        var stories = this.Repository.StoryList.Where
                            (s => s.Assignee == this.Repository.GetMember(stateName));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all stories filtered by assignee {stateName}");
                        sb.AppendLine(ListStories(stories));
                    }
                    else if (byWhatState == "status and assignee")
                    {
                        string[] stateNames = stateName.Split(',',StringSplitOptions.RemoveEmptyEntries).ToArray();

                        var stories = this.Repository.StoryList.Where
                            (s => s.Assignee == this.Repository.GetMember(stateNames[1]) 
                            && s.Status == Enum.Parse<StatusStory>(stateNames[0], ignoreCase: true));

                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all bugs filtered by status and assignee:");
                        sb.AppendLine(ListStories(stories));
                    }

                }
            }

            throw new InvalidUserInputException($"Invalid user input for this command.\r\n" +
                   $"To get a list of stories you must provide the \"list stories\" command or: \r\n" +
                   $"      list stories / sort by / title,priority or size \r\n" +
                   $"      list stories / filter by /status or assignee / status or  assignee's name\r\n" +
                   $"      list stories / filter by / status and assignee / status's name, assignee's name\r\n");
        }

        public string ListStories(IEnumerable<IStory> stories)
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (Story story in stories)
            {
                sb.AppendLine($"{counter}. {story.ShowInfo()}");
                counter++;
            }
            sb.AppendLine("===========================================");
            return sb.ToString();
        }

    }
}
