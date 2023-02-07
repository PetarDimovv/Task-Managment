using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands._Contracts;

namespace Task_Management.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string commandLine);
    }
}
