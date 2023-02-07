using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums.Bug;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.StatusOfBug;

namespace Task_Management.Models.Contracts
{
    public interface IBug : IAssignableTask, IPriority
    {
        IList<string> ListOfStepsToReproduceBug { get; }
        Severity Severity { get; }
        StatusBug Status { get; }

        void ChangeBugStatus(StatusBug status);
        void ChangeBugPriority(Priority priority);
        void ChangeBugSeverity(Severity severity);
      


    }
}
