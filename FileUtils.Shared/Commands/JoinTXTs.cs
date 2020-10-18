using Nucleus.ConsoleEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileUtils.Commands {
    public class JoinTXTs : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "List files with options"; }
        }

        private string command = "jointxt";

        public JoinTXTs(ConsoleManager manager)
            : base(manager) {

        }

        int comparer(string a, string b) {
            int aIndex = a.IndexOf('#');
            int aFirstSpace = a.IndexOf(' ', aIndex);
            string aNumber = a.Substring(aIndex + 1, aFirstSpace - aIndex);

            int bIndex = b.IndexOf('#');
            int bFirstSpace = b.IndexOf(' ', bIndex);
            string bNumber = b.Substring(bIndex + 1, bFirstSpace - bIndex);

            int aNum = int.Parse(aNumber);
            int bNum = int.Parse(bNumber);

            return aNum.CompareTo(bNum);
        }

        public override CommandFeedback Execute(string[] args) {
            string currentDir = Environment.CurrentDirectory;

            if (args.Length > 1) {
                currentDir = args[1];
            }

            DirectoryInfo dirInfo = new DirectoryInfo(currentDir);
            FileInfo[] files = dirInfo.GetFiles("*.txt");

            string outputPath = Path.Combine(currentDir, "output.txt");

            List<string> txts = new List<string>();
            for (int i = 0; i < files.Length; i++) {
                string txt = File.ReadAllText(files[i].FullName);
                txts.Add(txt);
            }

            txts.Sort(comparer);

            using (Stream str = File.OpenWrite(outputPath)) {
                using (StreamWriter writer = new StreamWriter(str)) {
                    for (int i = 0; i < txts.Count; i++) {
                        writer.WriteLine(txts[i]);
                    }
                }
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
