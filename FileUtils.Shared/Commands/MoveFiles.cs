using Nucleus.ConsoleEngine;
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

namespace FileUtils.Commands {
    public class MoveFiles : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Move files"; }
        }

        private string command = "movefiles";

        public MoveFiles(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strFolder = Environment.CurrentDirectory;
            if (args.Length > 1) {
                strFolder = args[1];
            }

            string strDestiny = args[2];

            List<string> filters = new List<string>();
            for (int i = 3; i < args.Length; i++) {
                filters.Add(args[i]);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(strFolder);
            RecursiveFolder(dirInfo, dirInfo, strDestiny, filters);

            return CommandFeedback.Success;
        }

        private void RecursiveFolder(DirectoryInfo baseDir, DirectoryInfo dirInfo,
            string strDestiny, List<string> filters) {

            for (int x = 0; x < filters.Count; x++) {
                FileInfo[] arrFiles = dirInfo.GetFiles(filters[x]);
                for (int i = 0; i < arrFiles.Length; i++) {
                    FileInfo file = arrFiles[i];

                    if (file.FullName.Contains(strDestiny)) {
                        continue;
                    }

                    string relPath = file.FullName.Replace(baseDir.FullName, "");
                    relPath = relPath.Remove(0, 1);

                    string fullPath = Path.Combine(strDestiny, relPath);
                    string strFullPath = Path.GetDirectoryName(fullPath);
                    Directory.CreateDirectory(strFullPath);

                    file.MoveTo(fullPath);
                    Console.WriteLine(fullPath);
                }
            }

            DirectoryInfo[] arrDirs = dirInfo.GetDirectories();
            for (int i = 0; i < arrDirs.Length; i++) {
                RecursiveFolder(baseDir, arrDirs[i], strDestiny, filters);
            }
        }
    }
}
