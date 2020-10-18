using Nucleus;
using Nucleus.ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileUtils.Commands {
    public class ListFiles : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "List files with options"; }
        }

        private string command = "listfiles";

        public ListFiles(ConsoleManager manager)
            : base(manager) {

        }

        public override CommandFeedback Execute(string[] args) {
            if (args.Length < 3) {
                return CommandFeedback.WrongNumberOfArguments;
            }

            string target = args[1];
            string pattern = args[2];

            if (Directory.Exists(target)) {
                ConsoleU.WriteLine("Searching directory...", Palette.Wait);

                // target is a directory
                DirectoryInfo dir = new DirectoryInfo(target);
                FileInfo[] files = dir.GetFiles(pattern);

                ConsoleU.WriteLine($"Found { files.Length } files", Palette.Feedback);
                ConsoleU.WriteLine($"Command", Palette.Question);

                for (; ; )
                {
                    string command = ConsoleS.ReadLine();
                    bool exit = false;
                    switch (command) {
                        case "3dsmaximport": {
                            StringBuilder namesBuilder = new StringBuilder();
                            for (int i = 0; i < files.Length; i++) {
                                string file = files[i].FullName;
                                namesBuilder.AppendLine("ImportFile \"" + file + "\" #noPrompt");
                            }
                            string str = namesBuilder.ToString();
                            Clipboard.SetText(namesBuilder.ToString());
                            ConsoleU.WriteLine($"Setting to clipboard { str } ", Palette.Feedback);
                        }
                        break;
                        case "clipboard": {
                            StringBuilder namesBuilder = new StringBuilder();
                            for (int i = 0; i < files.Length; i++) {
                                string file = files[i].FullName;
                                namesBuilder.AppendLine(file);
                            }
                            string str = namesBuilder.ToString();
                            Clipboard.SetText(str);
                            ConsoleU.WriteLine($"Setting to clipboard { str } ", Palette.Feedback);
                        }
                        break;
                        case "clipboardquotes": {
                            StringBuilder namesBuilder = new StringBuilder();
                            for (int i = 0; i < files.Length; i++) {
                                string file = files[i].FullName;
                                namesBuilder.AppendLine('"' + file + '"');
                            }
                            string str = namesBuilder.ToString();
                            Clipboard.SetText(str);
                            ConsoleU.WriteLine($"Setting to clipboard { str } ", Palette.Feedback);
                        }
                        break;
                        case "copy":
                            StringCollection fileList = new StringCollection();
                            for (int i = 0; i < files.Length; i++) {
                                string file = files[i].FullName;
                                ConsoleU.WriteLine($"Setting to clipboard { file } ", Palette.Feedback);
                                fileList.Add(file);
                            }
                            Clipboard.SetFileDropList(fileList);
                            break;
                        case "exit":
                            exit = true;
                            break;
                    }

                    if (exit) {
                        break;
                    }
                }
            } else {
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
