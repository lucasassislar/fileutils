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
    public class AddBundleToMetas : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Add AssetBundle to Unity meta files"; }
        }

        private string command = "addbundletometas";
        private string[] parameters = new string[]{
        };

        public override string[] Parameters { get { return parameters; } }
        private List<FileInfo> sourceFiles;
        private List<FileInfo> destFiles;
        private List<FileInfo> updatedFiles;
        private Dictionary<FileInfo, string> renamedFiles;
        private int similarCounter;
        private int referencesFound;

        public AddBundleToMetas(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strSourceFolder = args[2];
            string strBundleName = args[1];

            sourceFiles = new List<FileInfo>();
            ScanFolder(new DirectoryInfo(strSourceFolder), new List<string>() { "*.meta" }, sourceFiles);

            int noBundle = 0;
            int hasBundle = 0;
            int noVariant = 0;
            int doesntExist = 0;

            for (int i = 0; i < sourceFiles.Count; i++) {
                FileInfo file = sourceFiles[i];
                string strFullName = file.FullName;
                // check if meta is for folder
                string strNoMeta = strFullName.Remove(strFullName.Length - 5, 5);
                if (!File.Exists(strNoMeta)) {
                    ConsoleU.WriteLine($"File doesnt exist: " + strNoMeta, ConsoleColor.Red);
                    doesntExist++;
                    continue;
                }

                string strFileText;
                using (Stream str = file.OpenRead()) {
                    StreamReader strReader = new StreamReader(str);
                    strFileText = strReader.ReadToEnd();
                }

                const string STR_BUNDLE = "assetBundleName";
                int numBundleIndex = strFileText.IndexOf(STR_BUNDLE);
                if (numBundleIndex != -1) {
                    // get word after bundle
                    int numVariantIndex = strFileText.IndexOf("assetBundleVariant");
                    if (numVariantIndex == -1) {
                        ConsoleU.WriteLine($"Asset has bundle but no variant: " + file.FullName, ConsoleColor.Red);
                        noVariant++;
                        continue;
                    }

                    string bundleName = strFileText.Substring(numBundleIndex, numVariantIndex - numBundleIndex);
                    if (bundleName.IndexOf(strBundleName) != -1) {
                        ConsoleU.WriteLine($"Asset has bundle: " + file.FullName, ConsoleColor.Yellow);
                        hasBundle++;
                        continue;
                    }

                    strFileText = strFileText.Insert(numBundleIndex + STR_BUNDLE.Length + 2, strBundleName);
                } else {
                    strFileText = strFileText + $"\r\n  assetBundleName: {strBundleName}\r\n  assetBundleVariant: ";
                }

                ConsoleU.WriteLine($"Asset has no bundle: " + file.FullName, ConsoleColor.Green);
                noBundle++;
                //continue;

                file.Delete();
                File.WriteAllText(strFullName, strFileText);
            }

            ConsoleU.WriteLine($"Total: {hasBundle}/{noBundle}", ConsoleColor.Green);
            ConsoleU.WriteLine($"Total: No variant: {noVariant} Doesnt Exist: {doesntExist}", ConsoleColor.Green);

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
