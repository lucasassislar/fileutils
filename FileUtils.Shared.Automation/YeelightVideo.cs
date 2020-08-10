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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YeelightAPI;
using Color = System.Drawing.Color;

namespace FileUtils.Automation.Video {
    public class YeelightVideo {
        private Bitmap bmpScreen;
        private Bitmap bmpScreenSmall;
        private Bitmap bmpScreenSmallPreview;
        private LockBitmap locker;
        private ColorThief colorThief;

        private Graphics gSmall;
        private Graphics gSmallPreview;
        private Graphics gSnapshot;

        private Device lightDevice;
        private Screen screen;
        private bool bStop;

        private string strIP;
        private ColorAlgorithm colorAlgorithm;
        private bool bGeneratePreview;
        private int numInterval;

        public Bitmap PreviewImage {
            get { return bmpScreenSmallPreview; }
        }

        private int lastLightBrightness;
        public int LastBrightness {
            get { return lastLightBrightness; }
        }

        private Color lastLightColor;
        public Color LastColor {
            get { return lastLightColor; }
        }

        public YeelightVideo() {
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

        public void Initialize(string strIP, int numScreen, 
            ColorAlgorithm colorAlgorithm, int numInterval,
            bool bGeneratePreview) {
            this.strIP = strIP;
            this.numInterval = numInterval;
            this.colorAlgorithm = colorAlgorithm;
            this.bGeneratePreview = bGeneratePreview;

            screen = Screen.AllScreens[numScreen];
            bmpScreen = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
            bmpScreenSmall = new Bitmap(screen.Bounds.Width / 8, screen.Bounds.Height / 8, PixelFormat.Format24bppRgb);
            bmpScreenSmallPreview = new Bitmap(screen.Bounds.Width / 8, screen.Bounds.Height / 8, PixelFormat.Format24bppRgb);

            gSnapshot = Graphics.FromImage(bmpScreen);
            gSmall = Graphics.FromImage(bmpScreenSmall);
            gSmallPreview = Graphics.FromImage(bmpScreenSmallPreview);

            lightDevice = new Device(strIP);
            AsyncHelpers.RunSync(lightDevice.Connect);

            lightDevice.SetBrightness(100);

            switch (colorAlgorithm) {
                case ColorAlgorithm.AverageColor: 
                case ColorAlgorithm.DominantColor: {
                    locker = new LockBitmap(bmpScreenSmall);
                }
                break;
                case ColorAlgorithm.DominantColorThief: {
                    colorThief = new ColorThief();
                }
                break;
            }
        }

        public static string GetLightIP(string strGateway) {
            IPAddress ipAddress = IPAddress.Parse(strGateway);
            byte[] address = ipAddress.GetAddressBytes();

            for (int i = 0; i < 255; i++) {
                address[3] = (byte)i;
                IPAddress newIP = new IPAddress(address);
                Console.WriteLine($"Looking for light in {newIP.ToString()}");

                Device lightDevice = new Device(newIP.ToString());
                lightDevice.Connect();

                int sleeps = 10;
                for (; ; ) {
                    Thread.Sleep(100);
                    sleeps--;

                    if (sleeps <= 0) {
                        break;
                    }

                    if (lightDevice.IsConnected) {
                        return newIP.ToString();
                    }
                }
            }

            return null;
        }

        public void Execute() {
            Thread thread = new Thread(LightThread);
            thread.Start();
        }

        public void Stop() {
            bStop = true;
        }

        private void LightThread() { 
            Color lastColor = new Color();
            int counter = 0;
            int lastBrightness = -1;
            int lastRGB = -1;
            for (; ; ) {
                if (bStop) {
                    break;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(512));
                //Thread.Sleep(TimeSpan.FromMilliseconds(5000));
                Console.WriteLine($"Tick {counter++}");

                // snapshot display and copy to bitmap
                //g.CopyFromScreen(-1920, 0, 0, 0, new Size(1920, 1080));
                Rectangle bounds = screen.Bounds;
                gSnapshot.CopyFromScreen(bounds.X, bounds.Y, 0, 0, new Size(bounds.Width, bounds.Height));
                // scale bitmap down 8x
                gSmall.DrawImage(bmpScreen, new Rectangle(0, 0, bmpScreenSmall.Width, bmpScreenSmall.Height));
                if (bGeneratePreview) {
                    gSmallPreview.DrawImage(bmpScreen, new Rectangle(0, 0, bmpScreenSmall.Width, bmpScreenSmall.Height));
                }
                //bmpScreenSmall.Save($"F:\\Nuke\\img{counter}.png");

                int brightness = 0;
                Color color = new Color();
                switch (colorAlgorithm) {
                    case ColorAlgorithm.DominantColor: {
                        // lock bits
                        locker.LockBits();
                        color = GetDominantColor(locker.Pixels, out brightness);
                        locker.UnlockBits();
                    }
                    break;
                    case ColorAlgorithm.AverageColor: {
                        // lock bits
                        locker.LockBits();
                        color = GetAverageColor(locker.Pixels, out brightness);
                        locker.UnlockBits();
                    }
                    break;
                    case ColorAlgorithm.DominantColorThief: {
                        QuantizedColor qColor = colorThief.GetColor(bmpScreenSmall);
                        color = Color.FromArgb(qColor.Color.R, qColor.Color.G, qColor.Color.B);
                    }
                    break;
                }
                
                int red = color.R;
                int green = color.G;
                int blue = color.B;
                int num2 = red << 16 | green << 8 | blue;

                if (brightness != lastBrightness) {
                    int deltaBright = Math.Abs(brightness - lastBrightness);

                    if (deltaBright > 7) {
                        lastBrightness = brightness;
                        lastLightBrightness = brightness;

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
                        lastLightColor = color;
                        // dont change if the delta is too small

                        AsyncHelpers.RunSync(() => {
                            return lightDevice.SetRGBColor(red, green, blue);
                        });
                        Console.WriteLine($"New color: {red}, {green}, {blue}");
                    }
                }
                //string strRepeat = StringUtil.RepeatCharacter('■', peakValueBars);
                //Console.WriteLine($"({peakValueCent}) {strRepeat}");
            }

            lightDevice.Disconnect();
        }
    }
}
