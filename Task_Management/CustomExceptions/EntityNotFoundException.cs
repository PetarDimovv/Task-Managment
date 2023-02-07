using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.CustomExceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
