using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nucleus;

namespace FileUtils.Commands
{
    public class CutAudioFiles : ConsoleCommand
    {
        public override string Command { get { return command; } }

        public override string Help
        {
            get { return "Writes text to a a text file header"; }
        }

        private string command = "cutaudiofile";
        private string[] parameters = new string[]
            {

            };

        public override string[] Parameters { get { return parameters; } }

        public CutAudioFiles(ConsoleManager manager)
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
            if (!File.Exists(target))
            {
                return CommandFeedback.Error;
            }

            string startTime = args[2];
            string cutDuration = args[3];


            // rename the source file to backup
            string dir = Path.GetDirectoryName(target);
            string fileName = Path.GetFileNameWithoutExtension(target);
            string sourceFile = Path.Combine(dir, fileName + "_Backup.m4a");
            File.Move(target, sourceFile);

            //ffmpeg - ss 1:01:42 - i c:\Data\temp\in.m4a - vn - c copy - t 1:00:00 out.m4a

            //The first time(1:01:42) is the start time of the cut.
            //-vn means that only the audio stream is copied from the file.
            //The second time(1:00:00) is the duration of the cut.It can be longer then the length of the whole file.
            string startArgs = $"ffmpeg -ss {startTime} -i \"{sourceFile}\" -vn -c copy -t {cutDuration} \"{target}\"";

            int exitCode;
            CmdUtil.ExecuteCommand("", out exitCode, startArgs);

            bool makeBackup = false;
            if (File.Exists(target))
            {
                if (!makeBackup)
                {
                    File.Delete(sourceFile);
                }
                ConsoleU.WriteLine($"Okay", Palette.Success);
            }
            else
            {
                ConsoleU.WriteLine($"Failed", Palette.Error);
            }

            return CommandFeedback.Success;
        }

        public void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
            {
                return;
            }
            Console.WriteLine($"Redirected output: {e.Data}");
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
