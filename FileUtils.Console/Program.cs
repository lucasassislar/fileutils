using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUtils.Shared.Automation;
using Nucleus;
using Nucleus.ConsoleEngine;

namespace FileUtils.Console {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            ConsoleManager console = new ConsoleManager();

            console.Init();
            ConsoleU.WriteLine("DistroLucas's FileUtils v" + FileUtilsGlobals.DisplayVersion.ToString("F2", CultureInfo.InvariantCulture), ConsoleColor.White);

            console.SearchFromLoadedAssemblies();

            if (args.Length > 0) {
                console.ExecuteCommand(args);
            }

            console.Run();
        }
    }
}
