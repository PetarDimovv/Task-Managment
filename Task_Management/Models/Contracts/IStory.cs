using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums;
using Task_Management.Models.Enums.Priority;
using Task_Management.Models.Size;

namespace Task_Management.Models.Contracts
{
    public interface IStory : IAssignableTask, IPriority
    {
        SizeStory Size { get; }
        StatusStory Status { get; }
        void ChangeStoryStatus(StatusStory status);
        void ChangeStoryPriority(Priority priority);
        void ChangeStorySize(SizeStory size);

    }
}
