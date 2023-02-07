using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IBoard : IActivity
    {
        string Name { get; }
        IList<ITask> BoardTasks { get; }
        void AddTaskToBoard(ITask task);

    }
}
