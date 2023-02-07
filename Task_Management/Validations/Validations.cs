using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.Models.Contracts;

namespace Task_Management
{
    public static class Validations 
    {
        private const int TitleMinLength = 10;
        private const int TitleMaxLength = 50;
        private const string TitleLengthErrorMessage = "Title length must be between {0} and {1} characters long!";
        private const int DescriptionMinLength = 10;
        private const int DescriptionMaxLength = 500;
        private const string DescriptionLengthErrorMessage = "Description length must be between {0} and {1} characters long!";
        private const int NameMinLength = 5;
        private const int NameMaxLength = 15;
        private const string NameLengthErrorMessage = "Name length must be between {0} and {1} characters long!";
        private const int CommentMinLength = 10;
        private const int CommentMaxLength = 300;
        private const string InvalidCommentError = "Content must be between {0} and {1} characters long!";

        public static void ValidateTitleLength(string value)
        {
            if (value.Length < TitleMinLength || value.Length > TitleMaxLength)
            {
                string error = string.Format(TitleLengthErrorMessage, TitleMinLength, TitleMaxLength);

                throw new ArgumentException(error);
            }
        }
        public static void ValidateDescriptionLength(string value)
        { 
            if (value.Length < DescriptionMinLength || value.Length > DescriptionMaxLength)
            {
                string error = string.Format(DescriptionLengthErrorMessage, DescriptionMinLength, DescriptionMaxLength);

                throw new ArgumentException(error);
            }
        }
        public static void ValidateNameLength(string value)
        {
            if (value.Length < NameMinLength || value.Length > NameMaxLength)
            {
                string error = string.Format(NameLengthErrorMessage, NameMinLength, NameMaxLength);

                throw new ArgumentException(error);
            }
        }

        public static void ValidateIntRange(string value)
        {
            if (value.Length < CommentMinLength || value.Length > CommentMaxLength)
            {
                string error = string.Format(InvalidCommentError, CommentMinLength, CommentMaxLength);

                throw new ArgumentException(error);
            }
        }

        public static void ValidateListNonEmpty(IEnumerable<object> list, string typeOfTask)
        {
            if (!list.Any())
            {
                throw new InvalidOperationException($"There are no registered {typeOfTask}");
            }
        }

    }
}
