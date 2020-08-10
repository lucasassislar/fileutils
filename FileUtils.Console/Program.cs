using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileUtils.Shared.Automation;

namespace FileUtils.Console {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            ConsoleManager console = new ConsoleManager();

            console.Init();
            console.SearchFromLoadedAssemblies();

            if (args.Length > 0) {
                console.ExecuteCommand(args);
            }

            console.Run();
        }
    }
}
