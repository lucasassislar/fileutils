using Nucleus;
using Nucleus.ConsoleEngine;
using YeelightAPI;

namespace FileUtils.Automation.Commands {
    public class TurnOffLight : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Converts all non-mp3 files inside all the folders to mp3"; }
        }

        private string command = "lightoff";

        public TurnOffLight(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strIP = args[1];

            Device device = new Device(strIP);
            AsyncHelpers.RunSync(device.Connect);

            AsyncHelpers.RunSync(() => {
                return device.TurnOff();
            });

            device.Disconnect();

            return CommandFeedback.Success;
        }
    }
}
