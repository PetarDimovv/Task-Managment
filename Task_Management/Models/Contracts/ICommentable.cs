using System.Collections.Generic;

namespace Task_Management.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }
        void AddComment(Comment comment);
        string ShowAllComments();
    }
}
