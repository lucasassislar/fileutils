using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nucleus;

namespace FileUtils.Commands {
    public class RenameSimilar : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Rename Similar files to different"; }
        }

        private string command = "renamefiles";
        private string[] parameters = new string[]{
        };

        public override string[] Parameters { get { return parameters; } }
        private List<FileInfo> sourceFiles;

        public RenameSimilar(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strSourceFolder = Environment.CurrentDirectory;
            if (args.Length > 1) {
                strSourceFolder = args[1];
            }

            string strDestiny = args[2];
            string strPrefix = args[3];

            List<string> filters = new List<string>();
            for (int i = 4; i < args.Length; i++) {
                filters.Add(args[i]);
            }

            sourceFiles = new List<FileInfo>();
            // scan all files from directory info
            ScanFolder(new DirectoryInfo(strDestiny), filters);

            RecursiveFolder(new DirectoryInfo(strSourceFolder), strDestiny, strPrefix, filters);

            ConsoleU.WriteLine("Total similar: " + similarCounter, ConsoleColor.Green);

            return CommandFeedback.Success;
        }

        private void ScanFolder(DirectoryInfo dirInfo, List<string> filters) {
            for (int x = 0; x < filters.Count; x++) {
                FileInfo[] arrFiles = dirInfo.GetFiles(filters[x]);
                sourceFiles.AddRange(arrFiles);
            }

            DirectoryInfo[] arrDirs = dirInfo.GetDirectories();
            for (int i = 0; i < arrDirs.Length; i++) {
                ScanFolder(arrDirs[i], filters);
            }
        }

        private int similarCounter;

        private void RecursiveFolder(DirectoryInfo dirInfo,
            string strDestiny, string strPrefix, List<string> filters) {

            for (int x = 0; x < filters.Count; x++) {
                FileInfo[] arrFiles = dirInfo.GetFiles(filters[x]);
                for (int i = 0; i < arrFiles.Length; i++) {
                    FileInfo file = arrFiles[i];

                    FileInfo similar = sourceFiles.FirstOrDefault(c => c.Name == file.Name);
                    if (similar != null) {
                        similarCounter++;

                        string strFolder = Path.GetDirectoryName(file.FullName);
                        string strNewName = strPrefix + file.Name;
                        Console.WriteLine($"Rename: {file.Name} to {strNewName}");

                        string strFullPath = Path.Combine(strFolder, strNewName);
                        file.MoveTo(strFullPath);
                    }
                }
            }

            DirectoryInfo[] arrDirs = dirInfo.GetDirectories();
            for (int i = 0; i < arrDirs.Length; i++) {
                RecursiveFolder(arrDirs[i], strDestiny, strPrefix, filters);
            }
        }
    }
}
