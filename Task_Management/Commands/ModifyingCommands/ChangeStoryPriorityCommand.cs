using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums.Priority;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeStoryPriorityCommand : BaseCommand
    {
        public ChangeStoryPriorityCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change story priority\" command / the ID of the story / the new priority of the story");
            }


            int id = int.Parse(CommandParameters[0]);
            Priority newPriority = Enum.Parse<Priority>(CommandParameters[1], ignoreCase: true);

            IStory story = Repository.StoryList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered story with this ID");
            var priority = story.Priority;
            story.ChangeStoryPriority(newPriority);
            story.AddToHistory($"The priority of item with ID {id} switched from {priority} to {newPriority}");

            this.Repository.GetMember(story.Assignee.Name).AddToHistory
                ($"The priority of item with ID {id} switched from {priority} to {newPriority}");

            return $"The priority of item with ID {id} switched from {priority} to {newPriority}";
        }
    }
}
