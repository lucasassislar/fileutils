using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils {
    public class ConsoleManager {
        public Dictionary<string, ConsoleCommand> commands;
        private bool silentMode;

        public void SetSilentMode(bool value) {
            silentMode = value;
        }

        public void Init() {
            ConsoleU.WriteLine("DistroLucas's FileUtils v" + FileUtilsGlobals.DisplayVersion.ToString("F2", CultureInfo.InvariantCulture), ConsoleColor.White);
            commands = new Dictionary<string, ConsoleCommand>();
        }

        public void SearchFromLoadedAssemblies() {
            Type consoleType = typeof(ConsoleCommand);

            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly[] assemblies = new Assembly[] { Assembly.GetExecutingAssembly() };
            for (int i = 0; i < assemblies.Length; i++) {
                Assembly assembly = assemblies[i];
                Type[] types = assembly.GetTypes();
                for (int j = 0; j < types.Length; j++) {
                    Type t = types[j];

                    if (t.IsSubclassOf(consoleType)) {
                        ConsoleCommand cmd = (ConsoleCommand)Activator.CreateInstance(t, this);
                        commands.Add(cmd.Command, cmd);
                    }
                }
            }
        }

        public bool InputYesNo(bool silent = true) {
            ConsoleU.WriteLine("Yes/No", Palette.Question);
            if (silentMode) {
                // still shows up that we were asking the user a question
                ConsoleU.WriteLine(silent ? "Yes" : "No", Palette.Question);
                return silent;
            }

            string yesno = ConsoleU.ReadLine().ToLower();
            return yesno.StartsWith("y");
        }

        public void ExecuteCommand(string line) {
            if (string.IsNullOrEmpty(line)) {
                return;
            }

            string[] sep = line.Split(' ');
            ExecuteCommand(sep);
        }

        public void ExecuteCommand(string[] sep) {
            if (sep.Length == 0) {
                return;
            }

            string first = sep[0];
            ConsoleCommand cmd;
            if (!commands.TryGetValue(first, out cmd)) {
                ConsoleU.WriteLine("Unknown command", Palette.Error);
                return;
            }

            CommandFeedback feedback = cmd.Execute(sep);
            if (feedback != CommandFeedback.Success) {
                ConsoleU.WriteLine(feedback.ToString(), Palette.Error);
            }
        }

        public void Run() {
            for (; ; )
            {
                string line = ConsoleU.ReadLine();
                ExecuteCommand(line);
            }
        }

        public void ProcessCommand(string command) {

        }

        public ConsoleCommand GetCommand(string cmd) {
            ConsoleCommand command;
            commands.TryGetValue(cmd, out command);
            return command;
        }
    }
}
