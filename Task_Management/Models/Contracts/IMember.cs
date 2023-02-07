using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IMember : IActivity
    {
        string Name { get; }
        IList <IAssignableTask> MemberTasks { get; }
        void AddTask(IAssignableTask task);
        void RemoveTask(IAssignableTask task);


    }
}
