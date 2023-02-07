using System;
using Task_Management.Core.Contracts;
using Task_Management.Core;

namespace Task_Management
{
    public class Program
    {
        static void Main(string[] args)
        {
            IRepository repository = new Repository();
            ICommandFactory commandFactory = new CommandFactory(repository);
            IEngine engine = new Core.Engine(commandFactory);
            engine.Start();
        }
    }
}
