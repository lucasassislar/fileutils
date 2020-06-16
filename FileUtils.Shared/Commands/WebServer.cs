using Nucleus;
using Nucleus.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Commands {
    public class WebServer : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Starts a web server in the running folder"; }
        }

        private string command = "web";
        private string[] parameters = new string[]
            {

            };

        public override string[] Parameters { get { return parameters; } }

        private HttpServer httpServer;

        public WebServer(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            ushort port = 80;
            string folder;
            if (args.Length > 1) {
                folder = args[1];
            } else {
                folder = Directory.GetCurrentDirectory();
            }

            Console.WriteLine($"Running web server on port {port}");
            Console.WriteLine($"Working dir: {folder}");

            httpServer = new HttpServer(port, null, folder);
            httpServer.Listen();

            return CommandFeedback.Success;
        }
    }
}
