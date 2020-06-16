using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Commands {
    public class DeleteEmptyFolders : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Deletes folders that have nothing inside"; }
        }

        private string command = "delempty";
        private string[] parameters = new string[]
            {

            };

        public override string[] Parameters { get { return parameters; } }

        public DeleteEmptyFolders(ConsoleManager manager)
            : base(manager) {

        }

        public override CommandFeedback Execute(string[] args) {
            if (args.Length == 1) {
                return CommandFeedback.WrongNumberOfArguments;
            }

            string target = args[1];
            if (Directory.Exists(target)) {
                ConsoleU.WriteLine("Searching directory...", Palette.Wait);

                // target is a directory
                DirectoryInfo dir = new DirectoryInfo(target);
                List<string> dirs = new List<string>();
                RecursiveSearch(dir, dirs);

                if (dirs.Count == 0) {
                    ConsoleU.WriteLine($"Nothing to delete", Palette.Feedback);
                    return CommandFeedback.Success;
                }
                ConsoleU.WriteLine($"Found { dirs.Count } empty directories. Would you like to delete them?", Palette.Question);

                int deleted = 0;
                int failed = 0;
                if (consoleManager.InputYesNo()) {
                    for (int i = 0; i < dirs.Count; i++) {
                        string directory = dirs[i];
                        try {
                            Directory.Delete(directory);
                            deleted++;
                        } catch {
                            ConsoleU.WriteLine($"Failed deleting { directory } ", Palette.Error);
                            failed++;
                        }
                    }
                }

                ConsoleU.WriteLine($"Deleted { deleted } files", Palette.Success);
                ConsoleU.WriteLine($"Failed to delete { failed } files", failed == 0 ? Palette.Success : Palette.Error);
            } else {
                // target is a file/doesnt exist
            }


            return CommandFeedback.Success;
        }

        public static bool IsDirectoryEmpty(string path) {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        private static void RecursiveSearch(DirectoryInfo parent, List<string> folders) {
            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++) {
                DirectoryInfo dir = dirs[i];

                if (IsDirectoryEmpty(dir.FullName)) {
                    folders.Add(dir.FullName);
                } else {
                    RecursiveSearch(dir, folders);
                }
            }
        }
    }
}
