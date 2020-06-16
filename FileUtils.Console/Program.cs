using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Console {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            ConsoleManager console = new ConsoleManager();

            console.Init();
            console.SearchFromLoadedAssemblies();

            if (args.Length > 0) {
                console.ExecuteCommand(args);
            } else {
                console.Run();
            }

            System.Console.ReadLine();
        }
    }
}
