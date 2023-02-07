using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Size;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeStoryStatusCommand : BaseCommand
    {
        public ChangeStoryStatusCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }
        public override string Execute()
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change story status\" command / the ID of the story / the new status of the story");
            }


            int id = int.Parse(CommandParameters[0]);
            StatusStory newStatus = Enum.Parse<StatusStory>(CommandParameters[1], ignoreCase: true);

            IStory story = Repository.StoryList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered story with this ID");
            var status = story.Status;
            story.ChangeStoryStatus(newStatus);
            story.AddToHistory($"The status of item with ID {id} switched from {status} to {newStatus}");

            this.Repository.GetMember(story.Assignee.Name).AddToHistory
                ($"The status of item with ID {id} switched from {status} to {newStatus}");

            return $"The status of item with ID {id} switched from {status} to {newStatus}";
        }
    }
}
