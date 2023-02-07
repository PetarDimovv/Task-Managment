using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IAssignableTask : ITask
    {
        IMember Assignee { get; set; }
    }
}
