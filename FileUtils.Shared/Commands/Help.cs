using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Commands
{
    public class HelpCmd : ConsoleCommand
    {

       

        private string command = "help";
        private string[] parameters = new string[]
            {
            };

        public override string Command { get { return command; } }
        public override string Help
        {
            get { return "Example usage\nhelp commandName"; }
        }
        public override string[] Parameters { get { return parameters; } }

        public HelpCmd(ConsoleManager manager)
            : base(manager)
        {

        }

        public override CommandFeedback Execute(string[] args)
        {
            if (args.Length == 1)
            {
                return CommandFeedback.WrongNumberOfArguments;
            }

            string target = args[1];
            ConsoleCommand cmd = consoleManager.GetCommand(target);
            if (cmd != null)
            {
                ConsoleU.WriteLine(cmd.Help, Palette.Help);
            }

            return CommandFeedback.Success;
        }
    }
}
