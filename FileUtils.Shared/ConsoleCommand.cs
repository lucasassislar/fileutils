using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    public abstract class ConsoleCommand
    {
        protected ConsoleManager consoleManager;

        public ConsoleCommand(ConsoleManager manager)
        {
            consoleManager = manager;
        }

        public abstract string Command { get; }
        public abstract string Help { get; }
        public abstract string[] Parameters { get; }

        public abstract CommandFeedback Execute(string[] args);
    }
}
