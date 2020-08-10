using FileUtils.Shared.Automation;
using Nucleus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;

namespace FileUtils.Automation.Commands {
    public class TurnOnLight : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Converts all non-mp3 files inside all the folders to mp3"; }
        }

        private string command = "lighton";
        private string[] parameters = new string[] { };

        public override string[] Parameters { get { return parameters; } }

        public TurnOnLight(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strIP = args[1];

            Device device = new Device(strIP);
            AsyncHelpers.RunSync(device.Connect);

            AsyncHelpers.RunSync(() => {
                return device.TurnOn();
            });

            device.Disconnect();

            return CommandFeedback.Success;
        }
    }
}
