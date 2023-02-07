using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task_Management.Models.Contracts
{
    public interface ITask : IHasId, ICommentable, IActivity
    {
        string Title { get; }
        string Description { get; }
        string ShowInfo();
        string DispleyMostImportantInfo();
    }
}
