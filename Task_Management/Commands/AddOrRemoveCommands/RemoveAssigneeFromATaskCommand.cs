using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.AddCommands
{
    public class RemoveAssigneeFromTaskCommand : BaseCommand
    {
        public RemoveAssigneeFromTaskCommand(IList<string> commandParameters, IRepository repository)
           : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count < 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2," +
                    $" Received: {CommandParameters.Count}");
            }

            //Parameters:
            // [0] - Member's name
            // [1] - Tasks's id

            string memberName = CommandParameters[0];
            int taskID = int.Parse(CommandParameters[1]);
            IMember member = base.Repository.GetMember(memberName);
            ITask task = base.Repository.FindTaskByID(taskID);

            if (task is IFeedback)
            {
                throw new InvalidUserInputException("Tasks of type \"feedback\" do not have assignee");
            }

            // validate that the task is part of a certain board that this member is part of

            IAssignableTask assignable = (IAssignableTask)task;

            assignable.Assignee = null;
            member.RemoveTask(assignable);
            member.AddToHistory($"Task \"{assignable.Title}\" has been unassigned from {memberName}");
            assignable.AddToHistory($"Task \"{assignable.Title}\" has been assigned from {memberName}");

            return $"Task \"{assignable.Title}\" has been unassigned from {memberName}";
        }
    }
}

