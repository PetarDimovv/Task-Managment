using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Models.Enums.Priority;

namespace Task_Management.Models.Contracts
{
    public interface IPriority
    {
        Priority Priority { get;}
    }
}
