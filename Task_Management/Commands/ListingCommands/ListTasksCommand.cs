using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.ListingCommands
{
    public class ListTasksCommand : BaseCommand
    {
        public ListTasksCommand(IList<string> commandParameters, IRepository repository)
          : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            Validations.ValidateListNonEmpty(this.Repository.GetAllTasksList(), "tasks");

            //gives the user a list of all tasks
            if (this.CommandParameters == null || !this.CommandParameters.Any())
            {
                var tasks = this.Repository.GetAllTasksList();
                var sb = new StringBuilder();
                sb.AppendLine("List of all tasks:");
                sb.AppendLine(ListTasks(tasks));
                return sb.ToString();
            }

            //sorting the tasks list

            if (this.CommandParameters.Count == 1)
            {
                //Parameters:
                // [0] = Sort by

                string commandType = base.CommandParameters[0].ToLower();

                if (commandType == "sort by")
                {
                    var tasks = this.Repository.GetAllTasksList().OrderBy(m => m.Title);

                    var sb = new StringBuilder();
                    sb.AppendLine("List of all tasks sorted by title:");
                    sb.AppendLine(ListTasks(tasks));

                    return sb.ToString();

                }
            }
            if (this.CommandParameters.Count == 2)
            {
                //Parameters:
                // [0] = Filter by
                // [1] = string to look for

                string commandType = base.CommandParameters[0].ToLower();
                string stringToLookFor = base.CommandParameters[1];

                if (commandType == "filter by")
                {
                    var tasks = this.Repository.GetAllTasksList().Where(s => s.Title.Contains(stringToLookFor));

                    if (!tasks.Any())
                    {
                        throw new InvalidUserInputException("There are no tasks that contain this phrase in their title");
                    }

                    var sb = new StringBuilder();
                    sb.AppendLine($"List of all tasks that contain \"{stringToLookFor}\" in their title:");
                    sb.AppendLine(ListTasks(tasks));

                    return sb.ToString();
                }
            }

            throw new InvalidUserInputException($"Invalid user input for this command.\r\n" +
                   $"To get a list of tasks you must provide the \"list tasks\" command or: \r\n" +
                   $"      list tasks / sort by  *to sort tasks by their title \r\n"+
                   $"      list tasks / filter by / word you are looking for in the tasks's title ");

        }
        public string ListTasks(IEnumerable<ITask> tasks)
        {
            var sb = new StringBuilder();
            var counter = 1;
            foreach (ITask task in tasks)
            {
                sb.AppendLine($"{counter}. {task.DispleyMostImportantInfo()}");
                counter++;
            }
            sb.AppendLine("===========================================");
            return sb.ToString();
        }
    }
}


