using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Models.Contracts
{
    public interface IActivity
    {
       IList<ActivityHistory> ActivityHistory { get; }
        void AddToHistory(string content);
        string PrintActivity();

    }
}
