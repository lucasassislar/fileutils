﻿using Nucleus;
using Nucleus.ConsoleEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FileUtils.Commands {
    public class FixSpotifyM4A : ConsoleCommand
    {
        public override string Command { get { return command; } }

        public override string Help
        {
            get { return $"Writes text to a a text file header\n" +
                    $"Usage: fixspotifym4a [filepath]" ; }
        }

        private string command = "fixspotifym4a";

        public FixSpotifyM4A(ConsoleManager manager)
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
            if (!Path.IsPathRooted(target)) {
                // relative path
                string currentDir = Environment.CurrentDirectory;
                target = Path.Combine(currentDir, target);
            }

            if (!File.Exists(target))
            {
                Console.WriteLine($"File not found: {target}");
                return CommandFeedback.Error;
            }

            // rename the source file to backup
            string dir = Path.GetDirectoryName(target);
            string fileName = Path.GetFileNameWithoutExtension(target);
            string sourceFile = Path.Combine(dir, fileName + "_Backup.m4a");
            File.Move(target, sourceFile);

            string startArgs = $"ffmpeg -i \"{sourceFile}\" -acodec copy -movflags faststart \"{target}\"";

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
                ConsoleU.WriteLine($"FFMPEG failed with code {exitCode}", Palette.Error);
                return CommandFeedback.Error;
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
