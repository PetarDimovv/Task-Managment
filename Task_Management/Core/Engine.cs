using System;
using System.Collections.Generic;
using System.Text;
using Task_Management.Commands._Contracts;
using Task_Management.Core.Contracts;

namespace Task_Management.Core
{
    public class Engine : IEngine
    {
        private const string TerminationCommand = "exit";
        private const string EmptyCommandError = "Command cannot be empty.";

        private readonly ICommandFactory commandFactory;
        public Engine(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }
        public void Start()
        {
            while (true)
            {
                string inputLine = Console.ReadLine().Trim();
                try
                {
                    if (inputLine == string.Empty)
                    {
                        throw new ArgumentException(EmptyCommandError);
                    }
                    if (inputLine.ToLower() == TerminationCommand)
                    {
                        break;
                    }
                    ICommand command = this.commandFactory.CreateCommand(inputLine);
                    string result = command.Execute();
                    Console.WriteLine(result.Trim());
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(ex.Message))
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }
    }
}
