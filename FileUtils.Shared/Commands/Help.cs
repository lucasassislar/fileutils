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
            get { return "Example usage:\n  help commandName"; }
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
                ConsoleU.WriteLine(Help, Palette.Feedback);
                return CommandFeedback.Success;
            }

            string target = args[1];
            ConsoleCommand cmd = consoleManager.GetCommand(target);
            if (cmd != null)
            {
                ConsoleU.WriteLine(cmd.Help, Palette.Help);
            }
            else
            {
                ConsoleU.WriteLine("Unknown command", Palette.Error);
            }

            return CommandFeedback.Success;
        }
    }
}
