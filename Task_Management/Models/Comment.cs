using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Task_Management.Models
{
    public class Comment : IComment
    {
        #region Fields
        //private string content;
        //private string author;
        #endregion

        #region Ctor
        public Comment(string content, string author)
        {
            Validations.ValidateIntRange(content); //Validator created, checks lenght
            this.Content = content;
            this.Author = author;
        }
        #endregion
        #region Properties
        public string Content { get; }
        public string Author { get; }
        #endregion

        #region Methods
        public override string ToString() //Creates a SB - should be saved in the repo.
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("    ----------");
            sb.AppendLine($"\"{this.Content}\" -Author: {this.Author}");
            sb.Append("    ----------");

            return sb.ToString();
        }
        #endregion
    }
}
