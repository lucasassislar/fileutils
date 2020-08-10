using ColorThiefDotNet;
using FileUtils.Shared.Automation;
using NAudio.Wave;
using Nucleus;
using Nucleus.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YeelightAPI;
using Color = System.Drawing.Color;

namespace FileUtils.Automation.Commands {
    public class VideoLight : ConsoleCommand {
        public override string Command { get { return command; } }

        public override string Help {
            get { return "Converts all non-mp3 files inside all the folders to mp3"; }
        }

        private string command = "lightvideo";
        private string[] parameters = new string[] { };

        public override string[] Parameters { get { return parameters; } }

        private Bitmap bmpScreen;
        private Bitmap bmpScreenSmall;

        public VideoLight(ConsoleManager manager)
            : base(manager) {
        }

        public static Color GetDominantColor(byte[] colors, out int brightness) {
            Dictionary<Color, int?> source = new Dictionary<Color, int?>();

            double totalBrightness = 0;
            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];
                if (b < 10 && g < 10 && r < 10) {
                    totalBrightness += 0; // no color
                    continue;
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;

                Color color = Color.FromArgb(r, g, b);
                if (!source.ContainsKey(color)) {
                    source[color] = new int?(0);
                }

                Color index1 = color;
                int? nullable1 = source[index1];
                Color index2 = index1;
                int? nullable2 = nullable1;
                int? nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
                source[index2] = nullable3;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            brightness = (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);

            return source.OrderByDescending<KeyValuePair<Color, int?>, int?>((Func<KeyValuePair<Color, int?>, int?>)(p => p.Value)).First<KeyValuePair<Color, int?>>().Key;
        }

        public static Color GetAverageColor(byte[] colors, out int brightness) {
            double totalBrightness = 0;
            double totalR = 0;
            double totalG = 0;
            double totalB = 0;

            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];

                totalR += r;
                totalG += g;
                totalB += b;

                if (b < 10 && g < 10 && r < 10) {
                    totalBrightness += 0; // no color
                    continue;
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            brightness = (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);

            return Color.FromArgb((byte)(totalR / totalColors), (byte)(totalG / totalColors), (byte)(totalB / totalColors));
        }

        public static Color GetDominantColor(IEnumerable<Color> colors) {
            Dictionary<Color, int?> source = new Dictionary<Color, int?>();

            foreach (Color color in colors) {
                if (!source.ContainsKey(color)) {
                    source[color] = new int?(0);
                }

                Color index1 = color;
                int? nullable1 = source[index1];
                Color index2 = index1;
                int? nullable2 = nullable1;
                int? nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
                source[index2] = nullable3;
            }
            return source.OrderByDescending<KeyValuePair<Color, int?>, int?>((Func<KeyValuePair<Color, int?>, int?>)(p => p.Value)).First<KeyValuePair<Color, int?>>().Key;
        }

        public override CommandFeedback Execute(string[] args) {
            string strIP = args[1];

            bmpScreen = new Bitmap(1920, 1080);
            bmpScreenSmall = new Bitmap(240, 135, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpScreen);
            Graphics gSmall = Graphics.FromImage(bmpScreenSmall);
            LockBitmap locker = new LockBitmap(bmpScreenSmall);

            Device device = new Device(strIP);
            AsyncHelpers.RunSync(device.Connect);

            device.SetBrightness(100);

            ColorThief colorThief = new ColorThief();

            Color lastColor = new Color();
            int counter = 0;
            int lastBrightness = -1;
            int lastRGB = -1;
            for (; ; ) {
                Thread.Sleep(TimeSpan.FromMilliseconds(512));
                //Thread.Sleep(TimeSpan.FromMilliseconds(5000));
                Console.WriteLine($"Tick {counter++}");

                // snapshot display and copy to bitmap
                //g.CopyFromScreen(-1920, 0, 0, 0, new Size(1920, 1080));
                g.CopyFromScreen(0, 0, 0, 0, new Size(1920, 1080));
                // scale bitmap down 8x
                gSmall.DrawImage(bmpScreen, new Rectangle(0, 0, bmpScreenSmall.Width, bmpScreenSmall.Height));
                bmpScreenSmall.Save($"F:\\Nuke\\img{counter}.png");


                // lock bits
                //locker.LockBits();
                //int brightness;
                //Color color = GetDominantColor(locker.Pixels, out brightness);
                //Color color = GetAverageColor(locker.Pixels, out brightness);
                //locker.UnlockBits();

                int brightness = 0;

                QuantizedColor qColor = colorThief.GetColor(bmpScreenSmall);
                Color color = Color.FromArgb(qColor.Color.R, qColor.Color.G, qColor.Color.B);

                int red = color.R;
                int green = color.G;
                int blue = color.B;
                int num2 = red << 16 | green << 8 | blue;

                if (brightness != lastBrightness) {
                    int deltaBright = Math.Abs(brightness - lastBrightness);

                    if (deltaBright > 7) {
                        lastBrightness = brightness;

                        //device.SetBrightness(brightness);
                        Console.WriteLine($"New brightness: {brightness}");
                    }
                }

                if (num2 != lastRGB) {
                    lastRGB = num2;

                    int deltaR = Math.Abs(red - lastColor.R);
                    int deltaG = Math.Abs(green - lastColor.G);
                    int deltaB = Math.Abs(blue - lastColor.B);

                    if (deltaR > 25 ||
                        deltaG > 25 ||
                        deltaB > 25) {

                        //if (deltaR > 5 ||
                        //   deltaG > 5 ||
                        //   deltaB > 5) {
                        lastColor = color;
                        // dont change if the delta is too small

                        AsyncHelpers.RunSync(() => {
                            return device.SetRGBColor(red, green, blue);
                        });
                        Console.WriteLine($"New color: {red}, {green}, {blue}");
                    }
                }
                //string strRepeat = StringUtil.RepeatCharacter('■', peakValueBars);
                //Console.WriteLine($"({peakValueCent}) {strRepeat}");
            }

            device.Disconnect();

            return CommandFeedback.Success;
        }
    }
}
