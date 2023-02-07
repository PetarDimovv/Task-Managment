using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;
using Task_Management.Models.StatusOfBug;

namespace Task_Management.Commands.ListingCommands
{
    public class ListTasksWithAssigneeCommand : BaseCommand
    {
        public ListTasksWithAssigneeCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {

            Validations.ValidateListNonEmpty(this.Repository.GetAllTasksWithAssigneeList(), "tasks");

            //gives the user a list of all tasks with assignee

            if (this.CommandParameters == null || !this.CommandParameters.Any())
            {
                var list = this.Repository.GetAllTasksWithAssigneeList();
                var sb = new StringBuilder();
                sb.AppendLine("List of all tasks with assignee:");
                sb.AppendLine(ListTasks(list));

                return sb.ToString();
            }

            //sorting the tasks list

            if (this.CommandParameters.Count == 1)
            {
                //Parameters:
                //    [0] = Sort by 

                string commandType = base.CommandParameters[0].ToLower();

                if (commandType == "sort by")
                {
                    var list = this.Repository.GetAllTasksWithAssigneeList().OrderBy(m => m.Title);
                    var sb = new StringBuilder();
                    sb.AppendLine("List of all tasks sorted by title:");
                    sb.AppendLine(ListTasks(list));

                    return sb.ToString();
                }
            }

            if (this.CommandParameters.Count == 3)
            {
                //Parameters:
                //    [0] = Filter by
                //    [1] = status/assignee/status and assignee
                //    [2] = string to look for

                string commandType = base.CommandParameters[0].ToLower();
                string byWhatState = base.CommandParameters[1].ToLower();
                string stringToLookFor = base.CommandParameters[2];

                if (commandType == "filter by")
                {
                    if (byWhatState == "status")
                    {
                        var list = this.Repository.GetAllTasksWithAssigneeList();
                        
                        var counter = 1;
                        var sb = new StringBuilder();

                        foreach (var task in list)
                        {
                            if (GetStatus(task).ToLower() == stringToLookFor.ToLower())
                            {
                                sb.AppendLine($"{counter}. \"{task.Title}\" - Assignee: {task.Assignee}");
                                counter++;
                            }

                        }
                        if (string.IsNullOrEmpty(sb.ToString()))
                        {
                            throw new EntityNotFoundException($"There are no tasks with status: {stringToLookFor}");
                        }
                        else
                        {
                            Console.WriteLine($"List of all tasks filtered by status: {stringToLookFor}");
                            return sb.ToString();
                        }
                    }
                    else if (byWhatState == "assignee")
                    {
                        var list = this.Repository.GetAllTasksWithAssigneeList().Where
                            (s => s.Assignee == this.Repository.GetMember(stringToLookFor));
                        Validations.ValidateListNonEmpty(list, "tasks with this assignee");
                        var sb = new StringBuilder();
                        sb.AppendLine($"List of all tasks filtered by assignee: {stringToLookFor}");
                        sb.AppendLine(ListTasks(list));


                        return sb.ToString();
                    }
                    else if (byWhatState == "status and assignee")
                    {
                        string[] stringsToLookFor = stringToLookFor.Split(',', StringSplitOptions.RemoveEmptyEntries).ToArray();
                        var list = this.Repository.GetAllTasksWithAssigneeList().Where(s => s.Assignee == this.Repository.GetMember(stringsToLookFor[1]));
                        Validations.ValidateListNonEmpty(list, "tasks with this assignee");
                        var sb = new StringBuilder();
                        var counter = 1;

                        foreach (var task in list)
                        {
                            if (GetStatus(task).ToLower() == stringToLookFor.ToLower())
                            {
                                sb.AppendLine($"{counter}. \"{task.Title}\" - Assignee: {task.Assignee}");
                                counter++;
                            }

                        }
                        if (string.IsNullOrEmpty(sb.ToString()))
                        {
                            throw new EntityNotFoundException($"There are no tasks with status: {stringToLookFor}");
                        }
                        else
                        {
                            Console.WriteLine($"List of all tasks filtered by status: {stringsToLookFor[0]} and assignee: {stringToLookFor}");
                            return sb.ToString();
                        }

                    }
                }
            }
            throw new InvalidUserInputException($"Invalid user input for this command.\r\n" +
               $"To get a list of tasks you must provide the \"list tasks with assignee\" command or: \r\n" +
               $"      list tasks with assignee / sort by \r\n" +
               $"      list tasks with assignee /  filter by / status or assignee / status or assignee's name \r\n" +
               $"      list tasks with assignee /  filter by / status and assignee/ status's name,assignee's name");

        }

        public string ListTasks(IEnumerable<IAssignableTask> tasks)
        {
            var sb = new StringBuilder();
            var counter = 1;

            foreach (var task in tasks)
            {
                sb.AppendLine($"{counter}. \"{task.Title}\" - Assignee: {task.Assignee.Name}");
                counter++;
            }

            sb.AppendLine("===========================================");
            return sb.ToString();
        }

        public string GetStatus(IAssignableTask task)
        {
            if (task is Bug)
            {
                IBug bug = this.Repository.GetBug(task.Title);
                return bug.Status.ToString();
            }
            IStory story = this.Repository.GetStory(task.Title);
            return story.Status.ToString();


        }
    }

}
