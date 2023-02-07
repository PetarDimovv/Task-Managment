using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.CustomExceptions;
using Task_Management.Models.Contracts;

namespace Task_Management.Models
{
    public class ActivityHistory : IActivityHistory
    {
        private const int ContentMinLength = 2;
        private const int ContentMaxLength = 300;
        private const string ContentErrorMessage = "Activity log should be between {0} and {1} characters long.";
        private string content;

        public ActivityHistory(string content)
        {
            this.Content = content;
        }
        public string Content 
        {
            get { return this.content; }
            set
            {
                if (value.Length < ContentMinLength || value.Length > ContentMaxLength)
                {
                    throw new InvalidUserInputException(string.Format(ContentErrorMessage,ContentMinLength,ContentMaxLength));
                }

                this.content = value;
            }
        }

        
    }
}
