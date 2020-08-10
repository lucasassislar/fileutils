using FileUtils.Shared.Automation;
using NAudio.Wave;
using Nucleus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YeelightAPI;

namespace FileUtils.Automation.Commands {
    public class MusicLight : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Converts all non-mp3 files inside all the folders to mp3"; }
        }

        private string command = "lightmusic";
        private string[] parameters = new string[] { };

        public override string[] Parameters { get { return parameters; } }

        public MusicLight(ConsoleManager manager)
            : base(manager) {
        }

        public override CommandFeedback Execute(string[] args) {
            string strIP = args[1];
            string audioOutput = args[2];

            NAudio.CoreAudioApi.MMDeviceEnumerator enumerator = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            var audioDevices = enumerator.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.All, NAudio.CoreAudioApi.DeviceState.Active);
            var audioDevice = audioDevices.FirstOrDefault(c => c.FriendlyName.ToLower().Contains(audioOutput));

            if (audioDevice == null) {
                ConsoleU.WriteLine("Failed to locate audio device", ConsoleColor.Red);
                return CommandFeedback.Error;
            }

            //WasapiLoopbackCapture capture = new WasapiLoopbackCapture();
            //capture.DataAvailable += (object s, WaveInEventArgs waveArgs) => {

            //    //writer.Write(a.Buffer, 0, a.BytesRecorded);
            //    //if (writer.Position > capture.WaveFormat.AverageBytesPerSecond * 20) {
            //    //    capture.StopRecording();
            //    //}
            //};

            Device device = new Device(strIP);
            AsyncHelpers.RunSync(device.Connect);

            int lastBrightness = 1;
            AsyncHelpers.RunSync(() => {
                for (; ; ) {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1001));

                    float peakValue = audioDevice.AudioMeterInformation.MasterPeakValue;
                    int peakValueCent = (int)(peakValue * 120);
                    int peakValueBars = (int)(peakValue * 120);
                    int peakValueInt = (int)(peakValue * 200.0f);

                    peakValueCent = MathUtil.Clamp(peakValueCent, 1, 100);

                    //Math.Log()

                    //device.SetBrightness((int)(audioDevice.AudioMeterInformation.MasterPeakValue * 200));

                    //AsyncHelpers.RunSync(() => {
                    //    return device.SetRGBColor(peakValueInt / 2, peakValueInt / 2, peakValueInt);
                    //});

                    if (lastBrightness != peakValueCent) {
                        lastBrightness = peakValueCent;
                        AsyncHelpers.RunSync(() => {
                            return device.SetBrightness(peakValueCent);
                        });
                    }

                    string strRepeat = StringUtil.RepeatCharacter('■', peakValueBars);
                    Console.WriteLine($"({peakValueCent}) {strRepeat}");
                    //Console.WriteLine(peakValueInt);
                }
            });

            device.Disconnect();

            return CommandFeedback.Success;
        }
    }
}
