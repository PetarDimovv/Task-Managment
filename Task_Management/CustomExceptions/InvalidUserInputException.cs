using System;
using System.Collections.Generic;
using System.Text;

namespace Task_Management.CustomExceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException(string message) 
            : base(message)
        {

        }
    }
}
