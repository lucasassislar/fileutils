using Nucleus;
using Nucleus.ConsoleEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileUtils.Commands {
    public class RenameSimilarClasses : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Rename Similar files to different"; }
        }

        private string command = "renameclasses";
        private List<FileInfo> sourceFiles;
        private List<FileInfo> destFiles;
        private List<FileInfo> updatedFiles;
        private Dictionary<FileInfo, string> renamedFiles;
        private int similarCounter;
        private int referencesFound;

        public RenameSimilarClasses(ConsoleManager manager)
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
            destFiles = new List<FileInfo>();
            updatedFiles = new List<FileInfo>();
            renamedFiles = new Dictionary<FileInfo, string>();

            // scan all files from directory info
            ScanFolder(new DirectoryInfo(strSourceFolder), filters, sourceFiles);
            ScanFolder(new DirectoryInfo(strDestiny), filters, destFiles);

            RecursiveFolder(new DirectoryInfo(strSourceFolder), strPrefix, filters);

            ConsoleU.WriteLine("Total similar: " + similarCounter, ConsoleColor.Green);

            for (int i = 0; i < updatedFiles.Count; i++) {
                FileInfo file = updatedFiles[i];
                string strFullName = file.FullName;
                string strFileText;
                using (Stream str = file.OpenRead()) {
                    StreamReader strReader = new StreamReader(str);
                    strFileText = strReader.ReadToEnd();
                }

                //file.Delete();

                foreach (var filePair in renamedFiles) {
                    FileInfo strLastFile = filePair.Key;
                    string strLastName = Path.GetFileNameWithoutExtension(strLastFile.Name);
                    string strNewName = filePair.Value;

                    int numIndex = 0;

                    for (; ; ) {
                        int newIndex = strFileText.IndexOf(strLastName, numIndex);
                        if (newIndex == -1) {
                            break;
                        }

                        int numEndIndex = strFileText.IndexOf(" ", newIndex);
                        if (numEndIndex == -1) {
                            numEndIndex = strFileText.IndexOf(".", newIndex);

                            if (numEndIndex == -1) {
                                continue;
                            }
                        }
                        numIndex = numEndIndex + 1;

                        int numActualStart = strFileText.Substring(0, newIndex).LastIndexOf(" ");
                        int numEndPoint = strFileText.Substring(0, newIndex).LastIndexOf(".");
                        if (numActualStart == -1) {
                            numActualStart = numEndPoint;
                        } else if (numEndPoint != -1) {
                            // both have values
                            numActualStart = Math.Max(numActualStart, numEndPoint);
                        }

                        numActualStart += 1;

                        string strWord = strFileText.Substring(numActualStart, numEndIndex - numActualStart);
                        if (strWord != strLastName) {
                            continue;
                        }

                        int x = -1;
                        referencesFound++;
                        ConsoleU.WriteLine($"Rename {strLastName} to {strNewName}", ConsoleColor.Green);

                    }
                }

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
