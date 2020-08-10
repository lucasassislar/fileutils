using Nucleus;
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
    public class AddNamespaces : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Add namespaces to classes"; }
        }

        private string command = "addnamespace";
        private string[] parameters = new string[]{
        };

        public override string[] Parameters { get { return parameters; } }
        private List<FileInfo> sourceFiles;
        private List<FileInfo> destFiles;
        private List<FileInfo> updatedFiles;
        private Dictionary<FileInfo, string> renamedFiles;
        private int similarCounter;
        private int referencesFound;

        public AddNamespaces(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strSourceFolder = Environment.CurrentDirectory;
            if (args.Length > 1) {
                strSourceFolder = args[1];
            }

            string strFilter = args[2];
            sourceFiles = new List<FileInfo>();

            ScanFolder(new DirectoryInfo(strSourceFolder), new List<string>() { strFilter }, sourceFiles);

            for (int i = 0; i < sourceFiles.Count; i++) {
                FileInfo file = sourceFiles[i];
                string strFullName = file.FullName;
                string strFileText;
                using (Stream str = file.OpenRead()) {
                    StreamReader strReader = new StreamReader(str);
                    strFileText = strReader.ReadToEnd();
                }

                int numClassIndex = strFileText.IndexOf("class");
                int numNamespaceIndex = strFileText.IndexOf("namespace");

                int numInsertIndex = 0;
                int numDefineIndex = strFileText.LastIndexOf("#define");
                if (numDefineIndex != -1) {
                    numInsertIndex = strFileText.IndexOf("\n", numDefineIndex + 1);
                }

                int numUsingIndex = strFileText.LastIndexOf("using");
                if (numNamespaceIndex != -1) {
                    int numUsingIndexEnd = 0;
                    if (numUsingIndex != -1) {
                        numUsingIndexEnd = strFileText.IndexOf(";", numUsingIndex);
                    } 

                    ConsoleU.WriteLine($"File: {file.Name} already has namespace", ConsoleColor.Yellow);
                    strFileText = strFileText.Insert(numInsertIndex, Environment.NewLine + "using PixelRipped1989;" + Environment.NewLine);
                    //continue;
                } else {
                    strFileText = strFileText.Insert(numInsertIndex, "namespace PixelRipped1989 {" + Environment.NewLine);

                    int numLastBracket = strFileText.LastIndexOf("}");
                    strFileText = strFileText.Insert(strFileText.Length, Environment.NewLine + "}");
                    ConsoleU.WriteLine("File: " + file.Name, ConsoleColor.Green);
                }

                file.Delete();
                File.WriteAllText(strFullName, strFileText);
            }

            return CommandFeedback.Success;
        }

        private void ScanFolder(DirectoryInfo dirInfo, List<string> filters, List<FileInfo> files) {
            for (int x = 0; x < filters.Count; x++) {
                FileInfo[] arrFiles = dirInfo.GetFiles(filters[x]);
                files.AddRange(arrFiles);
            }

            DirectoryInfo[] arrDirs = dirInfo.GetDirectories();
            for (int i = 0; i < arrDirs.Length; i++) {
                ScanFolder(arrDirs[i], filters, files);
            }
        }

        private void RecursiveFolder(DirectoryInfo dirInfo, string strPrefix, List<string> filters) {
            for (int x = 0; x < filters.Count; x++) {
                FileInfo[] arrFiles = dirInfo.GetFiles(filters[x]);
                for (int i = 0; i < arrFiles.Length; i++) {
                    FileInfo file = arrFiles[i];

                    FileInfo similar = destFiles.FirstOrDefault(c => c.Name == file.Name);
                    if (similar == null) {
                        updatedFiles.Add(file);
                        continue;
                    }

                    similarCounter++;
                    string strFolder = Path.GetDirectoryName(file.FullName);
                    string strNewName = strPrefix + file.Name;
                    Console.WriteLine($"Rename: {file.Name} to {strNewName}");

                    string strFullPath = Path.Combine(strFolder, strNewName);

                    string strSource;
                    using (Stream str = file.OpenRead()) {
                        StreamReader reader = new StreamReader(str);
                        strSource = reader.ReadToEnd();
                    }

                    //file.Delete();
                    //FileInfo newFileInfo = new FileInfo(strFullPath);
                    FileInfo newFileInfo = new FileInfo(file.FullName);
                    updatedFiles.Add(newFileInfo);

                    renamedFiles.Add(newFileInfo, strNewName);

                    //int numClassIndex = strSource.IndexOf("class ");
                    //if (numClassIndex == -1) {
                    //    ConsoleU.WriteLine("No class found", ConsoleColor.Yellow);

                    //    // just rename file
                    //    continue;
                    //}
                    //file.MoveTo(strFullPath);
                    //numClassIndex += 6;
                    //int numEndClass = strSource.IndexOf(" ", numClassIndex);
                    //string strClass = strSource.Substring(numClassIndex, numEndClass - numClassIndex);
                    //string strRenamedClass = strPrefix + strClass;
                    //int xoxo = -1;
                }
            }

            DirectoryInfo[] arrDirs = dirInfo.GetDirectories();
            for (int i = 0; i < arrDirs.Length; i++) {
                RecursiveFolder(arrDirs[i], strPrefix, filters);
            }
        }
    }
}
