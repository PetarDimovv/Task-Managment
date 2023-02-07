using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task_Management.Models.Contracts
{
    public interface ITeam : IActivity
    {
        string Name { get; }

        IList<IBoard> Boards { get; }
        IList<IMember> Members { get; }
        void AddMember(IMember member);
        void RemoveMember(IMember member);
        void AddBoard(IBoard board);


    }
}
