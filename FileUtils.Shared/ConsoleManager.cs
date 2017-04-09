using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    public class ConsoleManager
    {
        public Dictionary<string, ConsoleCommand> commands;

        public void Init()
        {
            commands = new Dictionary<string, ConsoleCommand>();
        }

        public void SearchFromLoadedAssemblies()
        {
            Type consoleType = typeof(ConsoleCommand);

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                Assembly assembly = assemblies[i];
                Type[] types = assembly.GetTypes();
                for (int j = 0; j < types.Length; j++)
                {
                    Type t = types[j];

                    if (t.IsSubclassOf(consoleType))
                    {
                        ConsoleCommand cmd = (ConsoleCommand)Activator.CreateInstance(t, this);
                        commands.Add(cmd.Command, cmd);
                    }
                }
            }
        }

        public bool InputYesNo()
        {
            ConsoleU.WriteLine("Yes/No", Palette.Question);
            string yesno = ConsoleU.ReadLine().ToLower();

            return yesno.StartsWith("y");
        }

        public void Run()
        {
            ConsoleU.WriteLine("DistroLucas's FileUtils v" + FileUtilsGlobals.DisplayVersion.ToString("F2", CultureInfo.InvariantCulture), ConsoleColor.White);

            for (;;)
            {
                string line = ConsoleU.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line == "exit")
                {
                    break;
                }
                string[] sep = line.Split(' ');
                if (sep.Length == 0)
                {
                    continue;
                }

                string first = sep[0];
                ConsoleCommand cmd;
                if (!commands.TryGetValue(first, out cmd))
                {
                    ConsoleU.WriteLine("Unknown command", Palette.Error);
                    continue;
                }

                CommandFeedback feedback = cmd.Execute(sep);
                if (feedback != CommandFeedback.Success)
                {
                    ConsoleU.WriteLine(feedback.ToString(), Palette.Error);
                }
            }
        }

        public ConsoleCommand GetCommand(string cmd)
        {
            ConsoleCommand command;
            commands.TryGetValue(cmd, out command);
            return command;
        }
    }
}
