using Nucleus;
using Nucleus.ConsoleEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileUtils.Commands {
    /// <summary>
    /// Writes text
    /// </summary>
    public class AddToTxtBottom : ConsoleCommand
    {
        public override string Command { get { return command; } }

        public override string Help
        {
            get { return "Writes text to a a text file header"; }
        }

        private string command = "addtotxtbottom";

        public AddToTxtBottom(ConsoleManager manager)
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
            string line = args[2];
            string searchParams = args[3];
            while (line.Contains("\\n"))
            {
                line = line.Replace("\\n", "\n");
            }

            if (!Directory.Exists(target))
            {
                return CommandFeedback.Error;
            }

            ConsoleU.WriteLine("Searching directory...", Palette.Wait);

            // target is a directory
            DirectoryInfo dir = new DirectoryInfo(target);
            FileInfo[] files = dir.GetFiles(searchParams);
            ConsoleU.WriteLine($"Found { files.Length } files. Would you like to write to the top of them?", Palette.Question);

            int changed = 0;
            int failed = 0;
            if (consoleManager.InputYesNo())
            {
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo file = files[i];
                    try
                    {
                        string readAll = File.ReadAllText(file.FullName);
                        readAll += line;
                        File.Delete(file.FullName);
                        File.WriteAllText(file.FullName, readAll);
                        changed++;
                    }
                    catch
                    {
                        ConsoleU.WriteLine($"Failed writing to { file.Name } ", Palette.Error);
                        failed++;
                    }
                }
            }

            ConsoleU.WriteLine($"Changed { changed } files", Palette.Success);
            ConsoleU.WriteLine($"Failed to change { failed } files", failed == 0 ? Palette.Success : Palette.Error);
            ConsoleS.ReadLine();

            return CommandFeedback.Success;
        }

        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private static void RecursiveSearch(DirectoryInfo parent, List<string> folders)
        {
            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo dir = dirs[i];

                if (IsDirectoryEmpty(dir.FullName))
                {
                    folders.Add(dir.FullName);
                }
                else
                {
                    RecursiveSearch(dir, folders);
                }
            }
        }
    }
}
