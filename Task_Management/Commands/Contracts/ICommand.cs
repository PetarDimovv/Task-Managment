﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.Commands._Contracts
{
    public interface ICommand
    {
        string Execute();
    }
}
