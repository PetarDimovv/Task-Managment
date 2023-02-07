using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.AddCommands
{
    public class AddCommentToTaskCommand : BaseCommand
    {
        public AddCommentToTaskCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count < 3)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 3," +
                    $" Received: {CommandParameters.Count}");
            }

            //Parameters:
            // [0] - Content
            // [1] - Author
            // [2] - Tasks's Id

            string content = CommandParameters[0];
            string author = CommandParameters[1];
            int id = int.Parse(CommandParameters[2]);

            var comment = new Comment(content, author);
            var task = base.Repository.FindTaskByID(id);

            task.AddToHistory("Comment added:");
            task.AddToHistory($"{comment.ToString()}");
            task.AddComment(comment);
            return $"Your comment \"{content}\" has been added to \"{task.Title}\"";
        }
    }
}
