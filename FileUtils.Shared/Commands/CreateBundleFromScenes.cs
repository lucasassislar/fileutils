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
    public class CreateBundleFromScenes : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Create Asset Bundle from scenes"; }
        }

        private string command = "createbundlefromscenes";
        private string[] parameters = new string[]{
        };

        public override string[] Parameters { get { return parameters; } }
        private List<FileInfo> sourceFiles;
        private List<FileInfo> destFiles;
        private List<FileInfo> updatedFiles;
        private Dictionary<FileInfo, string> renamedFiles;
        private int similarCounter;
        private int referencesFound;

        private Dictionary<string, FileInfo> filesByGuid;
        private List<string> guidsToAdd;

        private List<string> sceneFiles;
        private List<string> prefabFiles;

        public CreateBundleFromScenes(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            // arg1=bundle name
            // arg2=assets base folder
            // arg3-&= scenes

            string strBundleName = args[1];
            string strSourceFolder = args[2];

            sourceFiles = new List<FileInfo>();
            guidsToAdd = new List<string>();
            sceneFiles = new List<string>();
            prefabFiles = new List<string>();
            filesByGuid = new Dictionary<string, FileInfo>();

            for (int i = 3; i < args.Length; i++) {
                string strScene = args[i];
                if (!File.Exists(strScene)) {
                    ConsoleU.WriteLine($"Scene does not exist: {strScene}", ConsoleColor.Red);
                    return CommandFeedback.Error;
                }
                sceneFiles.Add(strScene);
            }

            const string guidStr = "guid: ";
            ScanFolder(new DirectoryInfo(strSourceFolder), new List<string>() { "*.meta" }, sourceFiles);

            for (int i = 0; i < sourceFiles.Count; i++) {
                FileInfo file = sourceFiles[i];                

                string strFileText;
                using (Stream str = file.OpenRead()) {
                    StreamReader strReader = new StreamReader(str);
                    strFileText = strReader.ReadToEnd();
                }

                int numGuidIndex = strFileText.IndexOf(guidStr) + guidStr.Length;
                int nextLineIndex = strFileText.IndexOf('\n', numGuidIndex + 1);
                int breakLineIndex = strFileText.IndexOf('\r', numGuidIndex + 1);
                if (breakLineIndex != -1) {
                    nextLineIndex = Math.Min(breakLineIndex, nextLineIndex);
                }

                string guid = strFileText.Substring(numGuidIndex, nextLineIndex - numGuidIndex);
                if (guid.Length != 32) {
                    ConsoleU.WriteLine($"Guid incorrect length: {guid} - {guid.Length}", ConsoleColor.Red);
                }
                filesByGuid.Add(guid, file);
            }

            int numSceneGuids = 0;

            for (int i = 0; i < sceneFiles.Count; i++) {
                string strScene = sceneFiles[i];

                using (Stream str = File.OpenRead(strScene)) {
                    using (StreamReader reader = new StreamReader(str)) {
                        while (!reader.EndOfStream) {
                            string line = reader.ReadLine();

                            int lastIndex = 0;
                            for (; ; ) {
                                int numGuidIndex = line.IndexOf(guidStr, lastIndex);
                                if (numGuidIndex == -1) {
                                    break;
                                }

                                string guid = line.Substring(numGuidIndex + guidStr.Length, 32);
                                if (!guidsToAdd.Contains(guid)) {
                                    guidsToAdd.Add(guid);
                                    numSceneGuids++;

                                    FileInfo fileInfo;
                                    if (!filesByGuid.TryGetValue(guid, out fileInfo)) {
                                        //ConsoleU.WriteLine($"File not found: {guid} - {line}", ConsoleColor.Red);
                                        continue;
                                    }

                                    string strNoExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
                                    string strExtension = Path.GetExtension(strNoExtension).ToLower();
                                    if (strExtension == ".prefab") {
                                        string strPrefabFullPath = Path.Combine(Path.GetDirectoryName(fileInfo.FullName), strNoExtension);

                                        if (!prefabFiles.Contains(strPrefabFullPath)) {
                                            ConsoleU.WriteLine($"Prefab added: {strPrefabFullPath}", ConsoleColor.Green);
                                            prefabFiles.Add(strPrefabFullPath);
                                        }
                                    }
                                }
                                lastIndex = numGuidIndex + 1;
                            }
                        }
                    }
                }
            }

            int numPrefabGuids = 0;
            int numPrefabInsidePrefab = 0;

            for (int i = 0; i < prefabFiles.Count; i++) {
                string strScene = prefabFiles[i];

                using (Stream str = File.OpenRead(strScene)) {
                    using (StreamReader reader = new StreamReader(str)) {
                        while (!reader.EndOfStream) {
                            string line = reader.ReadLine();

                            int lastIndex = 0;
                            for (; ; ) {
                                int numGuidIndex = line.IndexOf(guidStr, lastIndex);
                                if (numGuidIndex == -1) {
                                    break;
                                }

                                string guid = line.Substring(numGuidIndex + guidStr.Length, 32);
                                if (!guidsToAdd.Contains(guid)) {
                                    guidsToAdd.Add(guid);
                                    numPrefabGuids++;

                                    FileInfo fileInfo;
                                    if (!filesByGuid.TryGetValue(guid, out fileInfo)) {
                                        continue;
                                    }

                                    string strNoExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
                                    string strExtension = Path.GetExtension(strNoExtension).ToLower();
                                    if (strExtension == ".prefab") {
                                        if (!prefabFiles.Contains(fileInfo.FullName)) {
                                            numPrefabInsidePrefab++;
                                            ConsoleU.WriteLine($"Prefab found inside prefab: {fileInfo.FullName}", ConsoleColor.Green);
                                            prefabFiles.Add(fileInfo.FullName);
                                        }
                                    }
                                }
                                lastIndex = numGuidIndex + 1;
                            }
                        }
                    }
                }
            }

            int numModified = 0;
            int numIgnored = 0;

            for (int i = 0; i < guidsToAdd.Count; i++) {
                string guid = guidsToAdd[i];
                FileInfo fileInfo;
                if (!filesByGuid.TryGetValue(guid, out fileInfo)) {
                    ConsoleU.WriteLine($"File not found: {guid}", ConsoleColor.Red);
                    numIgnored++;
                    continue;
                }

                string strFullName = fileInfo.FullName;
                // check if meta is for folder
                string strNoMeta = strFullName.Remove(strFullName.Length - 5, 5);
                if (!File.Exists(strNoMeta)) {
                    ConsoleU.WriteLine($"File doesnt exist: " + strNoMeta, ConsoleColor.Red);
                    continue;
                }

                string strFileText;
                using (Stream str = fileInfo.OpenRead()) {
                    StreamReader strReader = new StreamReader(str);
                    strFileText = strReader.ReadToEnd();
                }

                const string STR_BUNDLE = "assetBundleName";
                int numBundleIndex = strFileText.IndexOf(STR_BUNDLE);
                if (numBundleIndex != -1) {
                    // get word after bundle
                    int numVariantIndex = strFileText.IndexOf("assetBundleVariant");
                    if (numVariantIndex == -1) {
                        ConsoleU.WriteLine($"Asset has bundle but no variant: " + fileInfo.FullName, ConsoleColor.Red);
                        continue;
                    }

                    string bundleName = strFileText.Substring(numBundleIndex, numVariantIndex - numBundleIndex);
                    if (bundleName.IndexOf(strBundleName) != -1) {
                        ConsoleU.WriteLine($"Asset has bundle: " + fileInfo.FullName, ConsoleColor.Yellow);
                        continue;
                    }

                    strFileText = strFileText.Insert(numBundleIndex + STR_BUNDLE.Length + 2, strBundleName);
                } else {
                    strFileText = strFileText + $"\r\n  assetBundleName: {strBundleName}\r\n  assetBundleVariant: ";
                }

                ConsoleU.WriteLine($"Asset has no bundle: " + fileInfo.FullName, ConsoleColor.Green);

                numModified++;
                fileInfo.Delete();
                File.WriteAllText(strFullName, strFileText);
            }

            ConsoleU.WriteLine($"Total metas modified: {numModified}", ConsoleColor.Green);
            ConsoleU.WriteLine($"Total metas ignored: {numIgnored}", ConsoleColor.Yellow);
            ConsoleU.WriteLine($"Total scenes guids: {numSceneGuids}", ConsoleColor.DarkGreen);
            ConsoleU.WriteLine($"Total prefab guids: {numPrefabGuids}", ConsoleColor.DarkGreen);
            ConsoleU.WriteLine($"Total prefab inside prefabs: {numPrefabInsidePrefab}", ConsoleColor.DarkGreen);


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
    }
}
