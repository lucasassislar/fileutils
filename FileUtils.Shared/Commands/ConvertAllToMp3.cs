using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Commands {
    public class ConvertAllToMp3 : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Converts all non-mp3 files inside all the folders to mp3"; }
        }

        private string command = "convertalltomp3";
        private string[] parameters = new string[]
            {

            };

        public override string[] Parameters { get { return parameters; } }

        public ConvertAllToMp3(ConsoleManager manager)
            : base(manager) {

        }

        public override CommandFeedback Execute(string[] args) {
            if (args.Length == 1) {
                return CommandFeedback.WrongNumberOfArguments;
            }

            string targetFolder = args[1];
            if (!Directory.Exists(targetFolder)) {
                return CommandFeedback.Error;
            }

            var outputFolder = "";
            if (args.Length > 2) {
                outputFolder = args[2];
            }

            var sampleRate = "48000";
            if (args.Length > 3) {
                sampleRate = args[3];
            }

            var dir = new DirectoryInfo(targetFolder);
            var files = new List<FileInfo>();
            RecursiveGetFiles(dir, files);

            for (var i = 0; i < files.Count; i++) {
                var file = files[i];

                //ffmpeg -i input.wav -vn -ar 44100 -ac 2 -ab 192k -f mp3 output.mp3
                string target;

                if (string.IsNullOrEmpty(outputFolder)) {
                    target = Path.Combine(Path.GetDirectoryName(file.FullName), file.Name + ".mp3");
                } else {
                    target = Path.Combine(outputFolder, file.Name + ".mp3");
                }
                string startArgs = $"ffmpeg -i \"{file.FullName}\" -vn -ar {sampleRate} -ac 2 -ab `192k -f mp3 \"{target}\"";

                int exitCode;
                CmdUtil.ExecuteCommand("", out exitCode, startArgs);
            }

            return CommandFeedback.Success;
        }

        private string[] convertibleExtensions = new string[] {
            ".wav", ".ogg", ".m4a"
        };

        private void RecursiveGetFiles(DirectoryInfo parent, List<FileInfo> files) {
            FileInfo[] subFiles = parent.GetFiles();
            for (var i = 0; i < subFiles.Length; i++) {
                FileInfo file = subFiles[i];
                var extension = Path.GetExtension(file.FullName).ToLower();
                if (convertibleExtensions.Contains(extension)) {
                    files.Add(file);
                }
            }

            DirectoryInfo[] dirs = parent.GetDirectories();
            for (int i = 0; i < dirs.Length; i++) {
                RecursiveGetFiles(dirs[i], files);
            }
        }

        public void proc_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            if (string.IsNullOrEmpty(e.Data)) {
                return;
            }
            Console.WriteLine($"Redirected output: {e.Data}");
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
