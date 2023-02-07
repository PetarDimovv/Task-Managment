using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;
using Task_Management.Models.Enums;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.Size;

namespace Task_Management.Commands.ModifyingCommands
{
    public class ChangeStorySizeCommand : BaseCommand
    {
        public ChangeStorySizeCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }
        public override string Execute()
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments.Expected: 2, " +
                    $"Received: {CommandParameters.Count}" +
                    $"Provide the \"Change story Size\" command / the ID of the story / the new size of the story");
            }


            int id = int.Parse(CommandParameters[0]);
            SizeStory newSize = Enum.Parse<SizeStory>(CommandParameters[1], ignoreCase: true);

            IStory story = Repository.StoryList.Where(b => b.Id == id).FirstOrDefault() ?? throw new InvalidUserInputException("There is not registered story with this ID");
            var size = story.Size;
            story.ChangeStorySize(newSize);
            story.AddToHistory($"The size of item with ID {id} switched from {size} to {newSize}");

            this.Repository.GetMember(story.Assignee.Name).AddToHistory
                ($"The size of item with ID {id} switched from {size} to {newSize}");

            return $"The size of item with ID {id} switched from {size} to {newSize}";
        }
    }
}
