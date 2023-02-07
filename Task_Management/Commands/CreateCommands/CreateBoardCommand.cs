using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Core.Contracts;
using Task_Management.CustomExceptions;
using Task_Management.Models;
using Task_Management.Models.Contracts;

namespace Task_Management.Commands.CreateCommands
{
    public class CreateBoardCommand : BaseCommand
    {
        public CreateBoardCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {

        }

        public override string Execute() 
        {
            if (CommandParameters.Count != 2)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: 2, Received: {CommandParameters.Count}\r\n" +
                    $"To create a new board you need to type in: \r\n" +
                    $"create board / board name / team name");
                //TODO
                //Should create a unit test for this one
            }

            string boardName = CommandParameters[0];
            string teamName = CommandParameters[1];


            if (Repository.BoardExists(boardName))
            {
                throw new InvalidUserInputException("A board with this name already exists");
            }
            var createBoard = Repository.CreateBoard(boardName);
            createBoard.AddToHistory($"A new board [Title: \"{createBoard.Name}\"] in team: \"{teamName}\" was created");

            //IBoard board = this.Repository.GetBoard(boardName);
            ITeam team = this.Repository.GetTeam(teamName);
            
            team.AddBoard(createBoard);
            team.AddToHistory($"Board \"{createBoard.Name}\" has been created to team: \"{teamName}\"");

            return $"A new board [Title: \"{createBoard.Name}\"] in team: \"{teamName}\" was created";
        }
    }
}
